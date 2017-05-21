using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IE_Faktury
{
    /// <summary>
    /// Klasa osoby fizycznej.
    /// </summary>
    public class OsobaFizyczna
    {
        /// <summary>
        /// Imię.
        /// </summary>
        private string imie;
        /// <summary>
        /// Nazwisko.
        /// </summary>
        private string nazwisko;
        /// <summary>
        /// Pesel.
        /// </summary>
        private ulong pesel;
        /// <summary>
        /// Data urodzenia.
        /// </summary>
        private DateTime dataUrodzenia;
        /// <summary>
        /// Ulica.
        /// </summary>
        private string ulica;
        /// <summary>
        /// Kod pocztowy.
        /// </summary>
        private string kodPocztowy;
        /// <summary>
        /// Miasto.
        /// </summary>
        private string miasto;
        /// <summary>
        /// Liczba transakcji.
        /// </summary>
        private uint liczbaTransakcji = 0;
        /// <summary>
        /// Rabat.
        /// </summary>
        private double rabat;

        /// <summary>
        ///  Udostępnianie lub zmiana imienia.
        /// </summary>
        /// <value>
        /// Imie.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana nazwiska.
        /// </summary>
        /// <value>
        /// Nazwisko.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana nr pesel.
        /// </summary>
        /// <value>
        /// Pesel.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana daty urodzenia.
        /// </summary>
        /// <value>
        /// Data urodzenia.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana ulicy.
        /// </summary>
        /// <value>
        /// Ulica.
        /// </value>
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
        /// <summary>
        /// Udostępnianie lub zmiana miasta.
        /// </summary>
        /// <value>
        /// Miasto.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana kodu pocztowego.
        /// </summary>
        /// <value>
        /// Kod pocztowy.
        /// </value>
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

        /// <summary>
        ///  Udostępnianie lub zmiana liczby transakcji.
        /// </summary>
        /// <value>
        /// Liczba transakcji.
        /// </value>
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

        /// <summary>
        /// Udostępnianie lub zmiana rabatu.
        /// </summary>
        /// <value>
        /// Rabat.
        /// </value>
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

        /// <summary>
        /// Konstruktor domyślny klasy <see cref="OsobaFizyczna" />.
        /// </summary>
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


        /// <summary>
        /// Metoda zwracająca <see cref="System.String" /> reprezentujący osobę.
        /// </summary>
        /// <returns>
        /// <see cref="System.String" /> reprezentujący osobę.
        /// </returns>
        public override string ToString()
        {
            return (this.Imie + " " + this.Nazwisko);
        }

        /// <summary>
        /// Metoda wyświetlenia osoby.
        /// </summary>
        /// <returns>String reprezentujący osobę w wersji rozszerzonej</returns>
        public string wyswietl()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Imie + " " + this.Nazwisko);
            sb.AppendLine(this.Ulica);
            sb.AppendLine(this.KodPocztowy + " " + this.Miasto);
            return sb.ToString();
        }


        /// <summary>
        /// Metoda walidacji wprowadzonego imienia.
        /// </summary>
        /// <param name="i">Imię</param>
        /// <returns>True jeśli imie jest poprawne, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda walidacji wprowadzonego nazwiska.
        /// </summary>
        /// <param name="n">Nazwisko</param>
        /// <returns>True jeśli nazwaisko jest poprawne, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda walidacji wprowadzonego nr pesel.
        /// </summary>
        /// <param name="szPesel">Pesel.</param>
        /// <returns>True jeśli pesel jest poprawny, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda walidująca wprowadzoną ulicę
        /// </summary>
        /// <param name="u">Ulica.</param>
        /// <returns>True jeśli ulica jest poprawna, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda walidująca nr domu.
        /// </summary>
        /// <param name="nr">Nr domu</param>
        /// <returns>True jeśli nr jest poprawny, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda walidująca kod pocztowy.
        /// </summary>
        /// <param name="k">Kod pocztowy.</param>
        /// <returns>True jeśli kod pocztowy jest poprawny, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda walidująca wprowadzoną nazwę miasta.
        /// </summary>
        /// <param name="m">Miasto.</param>
        /// <returns>True jeśli miasto jest poprawne, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda ustawiająca rabat na podstawie liczby transakcji.
        /// </summary>
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
