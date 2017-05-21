using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Okno dodawania osoby fizycznej.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class Fizyczny : Window
    {
        /// <summary>
        /// Osoba fizyczna.
        /// </summary>
        OsobaFizyczna f;

        /// <summary>
        /// Konstruktor domyślny okna: <see cref="Fizyczny"/>.
        /// </summary>
        public Fizyczny()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor parametryczny okna: <see cref="Fizyczny"/>.
        /// </summary>
        /// <param name="f">Osoba fizyczna</param>
        public Fizyczny(OsobaFizyczna f) : this()
        {
            this.f = f;
            textBox_imie.Text = f.Imie;
            textBox_nazwisko.Text = f.Nazwisko;
            textBox_ulica.Text = f.Ulica;
            textBox_kod.Text = "";
            textBox_miasto.Text = f.Miasto;
            textBox_PESEL.Text = "";
            datePicker_dataur.DisplayDateEnd = DateTime.Today;
        }

        /// <summary>
        /// Obsługa zdarzenia dodania osoby fizycznej.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                //walidacja imienia.
                if (f.validateImie(textBox_imie.Text))
                {
                    f.Imie = textBox_imie.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawne imie!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja nazwiska.
                if (f.validateNazwisko(textBox_nazwisko.Text))
                {
                    f.Nazwisko = textBox_nazwisko.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawne nazwisko!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja nr pesel.
                if (f.validatePesel(textBox_PESEL.Text))
                {
                    f.Pesel = UInt64.Parse(textBox_PESEL.Text);
                }
                else
                {
                    MessageBox.Show("Niepoprawny nr PESEL!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja daty urodzenia.
                if (datePicker_dataur.SelectedDate != null)
                {
                    f.DataUrodzenia = datePicker_dataur.SelectedDate.Value;
                }
                else
                {
                    MessageBox.Show("Niepoprawna data urodzenia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja ulicy i nr domu.
                if (f.validateUlica(textBox_ulica.Text) && f.validateNrDomu(textBox_nrdomu.Text))
                {
                    f.Ulica = textBox_ulica.Text + " " + textBox_nrdomu.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawny adres!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja kodu pocztowego.
                if (f.validateKodPocztowy(textBox_kod.Text))
                {
                    f.KodPocztowy = textBox_kod.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawny kod pocztowy!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //walidacja miasta.
                if (f.validateMiasto(textBox_miasto.Text))
                {
                    f.Miasto = textBox_miasto.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawa nazwa miasta!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
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
   