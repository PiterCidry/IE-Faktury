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

namespace IE_Faktury
{

    public class Faktura
    {
        private static uint inkrementowany = 1;
        private string numerFaktury;
        private DateTime dataWystawienia;
        private OsobaFizyczna odbiorcaFizyczny;
        private OsobaPrawna odbiorcaPrawny;
        private Dictionary<Produkt, int> produkty;

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

        public Faktura()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inkrementowany).Append("/").Append(DateTime.Today.Year.ToString());
            this.numerFaktury = sb.ToString();
            inkrementowany++;
            this.dataWystawienia = DateTime.Now;
            this.produkty = new Dictionary<Produkt, int>();
        }

    }
}

