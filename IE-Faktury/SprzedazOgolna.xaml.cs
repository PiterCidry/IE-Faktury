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
using StatDescriptive;

namespace IE_Faktury
{
    /// <summary>
    /// Interaction logic for SprzedazOgolna.xaml
    /// </summary>
    public partial class SprzedazOgolna : Window
    {
        private List<DateTime> daty = new List<DateTime>();
        private List<double> ceny = new List<double>();
        private BazaFaktur bazaFaktur = new BazaFaktur();
        private Descriptive desc = new Descriptive();

        public SprzedazOgolna()
        {
            InitializeComponent();
            bazaFaktur = (BazaFaktur)bazaFaktur.OdczytajBaze();
            foreach (Faktura f in bazaFaktur.listaFaktur)
            {
                daty.Add(f.DataWystawienia);
            }
            daty.Sort();
            dataPocz.DisplayDateStart = daty.First();
            dataPocz.DisplayDateEnd = daty.Last();
            dataKon.DisplayDateStart = daty.First();
            dataKon.DisplayDateEnd = daty.Last();
        }

        private void button_pokaz_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton_przychod.IsChecked == true)
            {
                foreach (Faktura f in bazaFaktur.listaFaktur)
                {
                    if(f.DataWystawienia <= dataKon.SelectedDate && f.DataWystawienia >= dataPocz.SelectedDate)
                    {
                        ceny.Add(f.Razem);
                    }
                }
                desc = new Descriptive(ceny.ToArray());
                try
                {
                    desc.Analyze();
                    if (dataKon.SelectedDate < dataPocz.SelectedDate)
                    {
                        MessageBox.Show("Data koncowa nie moze byc wczesniejsza niz poczatkowa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (dataKon.SelectedDate == null || dataPocz.SelectedDate == null)
                    {
                        MessageBox.Show("Trzeba wybrac obie daty!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    textBox_srednia.Text = String.Format("{0:N2}", desc.Result.Mean);
                    textBox_mediana.Text = String.Format("{0:N2}", desc.Result.Median);
                    textBox_q1.Text = String.Format("{0:N2}", desc.Result.FirstQuartile);
                    textBox_q2.Text = String.Format("{0:N2}", desc.Result.ThirdQuartile);
                    textBox_min.Text = String.Format("{0:N2}", desc.Result.Min);
                    textBox_max.Text = String.Format("{0:N2}", desc.Result.Max);
                    textBox_kurtoza.Text = String.Format("{0:N2}", desc.Result.Kurtosis);
                    textBox_skosnosc.Text = String.Format("{0:N2}", desc.Result.Skewness);
                    textBox_standardowe.Text = String.Format("{0:N2}", desc.Result.StdDev);
                }
                catch (System.IndexOutOfRangeException)
                {
                    MessageBox.Show("Brak faktur wystawionych w wybranym okresie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBox_srednia.Text = "";
                    textBox_mediana.Text = "";
                    textBox_q1.Text = "";
                    textBox_q2.Text = "";
                    textBox_min.Text = "";
                    textBox_max.Text = "";
                    textBox_kurtoza.Text = "";
                    textBox_skosnosc.Text = "";
                    textBox_standardowe.Text = "";
                    return;
                }
                ceny.Clear();
            }
            else if (radioButton_koszty.IsChecked == true)
            {
                foreach (Faktura f in bazaFaktur.listaFaktur)
                {
                    if (f.DataWystawienia <= dataKon.SelectedDate && f.DataWystawienia >= dataPocz.SelectedDate)
                    {
                        double kosztFaktury = 0;
                        foreach (var kvp in f.ProduktyList)
                        {
                            kosztFaktury += kvp.Key.CenaHurtownia * kvp.Value;
                        }
                        ceny.Add(kosztFaktury);
                    }
                }
                desc = new Descriptive(ceny.ToArray());
                try
                {
                    desc.Analyze();
                    if (dataKon.SelectedDate < dataPocz.SelectedDate)
                    {
                        MessageBox.Show("Data koncowa nie moze byc wczesniejsza niz poczatkowa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (dataKon.SelectedDate == null || dataPocz.SelectedDate == null)
                    {
                        MessageBox.Show("Trzeba wybrac obie daty!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    textBox_srednia.Text = String.Format("{0:N2}", desc.Result.Mean);
                    textBox_mediana.Text = String.Format("{0:N2}", desc.Result.Median);
                    textBox_q1.Text = String.Format("{0:N2}", desc.Result.FirstQuartile);
                    textBox_q2.Text = String.Format("{0:N2}", desc.Result.ThirdQuartile);
                    textBox_min.Text = String.Format("{0:N2}", desc.Result.Min);
                    textBox_max.Text = String.Format("{0:N2}", desc.Result.Max);
                    textBox_kurtoza.Text = String.Format("{0:N2}", desc.Result.Kurtosis);
                    textBox_skosnosc.Text = String.Format("{0:N2}", desc.Result.Skewness);
                    textBox_standardowe.Text = String.Format("{0:N2}", desc.Result.StdDev);
                }
                catch (System.IndexOutOfRangeException)
                {
                    MessageBox.Show("Brak faktur wystawionych w wybranym okresie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBox_srednia.Text = "";
                    textBox_mediana.Text = "";
                    textBox_q1.Text = "";
                    textBox_q2.Text = "";
                    textBox_min.Text = "";
                    textBox_max.Text = "";
                    textBox_kurtoza.Text = "";
                    textBox_skosnosc.Text = "";
                    textBox_standardowe.Text = "";
                    return;
                }
                ceny.Clear();
            }
            else if (radioButton_zysk.IsChecked == true)
            {
                foreach (Faktura f in bazaFaktur.listaFaktur)
                {
                    if (f.DataWystawienia <= dataKon.SelectedDate && f.DataWystawienia >= dataPocz.SelectedDate)
                    {
                        if(f.OdbiorcaFizyczny != null && f.OdbiorcaPrawny == null)
                        {
                            double zyskFaktury = 0;
                            foreach (var kvp in f.ProduktyList)
                            {
                                zyskFaktury += (kvp.Key.CenaBrutto * f.OdbiorcaFizyczny.Rabat * kvp.Value) - (kvp.Key.CenaHurtownia * kvp.Value);
                            }
                            ceny.Add(zyskFaktury);
                        }
                        else if(f.OdbiorcaPrawny != null && f.OdbiorcaFizyczny == null)
                        {
                            double zyskFaktury = 0;
                            foreach (var kvp in f.ProduktyList)
                            {
                                zyskFaktury += (kvp.Key.CenaBrutto * f.OdbiorcaPrawny.Rabat * kvp.Value) - (kvp.Key.CenaHurtownia * kvp.Value);
                            }
                            ceny.Add(zyskFaktury);
                        }
                    }
                }
                desc = new Descriptive(ceny.ToArray());
                try
                {
                    desc.Analyze();
                    if (dataKon.SelectedDate < dataPocz.SelectedDate)
                    {
                        MessageBox.Show("Data koncowa nie moze byc wczesniejsza niz poczatkowa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (dataKon.SelectedDate == null || dataPocz.SelectedDate == null)
                    {
                        MessageBox.Show("Trzeba wybrac obie daty!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    textBox_srednia.Text = String.Format("{0:N2}", desc.Result.Mean);
                    textBox_mediana.Text = String.Format("{0:N2}", desc.Result.Median);
                    textBox_q1.Text = String.Format("{0:N2}", desc.Result.FirstQuartile);
                    textBox_q2.Text = String.Format("{0:N2}", desc.Result.ThirdQuartile);
                    textBox_min.Text = String.Format("{0:N2}", desc.Result.Min);
                    textBox_max.Text = String.Format("{0:N2}", desc.Result.Max);
                    textBox_kurtoza.Text = String.Format("{0:N2}", desc.Result.Kurtosis);
                    textBox_skosnosc.Text = String.Format("{0:N2}", desc.Result.Skewness);
                    textBox_standardowe.Text = String.Format("{0:N2}", desc.Result.StdDev);
                }
                catch (System.IndexOutOfRangeException)
                {
                    MessageBox.Show("Brak faktur wystawionych w wybranym okresie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBox_srednia.Text = "";
                    textBox_mediana.Text = "";
                    textBox_q1.Text = "";
                    textBox_q2.Text = "";
                    textBox_min.Text = "";
                    textBox_max.Text = "";
                    textBox_kurtoza.Text = "";
                    textBox_skosnosc.Text = "";
                    textBox_standardowe.Text = "";
                    return;
                }
                ceny.Clear();
            }
            else if (radioButton_iloscProd.IsChecked == true)
            {
                foreach (Faktura f in bazaFaktur.listaFaktur)
                {
                    if (f.DataWystawienia <= dataKon.SelectedDate && f.DataWystawienia >= dataPocz.SelectedDate)
                    {
                        int ilosc = 0;
                        foreach (var item in f.ProduktyList)
                        {
                            ilosc += item.Value;
                        }
                        ceny.Add(ilosc);
                    }
                }
                desc = new Descriptive(ceny.ToArray());
                try
                {
                    desc.Analyze();
                    if (dataKon.SelectedDate < dataPocz.SelectedDate)
                    {
                        MessageBox.Show("Data koncowa nie moze byc wczesniejsza niz poczatkowa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (dataKon.SelectedDate == null || dataPocz.SelectedDate == null)
                    {
                        MessageBox.Show("Trzeba wybrac obie daty!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    textBox_srednia.Text = String.Format("{0:N2}", desc.Result.Mean);
                    textBox_mediana.Text = String.Format("{0:N2}", desc.Result.Median);
                    textBox_q1.Text = String.Format("{0:N2}", desc.Result.FirstQuartile);
                    textBox_q2.Text = String.Format("{0:N2}", desc.Result.ThirdQuartile);
                    textBox_min.Text = String.Format("{0:N2}", desc.Result.Min);
                    textBox_max.Text = String.Format("{0:N2}", desc.Result.Max);
                    textBox_kurtoza.Text = String.Format("{0:N2}", desc.Result.Kurtosis);
                    textBox_skosnosc.Text = String.Format("{0:N2}", desc.Result.Skewness);
                    textBox_standardowe.Text = String.Format("{0:N2}", desc.Result.StdDev);
                }
                catch (System.IndexOutOfRangeException)
                {
                    MessageBox.Show("Brak faktur wystawionych w wybranym okresie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBox_srednia.Text = "";
                    textBox_mediana.Text = "";
                    textBox_q1.Text = "";
                    textBox_q2.Text = "";
                    textBox_min.Text = "";
                    textBox_max.Text = "";
                    textBox_kurtoza.Text = "";
                    textBox_skosnosc.Text = "";
                    textBox_standardowe.Text = "";
                    return;
                }
                ceny.Clear();
            }
        }
    }
}
