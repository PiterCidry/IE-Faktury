using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IE_Faktury
{
    public class OsobaFizyczna
    {
        private string imie;
        private string nawisko;
        private ulong pesel;
        private DateTime dataUrodzenia;
        private string adres;

        public string Imie
        {
            get
            {
                return imie;
            }

            set
            {
                imie = value;
            }
        }

        public string Nawisko
        {
            get
            {
                return nawisko;
            }

            set
            {
                nawisko = value;
            }
        }

        public ulong Pesel
        {
            get
            {
                return pesel;
            }

            set
            {
                pesel = value;
            }
        }

        public DateTime DataUrodzenia
        {
            get
            {
                return dataUrodzenia;
            }

            set
            {
                dataUrodzenia = value;
            }
        }

        public string Adres
        {
            get
            {
                return adres;
            }

            set
            {
                adres = value;
            }
        }
    }
}
