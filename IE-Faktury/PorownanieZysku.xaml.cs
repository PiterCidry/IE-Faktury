using MathNet.Numerics.Distributions;
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

namespace IE_Faktury
{
    /// <summary>
    /// Okno do porównywania zysku na dwóch produktach.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class PorownanieZysku : Window
    {
        /// <summary>
        /// Lista z datami.
        /// </summary>
        private List<DateTime> daty = new List<DateTime>();
        /// <summary>
        /// Lista z zyskami na pierwszym produkcie.
        /// </summary>
        private List<double> zyskiProd1 = new List<double>();
        /// <summary>
        /// Lista z zyskami na drugim produkcie.
        /// </summary>
        private List<double> zyskiProd2 = new List<double>();
        /// <summary>
        /// Baza faktur.
        /// </summary>
        private BazaFaktur bazaFaktur = new BazaFaktur();
        /// <summary>
        /// Klasa do analizy statystycznej pierwszego produktu.
        /// </summary>
        private Descriptive descProd1 = new Descriptive();
        /// <summary>
        /// Klasa do analizy statystycznej drugiego produktu.
        /// </summary>
        private Descriptive descProd2 = new Descriptive();
        /// <summary>
        /// Lista z nazwami produktów.
        /// </summary>
        private List<string> nazwyProduktow = new List<string>();

        /// <summary>
        /// Kontruktor domyślny okna: <see cref="PorownanieZysku"/>.
        /// </summary>
        public PorownanieZysku()
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
            int iloscProd1 = 0, iloscProd2 = 0;
            foreach (Faktura f in bazaFaktur.listaFaktur)
            {
                if (comboBox_produkt1.SelectedItem != null && comboBox_produkt2.SelectedItem != null && comboBox_produkt1.SelectedItem != comboBox_produkt2.SelectedItem)
                {
                    if (f.DataWystawienia >= dataPocz.SelectedDate && f.DataWystawienia <= dataKon.SelectedDate)
                    {
                        if (f.OdbiorcaFizyczny != null && f.OdbiorcaPrawny == null)
                        {
                            foreach (var kvp in f.ProduktyList)
                            {
                                if(kvp.Key.Nazwa == comboBox_produkt1.SelectedItem.ToString())
                                {
                                    zyskiProd1.Add((kvp.Key.CenaBrutto * kvp.Value * f.OdbiorcaFizyczny.Rabat) - (kvp.Key.CenaHurtownia * kvp.Value));
                                    iloscProd1 += kvp.Value;
                                }
                                else if(kvp.Key.Nazwa == comboBox_produkt2.SelectedItem.ToString())
                                {
                                    zyskiProd2.Add((kvp.Key.CenaBrutto * kvp.Value * f.OdbiorcaFizyczny.Rabat) - (kvp.Key.CenaHurtownia * kvp.Value));
                                    iloscProd2 += kvp.Value;
                                }
                            }
                        }
                        else if (f.OdbiorcaPrawny != null && f.OdbiorcaFizyczny == null)
                        {
                            foreach (var kvp in f.ProduktyList)
                            {
                                if (kvp.Key.Nazwa == comboBox_produkt1.SelectedItem.ToString())
                                {
                                    zyskiProd1.Add((kvp.Key.CenaBrutto * kvp.Value * f.OdbiorcaPrawny.Rabat) - (kvp.Key.CenaHurtownia * kvp.Value));
                                    iloscProd1 += kvp.Value;
                                }
                                else if (kvp.Key.Nazwa == comboBox_produkt2.SelectedItem.ToString())
                                {
                                    zyskiProd2.Add((kvp.Key.CenaBrutto * kvp.Value * f.OdbiorcaPrawny.Rabat) - (kvp.Key.CenaHurtownia * kvp.Value));
                                    iloscProd2 += kvp.Value;
                                }
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
            //analiza dwóch średnich.
            try
            {
                double sredniaProd1, sredniaProd2, varProd1, varProd2, n1, n2, statTestowa, statKrytyczna;
                descProd1 = new Descriptive(zyskiProd1.ToArray());
                descProd1.Analyze();
                sredniaProd1 = descProd1.Result.Mean;
                Debug.WriteLine("X1sr: " + sredniaProd1.ToString());
                varProd1 = descProd1.Result.Variance;
                Debug.WriteLine("X1var: " + varProd1.ToString());
                n1 = iloscProd1;
                Debug.WriteLine("N1: " + n1.ToString());
                descProd2 = new Descriptive(zyskiProd2.ToArray());
                descProd2.Analyze();
                sredniaProd2 = descProd2.Result.Mean;
                Debug.WriteLine("X2sr: " + sredniaProd2.ToString());
                varProd2 = descProd2.Result.Variance;
                Debug.WriteLine("X1var: " + varProd2.ToString());
                n2 = iloscProd2;
                Debug.WriteLine("N2: " + n2.ToString());
                statTestowa = (sredniaProd1 - sredniaProd2) / Math.Sqrt((varProd1 / n1) + (varProd2 / n2));
                Debug.WriteLine("Stat testowa: "+statTestowa.ToString());
                Normal normal = new Normal(0, 1);
                statKrytyczna = normal.InverseCumulativeDistribution(0.95);
                Debug.WriteLine("Stat kryt: " + statKrytyczna.ToString());
                MessageBoxResult res = MessageBoxResult.Yes;
                if (Math.Abs(statTestowa) >= statKrytyczna)
                {
                    statKrytyczna = normal.InverseCumulativeDistribution(0.9);
                    Debug.WriteLine("Stat kryt2: " + statKrytyczna.ToString());
                    if (statTestowa >= statKrytyczna)
                    {
                        res = MessageBox.Show(String.Format("Zysk na produkcie {0} jest istotnie większy niż na produkcie {1} w badanym okresie na poziomie ufności 95%\nCzy chcesz przeanalizować jeszcze jakieś produkty?", comboBox_produkt1.SelectedItem.ToString(), comboBox_produkt2.SelectedItem.ToString()), "Rezultat", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    }
                    else if (statTestowa <= -statKrytyczna)
                    {
                        res = MessageBox.Show(String.Format("Zysk na produkcie {0} jest istotnie mniejszy niż na produkcie {1} w badanym okresie na poziomie ufności 95%\nCzy chcesz przeanalizować jeszcze jakieś produkty?", comboBox_produkt1.SelectedItem.ToString(), comboBox_produkt2.SelectedItem.ToString()), "Rezultat", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    }
                }
                else
                {
                    res = MessageBox.Show("Zyski na podanych produktach nie różnią się istotnie statystycznie w badanym okresie na poziomie ufności 95%\nCzy chcesz przeanalizować jeszcze jakieś produkty?", "Rezultat", MessageBoxButton.YesNo, MessageBoxImage.Information);
                }
                if (res == MessageBoxResult.Yes)
                {
                    zyskiProd1.Clear();
                    zyskiProd2.Clear();
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
