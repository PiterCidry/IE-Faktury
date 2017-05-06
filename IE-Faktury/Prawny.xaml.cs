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
        List<OsobaPrawna> lista = new List<OsobaPrawna>();
        public Prawny()
        {
            InitializeComponent();
        }
        public Prawny(OsobaPrawna p) : this()
        {
            this.p = p;
            textBox_nazwa.Text = p.Nazwa;
            textBox_adres.Text = p.Adres;
            textBox_NIP.Text = p.Nip.ToString();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                p.Adres = textBox_adres.Text;
                p.Nip = UInt64.Parse(textBox_NIP.Text);
                p.Nazwa = textBox_nazwa.Text;
                
                DialogResult = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Popraw dane!");
                DialogResult = false;
            }
            lista.Add(p);
            this.Close();
            
        }
    }
}
