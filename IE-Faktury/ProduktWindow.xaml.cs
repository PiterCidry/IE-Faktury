using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Okno do dodawania i zmiany produktów.
    /// </summary>
    public partial class ProduktWindow : Window
    {
        /// <summary>
        /// Produkt do dodania/zmiany.
        /// </summary>
        Produkt p;

        /// <summary>
        /// Konstruktor domyślny okna: <see cref="ProduktWindow"/>.
        /// </summary>
        public ProduktWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor parametryczny okna: <see cref="ProduktWindow" />.
        /// </summary>
        /// <param name="p">Produkt do dodania/zmiany.</param>
        public ProduktWindow(Produkt p) : this()
        {
            this.p = p;
            if (p.CenaHurtownia != 0)
            {
                textBox_cenahurt.Text = p.CenaHurtownia.ToString();
            }
            else
            {
                textBox_cenahurt.Text = "";
            }
            if (p.CenaJednostkowa != 0)
            {
                textBox_cenajedn.Text = p.CenaJednostkowa.ToString();
            }
            else
            {
                textBox_cenajedn.Text = "";
            }
            if (p.StawkaPodatku != 0)
            {
                switch (p.StawkaPodatku.ToString())
                {
                    case "5":
                        comboBox_podatek.SelectedIndex = 2;
                        break;
                    case "8":
                        comboBox_podatek.SelectedIndex = 1;
                        break;
                    case "23":
                        comboBox_podatek.SelectedIndex = 0;
                        break;
                }
            }
            else
            {
                comboBox_podatek.SelectedIndex = 0;
            }
            textBox_nazwa.Text = p.Nazwa;
        }

        /// <summary>
        /// Obłsuga zdarzenia naciśnięcia przycisku zatwierdź.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double cenaHurtownia, cenaJednostkowa;
                //sprawdzanie poprawności wpisanej ceny z hurtowni.
                if (!Double.TryParse(textBox_cenahurt.Text, out cenaHurtownia) || (Decimal.Round((decimal)cenaHurtownia, 2) != (decimal)cenaHurtownia) || cenaHurtownia < 0)
                {
                    MessageBox.Show("Źle wpisana cena z hurtowni!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //sprawdzanie poprawności wpisanej ceny jednostkowej.
                else if (!Double.TryParse(textBox_cenajedn.Text, out cenaJednostkowa) || (Decimal.Round((decimal)cenaJednostkowa, 2) != (decimal)cenaJednostkowa) || cenaJednostkowa < 0)
                {
                    MessageBox.Show("Źle wpisana cena jednostkowa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //ostrzeżenie przed dodaniem produktu z niższą ceną jednostkową od ceny zakupu z hurtowni.
                else if (cenaJednostkowa < cenaHurtownia)
                {
                    MessageBoxResult result = MessageBox.Show("Cena jednostkowa mniejsza niż cena z hurtowni, Kontynuować?", "Ostrzeżenie!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                //jeżeli wszystko jest w porządku.
                else
                {
                    p.CenaHurtownia = cenaHurtownia;
                    p.CenaJednostkowa = cenaJednostkowa;
                }
                //sprawdzanie poprawności wpisanej nazwy.
                if (p.validateNazwa(textBox_nazwa.Text))
                {
                    p.Nazwa = textBox_nazwa.Text;
                }
                else
                {
                    MessageBox.Show("Zła nazwa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //zapis produktu jeżeli spełnia on warunki.
                p.StawkaPodatku = Double.Parse(comboBox_podatek.Text.Split('%')[0]);
                p.CenaBrutto = p.PodajBrutto();
                p.ZmienCene(cenaJednostkowa);
                DialogResult = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Popraw dane!");
                DialogResult = false;
            }
            this.Close();
        }
    }
}
