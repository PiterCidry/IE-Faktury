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
    /// Interaction logic for WyborProduktow.xaml
    /// </summary>
    public partial class WyborProduktow : Window
    {
        BazaProduktow baza = new BazaProduktow();
        Faktura f;
        Produkt p;

        public WyborProduktow()
        {
            InitializeComponent();
            if (File.Exists("../../BazaProduktow.xml"))
            {
                baza = (BazaProduktow)baza.OdczytajBaze();
            }
            comboBox_produkt.ItemsSource = baza.listaProduktow;
        }

        public WyborProduktow(Faktura faktura) : this()
        {
            this.f = faktura;
            button_dodaj.Visibility = Visibility.Visible;
            button_zmien.Visibility = Visibility.Hidden;
        }

        public WyborProduktow(Faktura faktura, Produkt produkt, int ilosc) : this(faktura)
        {
            this.p = produkt;
            foreach(Produkt item in comboBox_produkt.Items)
            {
                if(item.Nazwa.ToString() == produkt.Nazwa)
                {
                    comboBox_produkt.SelectedValue = item;
                }
            }
            textBox_ilosc.Text = ilosc.ToString();
            comboBox_produkt.IsEnabled = false;
            button_dodaj.Visibility = Visibility.Hidden;
            button_zmien.Visibility = Visibility.Visible;
        }

        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (var item in f.Produkty)
            {
                if (item.Key.ToString() == comboBox_produkt.SelectedItem.ToString())
                {
                    MessageBox.Show("Faktura zawiera już ten produkt!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    ok = false;
                    break;
                }
            }
            if (ok == true)
            {
                int ilosc;
                if (!Int32.TryParse(textBox_ilosc.Text, out ilosc) || ilosc <= 0)
                {
                    MessageBox.Show("Ilosc wprowadzona niepoprawnie!", "Złe dane!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (String.IsNullOrEmpty(comboBox_produkt.Text))
                {
                    MessageBox.Show("Nie wybrano żadnego produktu!", "Złe dane!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    f.Produkty.Add(comboBox_produkt.SelectedItem as Produkt, Int32.Parse(textBox_ilosc.Text));
                }
            }
            this.Close();
        }

        private void button_zmien_Click(object sender, RoutedEventArgs e)
        {
            int ilosc;
            if (!Int32.TryParse(textBox_ilosc.Text, out ilosc) || ilosc <= 0)
            {
                MessageBox.Show("Ilosc wprowadzona niepoprawnie!", "Złe dane!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                foreach (var item in f.Produkty)
                {
                    if(item.Key.ToString() == comboBox_produkt.SelectedItem.ToString())
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
