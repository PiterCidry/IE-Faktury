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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IE_Faktury
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_wystaw_Click(object sender, RoutedEventArgs e)
        {
            WystawianieFaktury wystawianie = new WystawianieFaktury();
            wystawianie.Show();
        }

        private void button_produkty_Click(object sender, RoutedEventArgs e)
        {
            ZmianaProduktow zmiana = new ZmianaProduktow();
            zmiana.Show();
        }
    }
}
