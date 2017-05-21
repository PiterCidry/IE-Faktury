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
    /// Interaction logic for AnalizaWindow.xaml
    /// </summary>
    public partial class AnalizaWindow : Window
    {
        public AnalizaWindow()
        {
            InitializeComponent();
        }

        private void button_ogolna_Click(object sender, RoutedEventArgs e)
        {
            SprzedazOgolna sprzedarz = new SprzedazOgolna();
            sprzedarz.ShowDialog();
        }

        private void button_produkt_Click(object sender, RoutedEventArgs e)
        {
            SprzedarzProduktow sprzedarz = new SprzedarzProduktow();
            sprzedarz.ShowDialog();
        }

        private void button_kontrahent_Click(object sender, RoutedEventArgs e)
        {
            SprzedarzKontrahent sprzedarz = new SprzedarzKontrahent();
            sprzedarz.ShowDialog();
        }

        private void button_porownanie_sprzedazy_Click(object sender, RoutedEventArgs e)
        {
            PorownanieSprzedazy porownanie = new PorownanieSprzedazy();
            porownanie.ShowDialog();
        }

        private void button_porownanie_zysku_Click(object sender, RoutedEventArgs e)
        {
            PorownanieZysku porownanie = new PorownanieZysku();
            porownanie.ShowDialog();
        }
    }
}
