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
    /// Interaction logic for ZmianaProduktow.xaml
    /// </summary>
    public partial class ZmianaProduktow : Window
    {
        List<Produkt> listaProduktow = new List<Produkt>();

        public ZmianaProduktow()
        {
            InitializeComponent();
        }

        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            Produkt p = new Produkt();
            ProduktWindow produkt = new ProduktWindow(p);
            produkt.Show();
            listaProduktow.Add(p);
            listBox_produkty.ItemsSource = this.listaProduktow;
        }
    }
}
