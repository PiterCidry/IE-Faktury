using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ProduktWindow.xaml
    /// </summary>
    public partial class ProduktWindow : Window
    {
        Produkt p;

        public ProduktWindow()
        {
            InitializeComponent();
        }

        public ProduktWindow(Produkt p):this()
        {
            this.p = p;
            if (p.CenaHurtownia != 0)
            {
                textBox_cenahurt.Text = p.CenaHurtownia.ToString();
            }
            else
            {
                textBox_cenahurt.Text = "";
            }
            if (p.CenaJednostkowa != 0)
            {
                textBox_cenajedn.Text = p.CenaJednostkowa.ToString();
            }
            else
            {
                textBox_cenajedn.Text = "";
            }
            if (p.StawkaPodatku != 0)
            {
                switch (p.StawkaPodatku.ToString())
                {
                    case "5":
                        comboBox_podatek.SelectedIndex = 2;
                        break;
                    case "8":
                        comboBox_podatek.SelectedIndex = 1;
                        break;
                    case "23":
                        comboBox_podatek.SelectedIndex = 0;
                        break;
                }       
            }
            else
            {
                comboBox_podatek.SelectedIndex = 0;
            }
            textBox_nazwa.Text = p.Nazwa;
        }

        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double cenaHurtownia, cenaJednostkowa;
                if(!Double.TryParse(textBox_cenahurt.Text, out cenaHurtownia) || (Decimal.Round((decimal)cenaHurtownia, 2) != (decimal)cenaHurtownia) ||  cenaHurtownia < 0)
                {
                    MessageBox.Show("Źle wpisana cena z hurtowni!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if(!Double.TryParse(textBox_cenajedn.Text, out cenaJednostkowa) || (Decimal.Round((decimal)cenaJednostkowa, 2) != (decimal)cenaJednostkowa) || cenaJednostkowa < 0)
                {
                    MessageBox.Show("Źle wpisana cena jednostkowa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if(cenaJednostkowa < cenaHurtownia)
                {
                    MessageBoxResult result = MessageBox.Show("Cena jednostkowa mniejsza niż cena z hurtowni, Kontynuować?", "Ostrzeżenie!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                else 
                {
                    p.CenaHurtownia = cenaHurtownia;
                    p.CenaJednostkowa = cenaJednostkowa;
                }

                Regex rgx_nazwa = new Regex(@"^[a-zA-Z0-9][a-zA-Z0-9]{0,28}[a-zA-Z0-9]$");
                Match match_nazwa = rgx_nazwa.Match(textBox_nazwa.Text);
                if (match_nazwa.Success)
                {
                    p.Nazwa = textBox_nazwa.Text;
                }
                else
                {
                    MessageBox.Show("Zła nazwa!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                p.StawkaPodatku = Double.Parse(comboBox_podatek.Text.Split('%')[0]);
                p.CenaBrutto = p.PodajBrutto();
                p.ZmienCene(cenaJednostkowa);
                DialogResult = true;
            }
            catch(FormatException)
            {
                MessageBox.Show("Popraw dane!");
                DialogResult = false;
            }
            this.Close();
        }
    }
}
