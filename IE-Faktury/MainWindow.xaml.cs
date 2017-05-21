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
    /// Startowe okno programu.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda wywoływana przy naciśnięciu przycisku "Wystaw", wyświetla okno do wystawiania faktury.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_wystaw_Click(object sender, RoutedEventArgs e)
        {
            WystawianieFaktury wystawianie = new WystawianieFaktury();
            wystawianie.ShowDialog();
        }

        /// <summary>
        /// Metoda wywoływana przy naciśnięciu przycisku "Produkty", wyświetla okno do zmiany bazy produktów.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_produkty_Click(object sender, RoutedEventArgs e)
        {
            ZmianaProduktow zmiana = new ZmianaProduktow();
            zmiana.ShowDialog();
        }

        /// <summary>
        /// Metoda wywoływana przy naciśnięciu przycisku "Analiza", wyświetla okno do analizy faktur.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_analiza_Click(object sender, RoutedEventArgs e)
        {
            AnalizaWindow analiza = new AnalizaWindow();
            analiza.ShowDialog();
        }
    }
}
