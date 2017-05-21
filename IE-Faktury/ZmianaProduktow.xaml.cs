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
    /// Okno w którym zmieniamy bazę produktów.
    /// </summary>
    public partial class ZmianaProduktow : Window
    {
        /// <summary>
        /// Instancja obiektu bazy produktów.
        /// </summary>
        BazaProduktow baza = new BazaProduktow();

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmianaProduktow" /> class.
        /// </summary>
        public ZmianaProduktow()
        {
            InitializeComponent();
            //odczyt bazy z pliku
            try
            {
                baza = (BazaProduktow)baza.OdczytajBaze();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
            listView_produkty.ItemsSource = baza.listaProduktow;
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku dodaj (produkt).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku usuń (produkt).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_usun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baza.UsunProdukt(listView_produkty.SelectedIndex);
                listView_produkty.Items.Refresh();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!", "Bład!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        /// Obsługa naciśnięcia przycisku zmień (produkt).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
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
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!", "Bład!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        /// Obsługa naciśnięcia przycisku gotowe.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_gotowe_Click(object sender, RoutedEventArgs e)
        {
            baza.ZapiszBaze();
            this.Close();
        }
    }
}
