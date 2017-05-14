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
    [Serializable]
    public class BazaOdbiorcow
    {
        public List<OsobaFizyczna> listaFizycznych { get; set; }
        public List<OsobaPrawna> listaPrawnych { get; set; }

        public BazaOdbiorcow()
        {
            listaFizycznych = new List<OsobaFizyczna>();
            listaPrawnych = new List<OsobaPrawna>();
        }

        public void DodajFizyczna(OsobaFizyczna o)
        {
            listaFizycznych.Add(o);
        }

        public void DodajPrawna(OsobaPrawna o)
        {
            listaPrawnych.Add(o);
        }

        public void UsunFizyczna(int indeks)
        {
            listaFizycznych.RemoveAt(indeks);
        }

        public void UsunPrawna(int indeks)
        {
            listaPrawnych.RemoveAt(indeks);
        }

        public OsobaFizyczna PodajFizyczna(int indeks)
        {
            return listaFizycznych.ElementAt(indeks);
        }

        public OsobaPrawna PodajPrawna(int indeks)
        {
            return listaPrawnych.ElementAt(indeks);
        }

        public void ZmienFizyczna(OsobaFizyczna stara, OsobaFizyczna nowa)
        {
            listaFizycznych[listaFizycznych.FindIndex(i => i.Equals(stara))] = nowa;
        }

        public void ZmienPrawna(OsobaPrawna stara, OsobaPrawna nowa)
        {
            listaPrawnych[listaPrawnych.FindIndex(i => i.Equals(stara))] = nowa;
        }

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
