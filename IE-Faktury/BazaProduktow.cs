using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace IE_Faktury
{
    [Serializable]
    public class BazaProduktow
    {
        public List<Produkt> listaProduktow { get; set; }

        public BazaProduktow()
        {
            listaProduktow = new List<Produkt>();
        }

        public void DodajProdukt(Produkt p)
        {
            listaProduktow.Add(p);
        }

        public void UsunProdukt(int indeks)
        {
            listaProduktow.RemoveAt(indeks);
        }

        public Produkt PodajProdukt(int indeks)
        {
            return listaProduktow.ElementAt(indeks);
        }

        public void ZmienProdukt(Produkt stary, Produkt nowy)
        {
            listaProduktow[listaProduktow.FindIndex(i => i.Equals(stary))] = nowy;
        }

        public void ZapiszBaze()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BazaProduktow));
                StreamWriter sw = new StreamWriter("../../BazaProduktow.xml");
                serializer.Serialize(sw, this);
                sw.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public Object OdczytajBaze()
        {
            BazaProduktow Baza = new BazaProduktow();
            TextReader tr = new StreamReader("../../BazaProduktow.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(BazaProduktow));
            Baza = (BazaProduktow)serializer.Deserialize(tr);
            tr.Close();
            return Baza;
        }
    }
}
