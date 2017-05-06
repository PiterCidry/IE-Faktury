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
    /// Interaction logic for Fizyczny.xaml
    /// </summary>
    
    public partial class Fizyczny : Window
    {
        OsobaFizyczna f;
        List<OsobaFizyczna> lista = new List<OsobaFizyczna>();

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
            textBox_kod.Text = f.KodPocztowy.ToString();
            textBox_miasto.Text = f.Miasto;
            textBox_PESEL.Text = f.Pesel.ToString();
        }
    
        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                f.Pesel = UInt64.Parse(textBox_PESEL.Text);
                f.Imie = textBox_imie.Text;
                f.Nazwisko = textBox_nazwisko.Text;
                f.DataUrodzenia = DateTime.Parse(textBox_data.Text);
                f.Ulica = textBox_ulica.Text;
                f.KodPocztowy = Int16.Parse(textBox_kod.Text);
                f.Miasto = textBox_miasto.Text;

                DialogResult = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Popraw dane!");
                DialogResult = false;
            }
            lista.Add(f);
            this.Close();
        }

        private void textBox_adres_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
    }
    }
   