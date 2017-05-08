using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace IE_Faktury
{
    [Serializable]
    [XmlType(TypeName = "ZmianyCen")]
    public struct KeyValuePair<K, V>
    {
        public K Key
        { get; set; }

        public V Value
        { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Produkt
    {
        private string nazwa;
        private double cenaHurtownia;
        private double cenaJednostkowa;
        private double stawkaPodatku;
        private double cenaBrutto;
        private List<KeyValuePair<DateTime, double>> listaZmian;

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

        public List<KeyValuePair<DateTime, double>> ListaZmian
        {
            get
            {
                return listaZmian;
            }

            set
            {
                listaZmian = value;
            }
        }

        public Produkt()
        {
            this.nazwa = "";
            this.cenaHurtownia = 0;
            this.cenaJednostkowa = 0;
            this.stawkaPodatku = 0;
            this.listaZmian = new List<KeyValuePair<DateTime, double>>();
        }
        
        public double PodajBrutto()
        {
            return (this.cenaJednostkowa * (1 + (this.stawkaPodatku/100)));
        }

        public void ZmienCene(double nowa)
        {
            KeyValuePair<DateTime, double> kvp = new KeyValuePair<DateTime, double>();
            kvp.Key = DateTime.Now;
            kvp.Value = nowa;
            ListaZmian.Add(kvp);
        }

        public override string ToString()
        {
            return this.Nazwa;
        }

        public bool validateNazwa(string n)
        {
            Regex rgx_nazwa = new Regex(@"^[\p{L}0-9][\p{L}0-9\s]{0,28}[\p{L}0-9]$");
            Match match_nazwa = rgx_nazwa.Match(n);
            if (match_nazwa.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
