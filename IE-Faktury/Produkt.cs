using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace IE_Faktury
{
    [Serializable]
    public class Produkt
    {
        private string nazwa;
        private double cenaHurtownia;
        private double cenaJednostkowa;
        private double stawkaPodatku;
        private double cenaBrutto;
        private static Dictionary<DateTime, double> zmianyCen;

        public string Nazwa
        {
            get
            {
                return nazwa;
            }
            set
            {
                if (Regex.IsMatch(value, @"^\S[a-zA-Z0-9]{2,30}"))
                {
                    nazwa = value;
                }
                else
                {
                    MessageBox.Show("Zła nazwa!");
                    throw new FormatException();
                }
            }
        }

        public double CenaHurtownia
        {
            get
            {
                return cenaHurtownia;
            }

            set
            {
                cenaHurtownia = value;
            }
        }

        public double CenaJednostkowa
        {
            get
            {
                return cenaJednostkowa;
            }

            set
            {
                cenaJednostkowa = value;
            }
        }

        public double StawkaPodatku
        {
            get
            {
                return stawkaPodatku;
            }

            set
            {
                stawkaPodatku = value;
            }
        }

        public double CenaBrutto
        {
            get
            {
                return cenaBrutto;
            }
            set
            {
                cenaBrutto = value;
            }
        }

        public Produkt()
        {
            this.nazwa = "";
            this.cenaHurtownia = 0;
            this.cenaJednostkowa = 0;
            this.stawkaPodatku = 0;
        }
        
        public double PodajBrutto()
        {
            return (this.cenaJednostkowa * (1 + (this.stawkaPodatku/100)));
        }

        public override string ToString()
        {
            return this.Nazwa;
        }
    }
}
