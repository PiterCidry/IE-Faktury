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

        public WyborProduktow()
        {
            InitializeComponent();
        }

        public WyborProduktow(Faktura faktura) : this()
        {
            this.f = faktura;
            if (File.Exists("../../BazaProduktow.xml"))
            {
                baza = (BazaProduktow)baza.OdczytajBaze();
            }
            comboBox_produkt.ItemsSource = baza.listaProduktow;
        }

        private void button_dodaj_Click(object sender, RoutedEventArgs e)
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
            this.Close();
        }
    }
}
