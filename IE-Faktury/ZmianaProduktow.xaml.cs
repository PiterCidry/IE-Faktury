﻿using System;
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
    /// Interaction logic for ZmianaProduktow.xaml
    /// </summary>
    public partial class ZmianaProduktow : Window
    {
        BazaProduktow baza = new BazaProduktow();

        public ZmianaProduktow()
        {
            InitializeComponent();
            baza = (BazaProduktow)baza.OdczytajBaze();
            listView_produkty.ItemsSource = baza.listaProduktow;
        }

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

        private void button_usun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baza.UsunProdukt(listView_produkty.SelectedIndex);
                listView_produkty.Items.Refresh();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!");
            }
        }

        private void button_zmien_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProduktWindow produkt = new ProduktWindow(baza.PodajProdukt(listView_produkty.SelectedIndex));
                produkt.ShowDialog();
                listView_produkty.Items.Refresh();
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!");
            }
        }

        private void button_gotowe_Click(object sender, RoutedEventArgs e)
        {
            baza.ZapiszBaze();
            this.Close();
        }
    }
}
