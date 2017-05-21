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
    /// Okno dodawania osoby prawnej.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Prawny : Window
    {
        /// <summary>
        /// Osoba prawna.
        /// </summary>
        OsobaPrawna p;

        /// <summary>
        /// Kontruktor domyślny okna: <see cref="Prawny"/>.
        /// </summary>
        public Prawny()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Kontruktor parametryczny okna: <see cref="Prawny"/>.
        /// </summary>
        /// <param name="p">Osoba prawna</param>
        public Prawny(OsobaPrawna p) : this()
        {
            this.p = p;
            textBox_nazwa.Text = p.Nazwa;
            textBox_ulica.Text = p.Ulica;
            textBox_kod.Text = "";
            textBox_miasto.Text = p.Miasto;
            textBox_NIP.Text = "";
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku dodaj.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //walidacja nazwy.
                if (p.validateNazwa(textBox_nazwa.Text))
                {
                    p.Nazwa = textBox_nazwa.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawna nazwa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja nr nip.
                if (p.validateNip(textBox_NIP.Text))
                {
                    p.Nip = UInt64.Parse(textBox_NIP.Text);
                }
                else
                {
                    MessageBox.Show("Niepoprawny nr NIP!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja ulicy i nr domu.
                if (p.validateUlica(textBox_ulica.Text) && p.validateNrDomu(textBox_nrDomu.Text))
                {
                    p.Ulica = textBox_ulica.Text + " " + textBox_nrDomu.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawna nazwa ulicy!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja kodu pocztowego.
                if (p.validateKodPocztowy(textBox_kod.Text))
                {
                    p.KodPocztowy = textBox_kod.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawny kod pocztowy!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja nazwy miasta.
                if (p.validateMiasto(textBox_miasto.Text))
                {
                    p.Miasto = textBox_miasto.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawna nazwa miasta!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DialogResult = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Popraw dane!");
                DialogResult = false;
            }
            this.Close();
        }
    }
}
