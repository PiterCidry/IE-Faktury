using System;
using System.Collections.Generic;
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
    /// Interaction logic for WystawianieFaktury.xaml
    /// </summary>
    public partial class WystawianieFaktury : Window
    {
        Faktura faktura = new Faktura();
        BazaOdbiorcow baza = new BazaOdbiorcow();
        Faktura f;

        public WystawianieFaktury()
        {
            InitializeComponent();
            textBox_nr.Text = faktura.NumerFaktury;
            textBox_data.Text = faktura.DataWystawienia.ToString("yyyy-MM-dd");
            dataGrid_produkty.ItemsSource = faktura.Produkty;
                    }
        public WystawianieFaktury(Faktura faktura)
        {
            this.f = faktura;
            baza = (BazaOdbiorcow)baza.OdczytajBaze();
           /* if (radioButton_prawny.IsChecked == true)
            {
                comboBox_odbiorca.ItemsSource = baza.listaPrawnych;
            }
            if (radioButton_fizyczny.IsChecked == true)
            {

                comboBox_odbiorca.ItemsSource = baza.listaFizycznych;
            }*/

           
        }
        private void button_dodajProd_Click(object sender, RoutedEventArgs e)
        {
            WyborProduktow wybor = new WyborProduktow(faktura);
            wybor.ShowDialog();
            dataGrid_produkty.Items.Refresh();
        }

        private void button_dodajOdbiorce_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton_prawny.IsChecked==true)
            {
                Prawny p1 = new Prawny();
                p1.ShowDialog();
            }
            if (radioButton_fizyczny.IsChecked==true)
            {
                Fizyczny f1 = new Fizyczny();
                f1.ShowDialog();
            }
        }

       private void comboBox_odbiorca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
