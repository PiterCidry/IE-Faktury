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
    /// <summary>
    /// Klasa bazy produktów.
    /// </summary>
    [Serializable]
    public class BazaProduktow
    {
        /// <summary>
        /// Udostępnianie lub zmiana listy produktów.
        /// </summary>
        /// <value>
        /// Lista produktow.
        /// </value>
        public List<Produkt> listaProduktow { get; set; }

        /// <summary>
        /// Konstruktor domyślny klasy: <see cref="BazaProduktow"/>.
        /// </summary>
        public BazaProduktow()
        {
            listaProduktow = new List<Produkt>();
        }

        /// <summary>
        /// Metoda dodająca produkt do listy.
        /// </summary>
        /// <param name="p">Produkt do dodania.</param>
        public void DodajProdukt(Produkt p)
        {
            listaProduktow.Add(p);
        }

        /// <summary>
        /// Metoda usuwająca produkt z listy.
        /// </summary>
        /// <param name="indeks">Indeks na którym jest produkt.</param>
        public void UsunProdukt(int indeks)
        {
            listaProduktow.RemoveAt(indeks);
        }

        /// <summary>
        /// Metoda zwracająca produkt z listy.
        /// </summary>
        /// <param name="indeks">Indeks produktu.</param>
        /// <returns>Obiekt klasy produkt.</returns>
        public Produkt PodajProdukt(int indeks)
        {
            return listaProduktow.ElementAt(indeks);
        }

        /// <summary>
        /// Metoda zmieniająca produkt na inny.
        /// </summary>
        /// <param name="stary">Stary produkt.</param>
        /// <param name="nowy">Nowy produkt.</param>
        public void ZmienProdukt(Produkt stary, Produkt nowy)
        {
            listaProduktow[listaProduktow.FindIndex(i => i.Equals(stary))] = nowy;
        }

        /// <summary>
        /// Metoda zapisująca bazę produktów do pliku xml.
        /// </summary>
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
                Debug.WriteLine(ex.InnerException);
            }
        }

        /// <summary>
        /// Metoda odczytująca bazę produktów z pliku xml.
        /// </summary>
        /// <returns>Obiekt bazy produktów.</returns>
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
