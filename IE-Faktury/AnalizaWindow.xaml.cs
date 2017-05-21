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
    /// Okno z wyborem jaką analizę chcę się przeprowadzić.
    /// </summary>
    public partial class AnalizaWindow : Window
    {
        /// <summary>
        /// Kontruktor domyślny okna: <see cref="AnalizaWindow"/>.
        /// </summary>
        public AnalizaWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku wyświetl statystyki sprzedaży ogólnej.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_ogolna_Click(object sender, RoutedEventArgs e)
        {
            SprzedazOgolna sprzedarz = new SprzedazOgolna();
            sprzedarz.ShowDialog();
        }

        /// <summary>
        /// H Obsługa zdarzenia naciśnięcia przycisku wyświetl statystyki produktu.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_produkt_Click(object sender, RoutedEventArgs e)
        {
            SprzedarzProduktow sprzedarz = new SprzedarzProduktow();
            sprzedarz.ShowDialog();
        }

        /// <summary>
        ///  Obsługa zdarzenia naciśnięcia przycisku wyświetl statystyki kontrahenta.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_kontrahent_Click(object sender, RoutedEventArgs e)
        {
            SprzedarzKontrahent sprzedarz = new SprzedarzKontrahent();
            sprzedarz.ShowDialog();
        }

        /// <summary>
        ///  Obsługa zdarzenia naciśnięcia przycisku porównanie sprzedaży dwóch produktów.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_porownanie_sprzedazy_Click(object sender, RoutedEventArgs e)
        {
            PorownanieSprzedazy porownanie = new PorownanieSprzedazy();
            porownanie.ShowDialog();
        }

        /// <summary>
        ///  Obsługa zdarzenia naciśnięcia przycisku prównanie zysku na dwóch produktach.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_porownanie_zysku_Click(object sender, RoutedEventArgs e)
        {
            PorownanieZysku porownanie = new PorownanieZysku();
            porownanie.ShowDialog();
        }
    }
}
