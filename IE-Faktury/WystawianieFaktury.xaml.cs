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

        public WystawianieFaktury()
        {
            InitializeComponent();
            textBox_nr.Text = faktura.NumerFaktury;
            textBox_data.Text = faktura.DataWystawienia.ToString("yyyy-MM-dd");
            listView_produkty.ItemsSource = faktura.Produkty.Keys;
        }

        private void button_zmien_Click(object sender, RoutedEventArgs e)
        {
            WyborProduktow wybor = new WyborProduktow(faktura);
            wybor.ShowDialog();
            listView_produkty.Items.Refresh();
        }
    }
}
