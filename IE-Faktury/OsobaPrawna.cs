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
        private uint liczbaTransakcji = 0;
        private double rabat;

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

        public double Rabat
        {
            get
            {
                return rabat;
            }

            set
            {
                rabat = value;
            }
        }

        public OsobaPrawna()
        {
            this.nazwa = "";
            this.ulica = "";
            this.kodPocztowy = "";
            this.miasto = "";
            this.nip = 0;
            this.rabat = 0;
        }

        public override string ToString()
        {
            return (this.Nazwa);
        }
        public string wyswietl()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Nazwa);
            sb.AppendLine(this.Ulica);
            sb.AppendLine(this.KodPocztowy +" "+this.Miasto);
            sb.AppendLine("NIP: " + this.Nip);
            return sb.ToString();
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

        public void ustawRabat()
        {
            if (this.LiczbaTransakcji < 6)
            {
                this.Rabat = 1;
            }
            else if (this.LiczbaTransakcji >= 6 && this.LiczbaTransakcji < 11)
            {
                this.Rabat = 0.9;
            }
            else if (this.LiczbaTransakcji >= 11 && this.LiczbaTransakcji < 16)
            {
                this.Rabat = 0.85;
            }
            else if (this.LiczbaTransakcji >= 16)
            {
                this.Rabat = 0.8;
            }
            else
            {
                this.Rabat = 1;
            }
        }
    }
}


