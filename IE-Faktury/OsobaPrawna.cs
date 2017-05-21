using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IE_Faktury
{
    /// <summary>
    /// Klasa osoby prawnej.
    /// </summary>
    public class OsobaPrawna
    {
        /// <summary>
        /// Nazwa.
        /// </summary>
        private string nazwa;
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
        /// NIP.
        /// </summary>
        private ulong nip;
        /// <summary>
        /// Liczba transakcji.
        /// </summary>
        private uint liczbaTransakcji = 0;
        /// <summary>
        /// Rabat.
        /// </summary>
        private double rabat;

        /// <summary>
        /// Udostępnianie lub zmiana nazwy.
        /// </summary>
        /// <value>
        /// Nazwa.
        /// </value>
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
        /// <summary>
        /// Udostępnianie lub zmiana ulicy.
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
        /// Udostępnianie lub zmiana Miasta.
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
        /// Udostępnianie lub zmiana kodu pocztowego.
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
        /// Udostępnianie lub zmiana nipu.
        /// </summary>
        /// <value>
        /// Nip.
        /// </value>
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
        /// <summary>
        /// Udostępnianie lub zmiana Liczb Transakcji.
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
        /// Kontruktor domyślny klasy: <see cref="OsobaPrawna" />.
        /// </summary>
        public OsobaPrawna()
        {
            this.nazwa = "";
            this.ulica = "";
            this.kodPocztowy = "";
            this.miasto = "";
            this.nip = 0;
            this.rabat = 0;
        }

        /// <summary>
        /// Metoda zwracająca <see cref="System.String" /> reprezentujący osobę prawną.
        /// </summary>
        /// <returns>
        /// <see cref="System.String" /> reprezentujący osobę prawną.
        /// </returns>
        public override string ToString()
        {
            return (this.Nazwa);
        }
        /// <summary>
        /// Metoda wyświetlająca osobę prawną w rozszerzonej wersji.
        /// </summary>
        /// <returns>String reprezentujący osobę prawną w rozszerzonej wersji.</returns>
        public string wyswietl()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Nazwa);
            sb.AppendLine(this.Ulica);
            sb.AppendLine(this.KodPocztowy + " " + this.Miasto);
            sb.AppendLine("NIP: " + this.Nip);
            return sb.ToString();
        }

        /// <summary>
        /// Metoda walidująca wprowadzoną nazwę.
        /// </summary>
        /// <param name="n">Nazwa.</param>
        /// <returns>True jeśli nazwa jest poprawna, false jeżeli nie jest.</returns>
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

        /// <summary>
        /// Metoda walidująca wprowadzoną ulicę.
        /// </summary>
        /// <param name="u">Ulica.</param>
        /// <returns>True jeśli ulicajest poprawna, false jeżeli nie jest.</returns>
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
        /// Metoda walidująca nr domu wprowadzony przez uzytkownika.
        /// </summary>
        /// <param name="nr">Numer domu.</param>
        /// <returns>True jeśli nr domu jest poprawny, false jeżeli nie jest.</returns>
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
        /// Metoda walidująca kod pocztowy wprowadzony przez użytkownika.
        /// </summary>
        /// <param name="k">Kod pocztowy</param>
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
        /// Metoda walidująca wporwadzone miasto.
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
        /// Metoda walidująca numer nip.
        /// </summary>
        /// <param name="n">Nip.</param>
        /// <returns>True jeśli nip jest poprawny, false jeżeli nie jest.</returns>
        public bool validateNip(string n)
        {
            int[] wagi = new int[9] { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            int[] cyfry = new int[10];
            char[] znaki = new char[10];
            bool result = false;
            n = n.Trim();
            if (n.Length == 10)
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
                if (sumaKontrolna == cyfry[9])
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


