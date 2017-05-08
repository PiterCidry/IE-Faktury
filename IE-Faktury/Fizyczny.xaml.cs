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
    /// Interaction logic for Fizyczny.xaml
    /// </summary>
    
    public partial class Fizyczny : Window
    {
        OsobaFizyczna f;

        public Fizyczny()
        {
            InitializeComponent();
        }

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
    
        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                if (f.validateImie(textBox_imie.Text))
                {
                    f.Imie = textBox_imie.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawne imie!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (f.validateNazwisko(textBox_nazwisko.Text))
                {
                    f.Nazwisko = textBox_nazwisko.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawne nazwisko!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (f.validatePesel(textBox_PESEL.Text))
                {
                    f.Pesel = UInt64.Parse(textBox_PESEL.Text);
                }
                else
                {
                    MessageBox.Show("Niepoprawny nr PESEL!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                if (datePicker_dataur.SelectedDate != null)
                {
                    f.DataUrodzenia = datePicker_dataur.SelectedDate.Value;
                }
                else
                {
                    MessageBox.Show("Niepoprawna data urodzenia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (f.validateUlica(textBox_ulica.Text) && f.validateNrDomu(textBox_nrdomu.Text))
                {
                    f.Ulica = textBox_ulica.Text + " " + textBox_nrdomu.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawny adres!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (f.validateKodPocztowy(textBox_kod.Text))
                {
                    f.KodPocztowy = textBox_kod.Text;
                }
                else
                {
                    MessageBox.Show("Niepoprawny kod pocztowy!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

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
   