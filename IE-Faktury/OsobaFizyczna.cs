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
        private string nazwisko;
        private ulong pesel;
        private DateTime dataUrodzenia;
        private string adres;
        private static uint liczbaTransakcji = 0;

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

        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }

            set
            {
                nazwisko = value;
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

        public uint LiczbaTransakcji
        {
            get
            {
                return liczbaTransakcji;
            }

            set
            {
                liczbaTransakcji = value;
            }
        }

        public override string ToString()
        {
            return (this.Imie + " " + this.Nazwisko);
        }
    }
}
