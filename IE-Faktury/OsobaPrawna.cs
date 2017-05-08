using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IE_Faktury
{
    public class OsobaPrawna
    {
        private string nazwa;
        private string ulica;
        private string kodPocztowy;
        private string miasto;
        private ulong nip;
        private static uint liczbaTransakcji = 0;

        public string Nazwa
        {
            get
            {
                return nazwa;
            }

            set
            {
                nazwa = value;
            }
        }
        public string Ulica
        {
            get
            {
                return ulica;
            }

            set
            {
                ulica = value;
            }
        }
        public string Miasto
        {
            get
            {
                return miasto;
            }

            set
            {
                miasto = value;
            }
        }
        public string KodPocztowy
        {
            get
            {
                return kodPocztowy;
            }

            set
            {
                kodPocztowy = value;
            }
        }
       
        public ulong Nip
        {
            get
            {
                return nip;
            }

            set
            {
                nip = value;
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

        public OsobaPrawna()
        {
            this.nazwa = "";
            this.ulica = "";
            this.kodPocztowy = "";
            this.miasto = "";
            this.nip = 0;
        }

        public override string ToString()
        {
            return (this.Nazwa);
        }

        public bool validateNazwa(string n)
        {
            Regex rgx_nazwa = new Regex(@"^[\p{Lu}](\w+|(\s|\.|\-)?){0,99}$");
            Match match_nazwa = rgx_nazwa.Match(n);
            if (match_nazwa.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateUlica(string u)
        {
            Regex rgx = new Regex(@"^[\p{L}0-9']+[\s\-]?[\p{L}0-9']*[\s-]?[\p{L}0-9']+$");
            Match mtch = rgx.Match(u);
            if (mtch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateNrDomu(string nr)
        {
            Regex rgx = new Regex(@"^\d{1,4}(\/\d{1,3})?$");
            Match mtch = rgx.Match(nr);
            if (mtch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateKodPocztowy(string k)
        {
            Regex rgx = new Regex(@"^\d{2}\-\d{3}$");
            Match mtch = rgx.Match(k);
            if (mtch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateMiasto(string m)
        {
            Regex rgx = new Regex(@"^[\p{Lu}][\p{L}]+[\s\-]?[\p{L}]*[\s-]?[\p{L}]+$");
            Match mtch = rgx.Match(m);
            if (mtch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateNip(string n)
        {
            int[] wagi = new int[9] { 6, 5, 7, 2, 3, 4, 5, 6, 7};
            int[] cyfry = new int[10];
            char[] znaki = new char[10];
            bool result = false;
            n = n.Trim();
            if(n.Length == 10)
            {
                znaki = n.ToCharArray();
                try
                {
                    for (int i = 0; i < n.Length; i++)
                    {
                        cyfry[i] = Int32.Parse(znaki[i].ToString());
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
                try
                {
                    for (int i = 0; i < wagi.Length; i++)
                    {
                        cyfry[i] = cyfry[i] * wagi[i];
                    }
                }
                catch (Exception)
                {
                    result = false;
                }
                int suma = 0;
                foreach (var item in cyfry)
                {
                    suma += item;
                }
                suma -= cyfry[9];
                int sumaKontrolna = 0;
                sumaKontrolna = (suma % 11);
                if(sumaKontrolna == cyfry[9])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}


