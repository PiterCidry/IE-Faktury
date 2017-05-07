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
    /// Interaction logic for Prawny.xaml
    /// </summary>
    public partial class Prawny : Window
    {
        OsobaPrawna p;

        public Prawny()
        {
            InitializeComponent();
        }

        public Prawny(OsobaPrawna p) : this()
        {
            this.p = p;
            textBox_nazwa.Text = p.Nazwa;
            textBox_ulica.Text = p.Ulica;
            textBox_kod.Text = "";
            textBox_miasto.Text = p.Miasto;
            textBox_NIP.Text = "";
        }

        private void button_dodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                p.Ulica = textBox_ulica.Text;
                p.KodPocztowy = textBox_kod.Text;
                p.Miasto = textBox_miasto.Text;
                p.Nip = UInt64.Parse(textBox_NIP.Text);
                p.Nazwa = textBox_nazwa.Text;
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
