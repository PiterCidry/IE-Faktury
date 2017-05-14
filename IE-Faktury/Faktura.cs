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

    [Serializable]
    [DataContract]
    public class Faktura
    {
        private static uint inkrementowany = 1;
        private string numerFaktury;
        private DateTime dataWystawienia;
        private OsobaFizyczna odbiorcaFizyczny;
        private OsobaPrawna odbiorcaPrawny;
        private Dictionary<Produkt, int> produkty;
        private List<IE_Faktury.KeyValuePair<Produkt, int>> produktyList;
        private double razem = 0.0;

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

        public Faktura()
        {
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
            sb.Append(inkrementowany).Append("/").Append(DateTime.Today.Year.ToString());
            this.numerFaktury = sb.ToString();
            this.dataWystawienia = DateTime.Now;
            this.produkty = new Dictionary<Produkt, int>();
            this.produktyList = new List<KeyValuePair<Produkt, int>>();
        }

        public double podajRazem()
        {
            double razem = 0.0;
            foreach (System.Collections.Generic.KeyValuePair<Produkt,int> kvp in this.Produkty)
            {
                razem += kvp.Key.CenaBrutto * kvp.Value;
            }
            if(this.OdbiorcaFizyczny != null)
            {
                razem = razem * this.OdbiorcaFizyczny.Rabat;
            }
            if(this.OdbiorcaPrawny != null)
            {
                razem = razem * this.OdbiorcaPrawny.Rabat;
            }
            this.Razem = razem;
            return razem;
        }

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

