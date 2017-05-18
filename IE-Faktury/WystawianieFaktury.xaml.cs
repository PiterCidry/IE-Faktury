
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

namespace IE_Faktury
{
    /// <summary>
    /// Interaction logic for WystawianieFaktury.xaml
    /// </summary>
    public partial class WystawianieFaktury : Window
    {
        Faktura faktura = new Faktura();
        BazaOdbiorcow bazaOdbiorcow = new BazaOdbiorcow();
        BazaFaktur bazaFaktur = new BazaFaktur();
        public bool JestFizyczny;

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

        private void button_dodajProd_Click(object sender, RoutedEventArgs e)
        {
            WyborProduktow wybor = new WyborProduktow(faktura);
            wybor.ShowDialog();
            dataGrid_produkty.Items.Refresh();
            textBox_razem.Text = faktura.podajRazem(textBox_rabat.Text).ToString();
        }

        private void button_zmienProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = dataGrid_produkty.SelectedItem;
                System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32> element2 = (System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32>)element;
                WyborProduktow wybor = new WyborProduktow(faktura, element2.Key, element2.Value);
                wybor.ShowDialog();
                dataGrid_produkty.Items.Refresh();
                textBox_razem.Text = faktura.podajRazem(textBox_rabat.Text).ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nie wybrano żadnego produktu!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void button_usunProd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var element = dataGrid_produkty.SelectedItem;
                System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32> element2 = (System.Collections.Generic.KeyValuePair<IE_Faktury.Produkt, System.Int32>)element;
                faktura.Produkty.Remove(element2.Key);
                dataGrid_produkty.Items.Refresh();
                textBox_razem.Text = faktura.podajRazem(textBox_rabat.Text).ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nie wybrano produktu do usunięcia!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

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

        private void radioButton_prawny_Checked(object sender, RoutedEventArgs e)
        {
            comboBox_odbiorca.ItemsSource = bazaOdbiorcow.listaPrawnych;
        }

        private void radioButton_fizyczny_Checked(object sender, RoutedEventArgs e)
        {
            comboBox_odbiorca.ItemsSource = bazaOdbiorcow.listaFizycznych;
        }

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
                JestFizyczny = true;
            }
            if (radioButton_prawny.IsChecked == true)
            {
                OsobaPrawna os = (OsobaPrawna)comboBox_odbiorca.SelectedItem;
                os.LiczbaTransakcji++;
                os.ustawRabat();
                bazaOdbiorcow.ZmienPrawna((OsobaPrawna)comboBox_odbiorca.SelectedItem, os);
                bazaOdbiorcow.ZapiszBaze();
                faktura.OdbiorcaPrawny = os;
                JestFizyczny = false;
            }
            faktura.podajRazem();
            /*  try
              {
                  // Create a invoice form with the sample invoice data
                  Faktura invoice = new Faktura("../../invoice.xml");

                  // Create a MigraDoc document
                  Document document = invoice.CreateDocument();
                  document.UseCmykColor = true;

    #if DEBUG
                  // for debugging only...
                  MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
                  document = MigraDoc.DocumentObjectModel.IO.DdlReader.DocumentFromFile("MigraDoc.mdddl");
  #endif

                  // Create a renderer for PDF that uses Unicode font encoding
                  PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

                  // Set the MigraDoc document
                  pdfRenderer.Document = document;

                  // Create the PDF document
                  pdfRenderer.RenderDocument();

                  // Save the PDF document...
                  string filename = "Invoice.pdf";
  #if DEBUG
                  // I don't want to close the document constantly...
                  filename = "Invoice-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";
  #endif
                  pdfRenderer.Save(filename);
                  // ...and start a viewer.
                  Process.Start(filename);

              }
              catch { }
          }*/
            /*  // Create a new PDF document
              PdfDocument document = new PdfDocument();
              document.Info.Title = "Created with PDFsharp";

              // Create an empty page
              PdfPage page = document.AddPage();

              // Get an XGraphics object for drawing
              XGraphics gfx = XGraphics.FromPdfPage(page);

              // Create a font
              XFont font = new XFont("Verdana", 10, XFontStyle.BoldItalic);

              // Draw the text
              gfx.DrawString("Data wystawienia: " + textBox_data.Text, font, XBrushes.Black,
                new XRect(50, 10, page.Width, page.Height), XStringFormats.TopLeft);
              gfx.DrawString("Numer faktury: " + textBox_nr.Text, font, XBrushes.Black,
               new XRect(50, 30, page.Width, page.Height), XStringFormats.TopLeft);

              gfx.DrawString("", font, XBrushes.Black,
               new XRect(50, 50, page.Width, page.Height), XStringFormats.TopLeft);

              // Save the document...
              const string filename = "Faktura.pdf";
              document.Save(filename);
              // ...and start a viewer.
              Process.Start(filename); }*/
            DokumentFaktury invoice = new DokumentFaktury();
            Document document = invoice.CreateDocument(faktura);
            document.UseCmykColor = true;

#if DEBUG
            // for debugging only...
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
            document = MigraDoc.DocumentObjectModel.IO.DdlReader.DocumentFromFile("MigraDoc.mdddl");
#endif

            // Create a renderer for PDF that uses Unicode font encoding
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

            // Set the MigraDoc document
            pdfRenderer.Document = document;

            // Create the PDF document
            pdfRenderer.RenderDocument();

            // Save the PDF document...
            string filename = "Invoice.pdf";
#if DEBUG
            // I don't want to close the document constantly...
            filename = "Invoice-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";
#endif
            pdfRenderer.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);

            foreach(System.Collections.Generic.KeyValuePair<Produkt, int> p in faktura.Produkty)
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

        private void button_sprawdz_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox_odbiorca.Text))
            {
                MessageBox.Show("Nie wybrano odbiorcy!", "Bład!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (dataGrid_produkty.Items.Count <= 0)
            {
                MessageBox.Show("Nie wprowadzono żadnego produktu!", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBox.Show("Wszystko w porządku, tworzenie faktury...", "OK!", MessageBoxButton.OK, MessageBoxImage.Information);
                button_utworz.IsEnabled = true;
            }
        }

        private void comboBox_odbiorca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_odbiorca.SelectedItem != null)
            {
                if(radioButton_fizyczny.IsChecked == true)
                {
                    OsobaFizyczna osf = new OsobaFizyczna();
                    osf = comboBox_odbiorca.SelectedItem as OsobaFizyczna;
                    osf.ustawRabat();
                    if(osf.Rabat == 1)
                    {
                        textBox_rabat.Text = "0%";
                    }
                    else if(osf.Rabat == 0.9)
                    {
                        textBox_rabat.Text = "10%";
                    }
                    else if(osf.Rabat == 0.85)
                    {
                        textBox_rabat.Text = "15%";
                    }
                    else if(osf.Rabat == 0.8)
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
            textBox_razem.Text = faktura.podajRazem(textBox_rabat.Text).ToString();
        }
    }
}