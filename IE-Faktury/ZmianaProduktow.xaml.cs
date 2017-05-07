using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ZmianaProduktow.xaml
    /// </summary>
    public partial class ZmianaProduktow : Window
    {
        BazaProduktow baza = new BazaProduktow();

        public ZmianaProduktow()
        {
            InitializeComponent();
            try
            {
                baza = (BazaProduktow)baza.OdczytajBaze();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
            listView_produkty.ItemsSource = baza.listaProduktow;
        }

        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            Produkt p = new Produkt();
            ProduktWindow produkt = new ProduktWindow(p);
            produkt.ShowDialog();
            if (produkt.DialogResult != false)
            {
                baza.DodajProdukt(p);
            }
            listView_produkty.Items.Refresh();
        }

        private void button_usun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baza.UsunProdukt(listView_produkty.SelectedIndex);
                listView_produkty.Items.Refresh();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!");
            }
        }

        private void button_zmien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produkt p = baza.PodajProdukt(listView_produkty.SelectedIndex);
                ProduktWindow produkt = new ProduktWindow(p);
                produkt.ShowDialog();
                if (produkt.DialogResult != false)
                {
                    baza.ZmienProdukt(baza.PodajProdukt(listView_produkty.SelectedIndex), p);
                }
                listView_produkty.Items.Refresh();
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!");
            }
        }

        private void button_gotowe_Click(object sender, RoutedEventArgs e)
        {
            baza.ZapiszBaze();
            this.Close();
        }
    }
}
