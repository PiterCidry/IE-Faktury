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
                textBox_cenahurt.Text = p.CenaHurtownia.ToString();
            else
                textBox_cenahurt.Text = "";
            if (p.CenaJednostkowa != 0)
                textBox_cenajedn.Text = p.CenaJednostkowa.ToString();
            else
                textBox_cenajedn.Text = "";
            textBox_nazwa.Text = p.Nazwa;
        }

        private void button_zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                p.CenaHurtownia = Double.Parse(textBox_cenahurt.Text);
                p.CenaJednostkowa = Double.Parse(textBox_cenajedn.Text);
                p.Nazwa = textBox_nazwa.Text;
                p.StawkaPodatku = Double.Parse(comboBox_podatek.Text.Split('%')[0]);
                p.CenaBrutto = p.PodajBrutto();
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
