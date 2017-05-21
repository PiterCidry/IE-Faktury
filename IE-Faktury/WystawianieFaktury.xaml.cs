
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Diagnostics;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Xml;
using System.ComponentModel;
using Microsoft.Win32;

namespace IE_Faktury
{
    /// <summary>
    /// Okno w którym dodawane są produkty i odbiorcy do faktury.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class WystawianieFaktury : Window
    {

        /// <summary>
        /// Instancja klasy faktury.
        /// </summary>
        Faktura faktura = new Faktura();
        /// <summary>
        /// Instancja klasy baza odbiorców.
        /// </summary>
        BazaOdbiorcow bazaOdbiorcow = new BazaOdbiorcow();
        /// <summary>
        /// Instancja klasy baza faktur.
        /// </summary>
        BazaFaktur bazaFaktur = new BazaFaktur();


        /// <summary>
        /// Kontruktor domyślny okna: <see cref="WystawianieFaktury" />.
        /// </summary>
        public WystawianieFaktury()
        {
            InitializeComponent();
            try
            {
                bazaOdbiorcow = (BazaOdbiorcow)bazaOdbiorcow.OdczytajBaze();
                bazaFaktur = (BazaFaktur)bazaFaktur.OdczytajBaze();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
            textBox_nr.Text = faktura.NumerFaktury;
            textBox_data.Text = faktura.DataWystawienia.ToString("dd-MM-yyyy");
            dataGrid_produkty.ItemsSource = faktura.Produkty;
            button_utworz.IsEnabled = false;
        }

        /// <summary>
        /// Obsługa zdarzenia po naciśnięciu przycisku dodaj (produkt).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void button_dodajProd_Click(object sender, RoutedEventArgs e)
        {
            WyborProduktow wybor = new WyborProduktow(faktura);
            wybor.ShowDialog();
            dataGrid_produkty.Items.Refresh();
            textBox_razem.Text = String.Format("{0:c}", faktura.podajRazem());
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku zmień (produkt).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_zmienProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = dataGrid_produkty.SelectedItem;
                System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32> element2 = (System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32>)element;
                WyborProduktow wybor = new WyborProduktow(faktura, element2.Key, element2.Value);
                wybor.ShowDialog();
                dataGrid_produkty.Items.Refresh();
                textBox_razem.Text = String.Format("{0:c}", faktura.podajRazem());
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        /// Obsługa naciśnięcia przycisku usuń (produkt).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_usunProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = dataGrid_produkty.SelectedItem;
                System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32> element2 = (System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32>)element;
                faktura.Produkty.Remove(element2.Key);
                dataGrid_produkty.Items.Refresh();
                textBox_razem.Text = String.Format("{0:c}", faktura.podajRazem());
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nie wybrano produktu do usunięcia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        /// <summary>
        /// Obsługa naciśnięcia przycisku dodaj (odbiorcę).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_dodajOdbiorce_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton_prawny.IsChecked == true)
            {
                OsobaPrawna osobaPrawna = new OsobaPrawna();
                Prawny p1 = new Prawny(osobaPrawna);
                p1.ShowDialog();
                if (p1.DialogResult != false)
                {
                    bazaOdbiorcow.DodajPrawna(osobaPrawna);
                    bazaOdbiorcow.ZapiszBaze();
                    bazaOdbiorcow.OdczytajBaze();
                    comboBox_odbiorca.Items.Refresh();
                    comboBox_odbiorca.SelectedIndex = comboBox_odbiorca.Items.Count - 1;
                }
            }
            if (radioButton_fizyczny.IsChecked == true)
            {
                OsobaFizyczna osobaFizyczna = new OsobaFizyczna();
                Fizyczny f1 = new Fizyczny(osobaFizyczna);
                f1.ShowDialog();
                if (f1.DialogResult != false)
                {
                    bazaOdbiorcow.DodajFizyczna(osobaFizyczna);
                    bazaOdbiorcow.ZapiszBaze();
                    bazaOdbiorcow.OdczytajBaze();
                    comboBox_odbiorca.Items.Refresh();
                    comboBox_odbiorca.SelectedIndex = comboBox_odbiorca.Items.Count - 1;
                }
            }
        }

        /// <summary>
        /// Obsługa zdarzenia wybrania opcji odbiorca prawny.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void radioButton_prawny_Checked(object sender, RoutedEventArgs e)
        {
            comboBox_odbiorca.ItemsSource = bazaOdbiorcow.listaPrawnych;
        }

        /// <summary>
        /// Obsługa zdarzenia wybrania opcji odbiorca fizyczny.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void radioButton_fizyczny_Checked(object sender, RoutedEventArgs e)
        {
            comboBox_odbiorca.ItemsSource = bazaOdbiorcow.listaFizycznych;
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku utwórz fakturę.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_utworz_Click(object sender, RoutedEventArgs e)
        {
            button_utworz.IsEnabled = false;
            if (radioButton_fizyczny.IsChecked == true)
            {
                OsobaFizyczna os = (OsobaFizyczna)comboBox_odbiorca.SelectedItem;
                os.LiczbaTransakcji++;
                os.ustawRabat();
                bazaOdbiorcow.ZmienFizyczna((OsobaFizyczna)comboBox_odbiorca.SelectedItem, os);
                bazaOdbiorcow.ZapiszBaze();
                faktura.OdbiorcaFizyczny = os;

            }
            if (radioButton_prawny.IsChecked == true)
            {
                OsobaPrawna os = (OsobaPrawna)comboBox_odbiorca.SelectedItem;
                os.LiczbaTransakcji++;
                os.ustawRabat();
                bazaOdbiorcow.ZmienPrawna((OsobaPrawna)comboBox_odbiorca.SelectedItem, os);
                bazaOdbiorcow.ZapiszBaze();
                faktura.OdbiorcaPrawny = os;

            }
            faktura.podajRazem();

            //tworzenie dokumentu pustego
            DokumentFaktury invoice = new DokumentFaktury();
            //podpinanie faktury pod dokument
            Document document = invoice.CreateDocument(faktura);
            document.UseCmykColor = true;

#if DEBUG
            //debugging.
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
            document = MigraDoc.DocumentObjectModel.IO.DdlReader.DocumentFromFile("MigraDoc.mdddl");
#endif

            //Utorzenie renderera
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

            //podpięcie dokumentu pod renderer
            pdfRenderer.Document = document;

            //zrenderowanie dokumentu
            pdfRenderer.RenderDocument();

            //obsługa nazwy pliku
            string[] numerek = faktura.NumerFaktury.Split('/');
            string filename = "Faktura_" + numerek[0] + "_" + numerek[1] + ".pdf";

            //zapis do pliku na wybranym miejscu na dysku
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = filename;

            Nullable<bool> result = saveFileDialog1.ShowDialog();
            if (result == true)
            {
                filename = saveFileDialog1.FileName;
                Debug.WriteLine(filename);
            }
            //zapis faktury
            pdfRenderer.Save(filename);
            //podgląd faktury
            Process.Start(filename);

            foreach (System.Collections.Generic.KeyValuePair<Produkt, int> p in faktura.Produkty)
            {
                KeyValuePair<Produkt, int> kvp = new KeyValuePair<Produkt, int>();
                kvp.Key = p.Key;
                kvp.Value = p.Value;
                faktura.ProduktyList.Add(kvp);
            }

            bazaFaktur.DodajFakture(faktura);
            bazaFaktur.ZapiszBaze();
            this.Close();
        }

        /// <summary>
        /// Obsługa zdarzenia naciśnięcia przycisku sprawdź dane.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void button_sprawdz_Click(object sender, RoutedEventArgs e)
        {
            //sprawdzanie istnienia odbiorcy
            if (String.IsNullOrEmpty(comboBox_odbiorca.Text))
            {
                MessageBox.Show("Nie wybrano odbiorcy!", "Bład!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //sprawdzanie ilosci produktow
            else if (dataGrid_produkty.Items.Count <= 0)
            {
                MessageBox.Show("Nie wprowadzono żadnego produktu!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //jak wszystko w porządku
            else
            {
                MessageBox.Show("Wszystko w porządku, tworzenie faktury...", "OK!", MessageBoxButton.OK, MessageBoxImage.Information);
                button_utworz.IsEnabled = true;
            }
        }

        /// <summary>
        /// Obsługa wyświetlania rabatów
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs" /> instance containing the event data.</param>
        private void comboBox_odbiorca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_odbiorca.SelectedItem != null)
            {
                if (radioButton_fizyczny.IsChecked == true)
                {
                    OsobaFizyczna osf = new OsobaFizyczna();
                    osf = comboBox_odbiorca.SelectedItem as OsobaFizyczna;
                    osf.ustawRabat();
                    if (osf.Rabat == 1)
                    {
                        textBox_rabat.Text = "0%";
                    }
                    else if (osf.Rabat == 0.9)
                    {
                        textBox_rabat.Text = "10%";
                    }
                    else if (osf.Rabat == 0.85)
                    {
                        textBox_rabat.Text = "15%";
                    }
                    else if (osf.Rabat == 0.8)
                    {
                        textBox_rabat.Text = "20%";
                    }
                    else
                    {
                        textBox_rabat.Text = "0%";
                    }
                }
                else
                {
                    OsobaPrawna osp = new OsobaPrawna();
                    osp = comboBox_odbiorca.SelectedItem as OsobaPrawna;
                    osp.ustawRabat();
                    if (osp.Rabat == 1)
                    {
                        textBox_rabat.Text = "0%";
                    }
                    else if (osp.Rabat == 0.9)
                    {
                        textBox_rabat.Text = "10%";
                    }
                    else if (osp.Rabat == 0.85)
                    {
                        textBox_rabat.Text = "15%";
                    }
                    else if (osp.Rabat == 0.8)
                    {
                        textBox_rabat.Text = "20%";
                    }
                    else
                    {
                        textBox_rabat.Text = "0%";
                    }
                }
            }
            textBox_razem.Text = String.Format("{0:c}", faktura.podajRazem());
        }
    }
}