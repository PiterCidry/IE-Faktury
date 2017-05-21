using StatDescriptive;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MathNet.Numerics.Distributions;

namespace IE_Faktury
{
    /// <summary>
    /// Okno do porównywania sprzedaży dwóch produktów.
    /// </summary>
    public partial class PorownanieSprzedazy : Window
    {
        /// <summary>
        /// Lista z datami.
        /// </summary>
        private List<DateTime> daty = new List<DateTime>();
        /// <summary>
        /// Lista z ilościami sprzedanych produktów typu pierwszego.
        /// </summary>
        private List<double> ilosciProd1 = new List<double>();
        /// <summary>
        /// Lista z ilościami sprzedanych produktów typu drugiego.
        /// </summary>
        private List<double> ilosciProd2 = new List<double>();
        /// <summary>
        /// Baza faktur.
        /// </summary>
        private BazaFaktur bazaFaktur = new BazaFaktur();
        /// <summary>
        /// Klasa do obsługi statystyk produktu typu pierwszego.
        /// </summary>
        private Descriptive descProd1 = new Descriptive();
        /// <summary>
        /// Klasa do obsługi statystyk produktu typu drugiego.
        /// </summary>
        private Descriptive descProd2 = new Descriptive();
        /// <summary>
        /// Lista z nazwami produktów
        /// </summary>
        private List<string> nazwyProduktow = new List<string>();

        /// <summary>
        /// Konstruktor domyślny okna: <see cref="PorownanieSprzedazy"/>.
        /// </summary>
        public PorownanieSprzedazy()
        {
            InitializeComponent();
            bazaFaktur = (BazaFaktur)bazaFaktur.OdczytajBaze();
            foreach (Faktura f in bazaFaktur.listaFaktur)
            {
                daty.Add(f.DataWystawienia);
            }
            foreach (Faktura f in bazaFaktur.listaFaktur)
            {
                foreach (var kvp in f.ProduktyList)
                {
                    if (!nazwyProduktow.Contains(kvp.Key.Nazwa))
                    {
                        nazwyProduktow.Add(kvp.Key.Nazwa);
                    }
                }
            }
            daty.Sort();
            dataPocz.DisplayDateStart = daty.First();
            dataPocz.DisplayDateEnd = daty.Last();
            dataKon.DisplayDateStart = daty.First();
            dataKon.DisplayDateEnd = daty.Last();
            comboBox_produkt1.ItemsSource = nazwyProduktow;
            comboBox_produkt2.ItemsSource = nazwyProduktow;
        }

        /// <summary>
        ///  Obsługa zdarzenia naciśnięcia przycisku porównaj.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_porownaj_Click(object sender, RoutedEventArgs e)
        {
            foreach (Faktura f in bazaFaktur.listaFaktur)
            {
                if (comboBox_produkt1.SelectedItem != null && comboBox_produkt2.SelectedItem !=null && comboBox_produkt1.SelectedItem != comboBox_produkt2.SelectedItem)
                {
                    if(f.DataWystawienia >= dataPocz.SelectedDate && f.DataWystawienia <= dataKon.SelectedDate)
                    {
                        foreach (var item in f.ProduktyList)
                        {
                            if (item.Key.Nazwa == comboBox_produkt1.SelectedItem.ToString())
                            {
                                ilosciProd1.Add(item.Value);
                            }
                            else if (item.Key.Nazwa == comboBox_produkt2.SelectedItem.ToString())
                            {
                                ilosciProd2.Add(item.Value);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Produkty wybrane niepoprawnie!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            //testowanie dwóch średnich.
            try
            {
                double sredniaProd1, sredniaProd2, varProd1, varProd2, n1, n2, statTestowa, statKrytyczna;
                descProd1 = new Descriptive(ilosciProd1.ToArray());
                descProd1.Analyze();
                sredniaProd1 = descProd1.Result.Mean;
                varProd1 = descProd1.Result.Variance;
                n1 = ilosciProd1.Sum();
                descProd2 = new Descriptive(ilosciProd2.ToArray());
                descProd2.Analyze();
                sredniaProd2 = descProd2.Result.Mean;
                varProd2 = descProd2.Result.Variance;
                n2 = ilosciProd2.Sum();
                statTestowa = (sredniaProd1 - sredniaProd2) / Math.Sqrt((varProd1 / n1) + (varProd2 / n2));
                Normal normal = new Normal(0, 1);
                statKrytyczna = normal.InverseCumulativeDistribution(0.95);
                MessageBoxResult res = MessageBoxResult.Yes;
                if (Math.Abs(statTestowa) >= statKrytyczna)
                {
                    statKrytyczna = normal.InverseCumulativeDistribution(0.9);
                    if (statTestowa >= statKrytyczna)
                    {
                        res = MessageBox.Show(String.Format("Sprzedaż produktu {0} jest istotnie większa niż produktu {1} w badanym okresie na poziomie ufności 95%\nCzy chcesz przeanalizować jeszcze jakieś produkty?", comboBox_produkt1.SelectedItem.ToString(), comboBox_produkt2.SelectedItem.ToString()), "Rezultat", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    }
                    else if (statTestowa <= -statKrytyczna)
                    {
                        res = MessageBox.Show(String.Format("Sprzedaż produktu {0} jest istotnie mniejsza niż produktu {1} w badanym okresie na poziomie ufności 95%\nCzy chcesz przeanalizować jeszcze jakieś produkty?", comboBox_produkt1.SelectedItem.ToString(), comboBox_produkt2.SelectedItem.ToString()), "Rezultat", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    }
                }
                else
                {
                    res = MessageBox.Show("Podane produkty nie różnią się ilością sprzedaży istotnie statystycznie w badanym okresie na poziomie ufności 95%\nCzy chcesz przeanalizować jeszcze jakieś produkty?", "Rezultat", MessageBoxButton.YesNo, MessageBoxImage.Information);
                }
                if (res == MessageBoxResult.Yes)
                {
                    ilosciProd1.Clear();
                    ilosciProd2.Clear();
                    return;
                }
                else
                {
                    this.Close();
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                MessageBox.Show("Bład przy wprowadzaniu dat lub brak wystarczającej ilości faktur w wybranym okresie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}