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
   public class BazaFaktur
    {
        public List<Faktura> listaFaktur { get; set; }

        public BazaFaktur()
        {
            listaFaktur = new List<Faktura>();
        }

        public void DodajFakture(Faktura f)
        {
            listaFaktur.Add(f);
        }

      /*  public void UsunFakture(int indeks)
        {
            listaFaktur.RemoveAt(indeks);
        }
        */
      /*  public Faktura PodajFakture(int nrFaktury)
        {
            return listaFaktur.ElementAt(nrFaktury);
        }
        */
       

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
                Debug.WriteLine(ex.Message);
            }
        }

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
