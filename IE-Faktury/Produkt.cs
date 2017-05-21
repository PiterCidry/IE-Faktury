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
    /// <summary>
    /// Struktura pozwalająca na serializację oszukanego słownika.
    /// </summary>
    /// <typeparam name="K">Klucz</typeparam>
    /// <typeparam name="V">Wartość</typeparam>
    [Serializable]
    [XmlType]
    public struct KeyValuePair<K, V>
    {
        /// <summary>
        /// Udostępnianie lub zmiana klucza.
        /// </summary>
        /// <value>
        /// Klucz.
        /// </value>
        public K Key
        { get; set; }

        /// <summary>
        /// Udostępnianie lub zmiana wartości.
        /// </summary>
        /// <value>
        /// Wartość.
        /// </value>
        public V Value
        { get; set; }
    }

    /// <summary>
    /// Klasa produktu.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Produkt
    {
        /// <summary>
        /// Nazwa produktu.
        /// </summary>
        private string nazwa;
        /// <summary>
        /// Cena brutto zakupu w hurtowni artykułów spożywczych.
        /// </summary>
        private double cenaHurtownia;
        /// <summary>
        /// Cena jednostkowa netto sprzedaży.
        /// </summary>
        private double cenaJednostkowa;
        /// <summary>
        /// Stawka podatku dla produktu.
        /// </summary>
        private double stawkaPodatku;
        /// <summary>
        /// Cena jednostkowa brutto sprzedaży.
        /// </summary>
        private double cenaBrutto;
        /// <summary>
        /// Lista ze zmianami cen produktu w zależności od daty.
        /// </summary>
        private List<KeyValuePair<DateTime, double>> listaZmian;

        /// <summary>
        /// Udostępnianie lub zmiana nazwy produktu.
        /// </summary>
        /// <value>
        /// Nazwa produktu.
        /// </value>
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

        /// <summary>
        /// Udostępnianie lub zmiana ceny z hurtowni.
        /// </summary>
        /// <value>
        /// Cena z hurtowni.
        /// </value>
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

        /// <summary>
        /// Udostępnianie lub zmiana ceny jednostkowej.
        /// </summary>
        /// <value>
        /// Cena jednostkowa.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana stawki podatku.
        /// </summary>
        /// <value>
        /// Stawka podatku.
        /// </value>
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

        /// <summary>
        /// Udostępnianie lub zmiana ceny brutto.
        /// </summary>
        /// <value>
        /// Cena brutto.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana listy ze zmianami cen.
        /// </summary>
        /// <value>
        /// Lista zmian cen.
        /// </value>
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

        /// <summary>
        /// Konstruktor domyślny klasy <see cref="Produkt" />.
        /// </summary>
        public Produkt()
        {
            this.nazwa = "";
            this.cenaHurtownia = 0;
            this.cenaJednostkowa = 0;
            this.stawkaPodatku = 0;
            this.listaZmian = new List<KeyValuePair<DateTime, double>>();
        }

        /// <summary>
        /// Metoda obliczająca cenę brutto produktu.
        /// </summary>
        /// <returns>Cena brutto produktu.</returns>
        public double PodajBrutto()
        {
            return (this.cenaJednostkowa * (1 + (this.stawkaPodatku / 100)));
        }

        /// <summary>
        /// Metoda zmieniająca cenę produktu
        /// </summary>
        /// <param name="nowa">Nowa cena.</param>
        public void ZmienCene(double nowa)
        {
            KeyValuePair<DateTime, double> kvp = new KeyValuePair<DateTime, double>();
            kvp.Key = DateTime.Now;
            kvp.Value = nowa;
            ListaZmian.Add(kvp);
        }

        /// <summary>
        /// Metoda zwracająca  <see cref="System.String" /> reprezentujący produkt.
        /// </summary>
        /// <returns>
        /// <see cref="System.String" /> reprezentujący produkt.
        /// </returns>
        public override string ToString()
        {
            return this.Nazwa;
        }

        /// <summary>
        /// Metoda walidacji nazwy wprowadzonej przez użytkownika.
        /// </summary>
        /// <param name="n">Nazwa</param>
        /// <returns>True jeśli nazwa jest poprawna, false jeżeli nie jest.</returns>
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
