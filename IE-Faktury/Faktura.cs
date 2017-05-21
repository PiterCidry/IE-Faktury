using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Threading;

namespace IE_Faktury
{

    /// <summary>
    /// Klasa faktury.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Faktura
    {
        /// <summary>
        /// Inkrementowany numer faktury.
        /// </summary>
        private static uint inkrementowany = 1;
        /// <summary>
        /// Numer faktury.
        /// </summary>
        private string numerFaktury;
        /// <summary>
        /// Data wystawienia faktury.
        /// </summary>
        private DateTime dataWystawienia;
        /// <summary>
        /// Odbiorca fizyczny faktury.
        /// </summary>
        private OsobaFizyczna odbiorcaFizyczny;
        /// <summary>
        /// Odbiorca prawny faktury.
        /// </summary>
        private OsobaPrawna odbiorcaPrawny;
        /// <summary>
        /// Słownik z produktami i ilościami.
        /// </summary>
        private Dictionary<Produkt, int> produkty;
        /// <summary>
        /// Lista z produktami i ilościami.
        /// </summary>
        private List<IE_Faktury.KeyValuePair<Produkt, int>> produktyList;
        /// <summary>
        /// Cena razem faktury.
        /// </summary>
        private double razem = 0.0;


        /// <summary>
        /// Udostępnianie lub zmiana numeru faktury.
        /// </summary>
        /// <value>
        /// Numer faktury.
        /// </value>
        public string NumerFaktury
        {
            get
            {
                return numerFaktury;
            }

            set
            {
                numerFaktury = value;
            }
        }

        /// <summary>
        /// Udostępnianie lub zmiana datę wystawienia.
        /// </summary>
        /// <value>
        /// Data wystawienia.
        /// </value>
        public DateTime DataWystawienia
        {
            get
            {
                return dataWystawienia.Date;
            }

            set
            {
                dataWystawienia = value;
            }
        }

        /// <summary>
        /// Udostępnianie lub zmiana odbiorcy fizycznego.
        /// </summary>
        /// <value>
        /// Odbiorca fizyczny.
        /// </value>
        public OsobaFizyczna OdbiorcaFizyczny
        {
            get
            {
                return odbiorcaFizyczny;
            }

            set
            {
                odbiorcaFizyczny = value;
            }
        }

        /// <summary>
        /// Udostępnianie lub zmiana odbiorcy prawnego.
        /// </summary>
        /// <value>
        /// Odbiorca prawny.
        /// </value>
        public OsobaPrawna OdbiorcaPrawny
        {
            get
            {
                return odbiorcaPrawny;
            }

            set
            {
                odbiorcaPrawny = value;
            }
        }
        /// <summary>
        /// Udostępnianie lub zmiana słownika z produktami.
        /// </summary>
        /// <value>
        /// Słownik z produktami.
        /// </value>
        [XmlIgnore]
        public Dictionary<Produkt, int> Produkty
        {
            get
            {
                return produkty;
            }

            set
            {
                produkty = value;
            }
        }

        /// <summary>
        /// Udostępnianie lub zmiana listy z produktami.
        /// </summary>
        /// <value>
        /// Lista z produktami.
        /// </value>
        public List<KeyValuePair<Produkt, int>> ProduktyList
        {
            get
            {
                return produktyList;
            }

            set
            {
                produktyList = value;
            }
        }

        /// <summary>
        /// Udostępnianie lub zmiana ceny razem.
        /// </summary>
        /// <value>
        /// Cena razem.
        /// </value>
        public double Razem
        {
            get
            {
                return razem;
            }

            set
            {
                razem = value;
            }
        }

        /// <summary>
        /// Kontruktor domyślny klasy <see cref="Faktura"/>.
        /// </summary>
        public Faktura()
        {
            //ustawianie numeru faktury
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("../../BazaFaktur.xml");
                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("//listaFaktur/Faktura[last()]/NumerFaktury");
                foreach (XmlNode node in nodes)
                {
                    string[] numerki = node.InnerText.ToString().Split('/');
                    inkrementowany = UInt32.Parse(numerki[0]);
                }
                inkrementowany++;
            }
            catch(System.IO.FileNotFoundException fnfe)
            {
                Debug.WriteLine(fnfe.InnerException);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(inkrementowany).Append(@"/").Append(DateTime.Today.Year.ToString());
            this.numerFaktury = sb.ToString();
            this.dataWystawienia = DateTime.Now;
            this.produkty = new Dictionary<Produkt, int>();
            this.produktyList = new List<KeyValuePair<Produkt, int>>();
        }

        /// <summary>
        /// Metoda licząca cenę razem faktury.
        /// </summary>
        /// <returns>Łączna cena.</returns>
        public double podajRazem()
        {
            double razem = 0.0;
            foreach (System.Collections.Generic.KeyValuePair<Produkt, int> kvp in this.Produkty)
            {
                razem += kvp.Key.CenaBrutto * kvp.Value;
            }
            if (this.OdbiorcaFizyczny != null)
            {
                razem = razem * this.OdbiorcaFizyczny.Rabat;
            }
            if (this.OdbiorcaPrawny != null)
            {
                razem = razem * this.OdbiorcaPrawny.Rabat;
            }
            this.Razem = razem;
            return razem;
        }

        /// <summary>
        /// Metoda zwracająca cenę łączną na podstawie rabatu.
        /// </summary>
        /// <param name="rabat">Rabat.</param>
        /// <returns>Cena razem łącznie z rabatem.</returns>
        public double podajRazem(string rabat)
        {
            double razem = 0.0;
            if (!string.IsNullOrEmpty(rabat))
            {
                string[] rabaty = rabat.Split('%');
                double rabacik = Double.Parse(rabaty[0]);
                foreach (System.Collections.Generic.KeyValuePair<Produkt, int> kvp in this.Produkty)
                {
                    razem += kvp.Key.CenaBrutto * kvp.Value;
                }
                razem = razem * (1 - (rabacik / 100));
            }
            return razem;
        }
    }
}

