using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IE_Faktury
{
    public class Produkt
    {
        private string nazwa;
        private double cenaHurtownia;
        private double cenaJednostkowa;
        private double stawkaPodatku;
        private static Dictionary<DateTime, double> zmianyCen;

        public string Nazwa
        {
            get
            {
                return nazwa;
            }

            set
            {
                nazwa = value;
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
    }
}
