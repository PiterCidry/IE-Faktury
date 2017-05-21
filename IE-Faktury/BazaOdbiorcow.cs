using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IE_Faktury
{
    /// <summary>
    /// Klasa bazy odbiorców.
    /// </summary>
    [Serializable]
    public class BazaOdbiorcow
    {
        /// <summary>
        /// Udostępnianie lub zmiana listy osób fizycznych.
        /// </summary>
        /// <value>
        /// Lsita osób fizycznych.
        /// </value>
        public List<OsobaFizyczna> listaFizycznych { get; set; }
        /// <summary>
        /// Udostępnianie lub zmiana listy osób prawnych.
        /// </summary>
        /// <value>
        /// Lista osób prawnych.
        /// </value>
        public List<OsobaPrawna> listaPrawnych { get; set; }

        /// <summary>
        /// Kontruktor domyślny klasy: <see cref="BazaOdbiorcow"/>.
        /// </summary>
        public BazaOdbiorcow()
        {
            listaFizycznych = new List<OsobaFizyczna>();
            listaPrawnych = new List<OsobaPrawna>();
        }

        /// <summary>
        /// Metoda dodawania osoby fizycznej do listy.
        /// </summary>
        /// <param name="o">Osoba fizyczna.</param>
        public void DodajFizyczna(OsobaFizyczna o)
        {
            listaFizycznych.Add(o);
        }

        /// <summary>
        /// Metoda dodawania osoby prawnej do listy.
        /// </summary>
        /// <param name="o">Osoba prawna.</param>
        public void DodajPrawna(OsobaPrawna o)
        {
            listaPrawnych.Add(o);
        }

        /// <summary>
        /// Metoda usuwająca osobę fizyczną z lsity.
        /// </summary>
        /// <param name="indeks">Indeks osoby do usunięcia.</param>
        public void UsunFizyczna(int indeks)
        {
            listaFizycznych.RemoveAt(indeks);
        }

        /// <summary>
        /// Metoda usuwająca osobę prawną z listy.
        /// </summary>
        /// <param name="indeks">Indeks osoby do usunięcia.</param>
        public void UsunPrawna(int indeks)
        {
            listaPrawnych.RemoveAt(indeks);
        }

        /// <summary>
        /// Metoda zwracająca osobę fizyczną z listy.
        /// </summary>
        /// <param name="indeks">Indeks osoby fizycznej.</param>
        /// <returns>Obiekt klasy osoba fizyczna.</returns>
        public OsobaFizyczna PodajFizyczna(int indeks)
        {
            return listaFizycznych.ElementAt(indeks);
        }

        /// <summary>
        /// Metoda zwracająca osobę prwaną z listy.
        /// </summary>
        /// <param name="indeks">Indeks osoby prawnej.</param>
        /// <returns>Obiekt klasy osoba prawna.</returns>
        public OsobaPrawna PodajPrawna(int indeks)
        {
            return listaPrawnych.ElementAt(indeks);
        }

        /// <summary>
        /// Metoda zmieniająca osobę fizyczną na inną.
        /// </summary>
        /// <param name="stara">Stara osoba fizyczna.</param>
        /// <param name="nowa">Nowa osoba fizyczna.</param>
        public void ZmienFizyczna(OsobaFizyczna stara, OsobaFizyczna nowa)
        {
            listaFizycznych[listaFizycznych.FindIndex(i => i.Equals(stara))] = nowa;
        }

        /// <summary>
        /// Medota zmieniająca osobę prawną na inną.
        /// </summary>
        /// <param name="stara">Stara osoba prawna.</param>
        /// <param name="nowa">Nowa osoba prawna.</param>
        public void ZmienPrawna(OsobaPrawna stara, OsobaPrawna nowa)
        {
            listaPrawnych[listaPrawnych.FindIndex(i => i.Equals(stara))] = nowa;
        }

        /// <summary>
        /// Metoda zapisująca bazę odbiorców do pliku xml.
        /// </summary>
        public void ZapiszBaze()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BazaOdbiorcow));
                StreamWriter sw = new StreamWriter("../../BazaOdbiorcow.xml");
                serializer.Serialize(sw, this);
                sw.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }

        /// <summary>
        /// Metoda odczytująca bazę odbiorców z pliku xml.
        /// </summary>
        /// <returns>Obiekt bazy produktów</returns>
        public Object OdczytajBaze()
        {
            BazaOdbiorcow Baza = new BazaOdbiorcow();
            TextReader tr = new StreamReader("../../BazaOdbiorcow.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(BazaOdbiorcow));
            Baza = (BazaOdbiorcow)serializer.Deserialize(tr);
            tr.Close();
            return Baza;
        }
    }
}
