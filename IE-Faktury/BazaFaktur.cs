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
    /// Klasa bazy faktur.
    /// </summary>
    [Serializable]
   public class BazaFaktur
    {
        /// <summary>
        /// Udostępnianie lub zmiana listy faktur.
        /// </summary>
        /// <value>
        /// Lista faktur.
        /// </value>
        public List<Faktura> listaFaktur { get; set; }

        /// <summary>
        /// Kontruktor domyślny klasy: <see cref="BazaFaktur"/>.
        /// </summary>
        public BazaFaktur()
        {
            listaFaktur = new List<Faktura>();
        }

        /// <summary>
        /// Metoda dodająca fakturę do listy.
        /// </summary>
        /// <param name="f">Faktura do dodania.</param>
        public void DodajFakture(Faktura f)
        {
            listaFaktur.Add(f);
        }

        /// <summary>
        /// Metoda zapisująca bazę faktur do pliku xml.
        /// </summary>
        public void ZapiszBaze()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BazaFaktur));
                StreamWriter sw = new StreamWriter("../../BazaFaktur.xml");
                serializer.Serialize(sw, this);
                sw.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }

        /// <summary>
        /// Metoda oczytująca bazę faktur z pliku xml.
        /// </summary>
        /// <returns></returns>
        public Object OdczytajBaze()
        {
            BazaFaktur Baza = new BazaFaktur();
            TextReader tr = new StreamReader("../../BazaFaktur.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(BazaFaktur));
            Baza = (BazaFaktur)serializer.Deserialize(tr);
            tr.Close();
            return Baza;
        }
    }
}
