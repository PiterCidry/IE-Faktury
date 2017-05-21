using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Okno do wyboru produktów do faktury.
    /// </summary>
    public partial class WyborProduktow : Window
    {
        /// <summary>
        /// Baza produktów.
        /// </summary>
        BazaProduktow baza = new BazaProduktow();
        /// <summary>
        /// Faktura do której dodany lub w której zmieniony będzie produkt.
        /// </summary>
        Faktura f;
        /// <summary>
        /// Produkt, który ma być dodany albo zmieniony.
        /// </summary>
        Produkt p;

        /// <summary>
        /// Konstruktor domyślny okna: <see cref="WyborProduktow" />.
        /// </summary>
        public WyborProduktow()
        {
            InitializeComponent();
            //odczyt bazy z pliku
            if (File.Exists("../../BazaProduktow.xml"))
            {
                baza = (BazaProduktow)baza.OdczytajBaze();
            }
            comboBox_produkt.ItemsSource = baza.listaProduktow;
        }

        /// <summary>
        /// Konstruktor parametryczny okna: <see cref="WyborProduktow" />. Używany w przypadku dodawania nowego produktu do faktury.
        /// </summary>
        /// <param name="faktura">Faktura w której dodany zostanie produkt.</param>
        public WyborProduktow(Faktura faktura) : this()
        {
            this.f = faktura;
            button_dodaj.Visibility = Visibility.Visible;
            button_zmien.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Konstruktor parametryczny okna: <see cref="WyborProduktow" />. Używany w przypadku zmainy produktu na fakturze.
        /// </summary>
        /// <param name="faktura">Faktura w której zmieniony będzie produkt.</param>
        /// <param name="produkt">Produkt do zmiany.</param>
        /// <param name="ilosc">Stara ilość produktu.</param>
        public WyborProduktow(Faktura faktura, Produkt produkt, int ilosc) : this(faktura)
        {
            this.p = produkt;
            foreach (Produkt item in comboBox_produkt.Items)
            {
                if (item.Nazwa.ToString() == produkt.Nazwa)
                {
                    comboBox_produkt.SelectedValue = item;
                }
            }
            textBox_ilosc.Text = ilosc.ToString();
            comboBox_produkt.IsEnabled = false;
            button_dodaj.Visibility = Visibility.Hidden;
            button_zmien.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku dodaj (produkt).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            //sprawdzanie czy faktura już zawiera dany produkt.
            foreach (var item in f.Produkty)
            {
                if (item.Key.ToString() == comboBox_produkt.SelectedItem.ToString())
                {
                    MessageBox.Show("Faktura zawiera już ten produkt!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    ok = false;
                    break;
                }
            }
            //jeżeli nie zawiera.
            if (ok == true)
            {
                int ilosc;
                //sprawdzanie poprawności wprowadzonej ilości.
                if (!Int32.TryParse(textBox_ilosc.Text, out ilosc) || ilosc <= 0)
                {
                    MessageBox.Show("Ilosc wprowadzona niepoprawnie!", "Złe dane!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //sprawdzanie czy wybrany został produkt.
                else if (String.IsNullOrEmpty(comboBox_produkt.Text))
                {
                    MessageBox.Show("Nie wybrano żadnego produktu!", "Złe dane!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //jeżeli wszystko jest w porządku.
                else
                {
                    f.Produkty.Add(comboBox_produkt.SelectedItem as Produkt, Int32.Parse(textBox_ilosc.Text));
                }
            }
            this.Close();
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku zmień (produkt)
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_zmien_Click(object sender, RoutedEventArgs e)
        {
            int ilosc;
            //sprawdzanie poprawności ilości.
            if (!Int32.TryParse(textBox_ilosc.Text, out ilosc) || ilosc <= 0)
            {
                MessageBox.Show("Ilosc wprowadzona niepoprawnie!", "Złe dane!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //zmiana produktu, jeżeli jest poprawnie wpisana ilość.
            else
            {
                foreach (var item in f.Produkty)
                {
                    if (item.Key.ToString() == comboBox_produkt.SelectedItem.ToString())
                    {
                        f.Produkty[item.Key] = Int32.Parse(textBox_ilosc.Text);
                        break;
                    }
                }
            }
            this.Close();
        }
    }
}
