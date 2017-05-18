using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IE_Faktury
{
    public class OsobaFizyczna
    {
        private string imie;
        private string nazwisko;
        private ulong pesel;
        private DateTime dataUrodzenia;
        private string ulica;
        private string kodPocztowy;
        private string miasto;
        private uint liczbaTransakcji = 0;
        private double rabat;

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

        public OsobaFizyczna()
        {
            this.imie = "";
            this.nazwisko = "";
            this.pesel = 0;
            this.dataUrodzenia = new DateTime();
            this.ulica = "";
            this.kodPocztowy = "";
            this.miasto = "";
            this.rabat = 0;
        }


        public override string ToString()
        {
           return (this.Imie + " " + this.Nazwisko);
            
        }
        
        public string wyswietl()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Imie + " " + this.Nazwisko);
            sb.AppendLine(this.Ulica);
            sb.AppendLine(this.KodPocztowy + " " + this.Miasto);
            return sb.ToString();
        }
    

        public bool validateImie(string i)
        {
            Regex rgx = new Regex(@"^[\p{Lu}][\p{L}'\-\s]{0,48}[\p{L}'\-]$");
            Match mtch = rgx.Match(i);
            if (mtch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateNazwisko(string n)
        {
            Regex rgx = new Regex(@"^[\p{Lu}][\p{L}0-9']+[\s\-]?[\p{L}0-9']*[\s-]?[\p{L}0-9']+$");
            Match mtch = rgx.Match(n);
            if (mtch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validatePesel(string szPesel)
        {
            byte[] tab = new byte[10] { 9, 7, 3, 1, 9, 7, 3, 1, 9, 7 };
            byte[] tablicz = new byte[] { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };
            bool bResult = false;
            int suma = 0;
            int sumcontrol = 0;

            szPesel = szPesel.Trim();

            if (szPesel.Length == 11)
            {
                foreach (char l in szPesel)
                {
                    byte b = Convert.ToByte(l);
                    if (Array.IndexOf(tablicz, Convert.ToByte(l)) == -1) return false;
                }

                sumcontrol = Convert.ToInt32(szPesel[10].ToString());

                for (int i = 0; i < 10; i++)
                {
                    suma += tab[i] * Convert.ToInt32(szPesel[i].ToString());
                }

                bResult = ((suma % 10) == sumcontrol);

                if (bResult)
                {
                    int rok = 0;
                    int mies = 0;
                    int dzien = Convert.ToInt32(szPesel[4].ToString()) * 10 + Convert.ToInt32(szPesel[5].ToString());

                    if (szPesel[2] == '0' || szPesel[2] == '1')
                    {
                        rok = 1900;
                        mies = Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString());
                    }
                    else if (szPesel[2] == '2' || szPesel[2] == '3')
                    {
                        rok = 2000;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 20);
                    }
                    else if (szPesel[2] == '4' || szPesel[2] == '5')
                    {
                        rok = 2100;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 40);
                    }
                    else if (szPesel[2] == '6' || szPesel[2] == '7')
                    {
                        rok = 2200;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 60);
                    }
                    else if (szPesel[2] == '8' || szPesel[2] == '9')
                    {
                        rok = 1800;
                        mies = (Convert.ToInt32(szPesel[2].ToString()) * 10 + Convert.ToInt32(szPesel[3].ToString()) - 80);
                    }
                    rok += Convert.ToInt32(szPesel[0].ToString()) * 10 + Convert.ToInt32(szPesel[1].ToString());
                    String szDate = rok.ToString() + "-" + (mies < 10 ? "0" + mies.ToString() : mies.ToString()) + "-" + (dzien < 10 ? "0" + dzien.ToString() : dzien.ToString());
                    DateTime dt;
                    bResult = DateTime.TryParse(szDate, out dt);
                }
            }
            else
            {
                return false;
            }

            return bResult;
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

        public void ustawRabat()
        {
            if(this.LiczbaTransakcji < 6)
            {
                this.Rabat = 1;
            }
            else if(this.LiczbaTransakcji >= 6 && this.LiczbaTransakcji < 11)
            {
                this.Rabat = 0.9;
            }
            else if(this.LiczbaTransakcji >= 11 && this.LiczbaTransakcji < 16)
            {
                this.Rabat = 0.85;
            }
            else if(this.LiczbaTransakcji >= 16)
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
