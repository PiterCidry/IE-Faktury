
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

namespace IE_Faktury
{
    /// <summary>
    /// Interaction logic for WystawianieFaktury.xaml
    /// </summary>
    public partial class WystawianieFaktury : Window
    {
        Faktura faktura = new Faktura();
        BazaOdbiorcow baza = new BazaOdbiorcow();

        public WystawianieFaktury()
        {
            InitializeComponent();
            if (File.Exists("../../BazaOdbiorcow.xml"))
            {
                baza = (BazaOdbiorcow)baza.OdczytajBaze();
            }
            textBox_nr.Text = faktura.NumerFaktury;
            textBox_data.Text = faktura.DataWystawienia.ToString("yyyy-MM-dd");
            dataGrid_produkty.ItemsSource = faktura.Produkty;
        }

        private void button_dodajProd_Click(object sender, RoutedEventArgs e)
        {
            WyborProduktow wybor = new WyborProduktow(faktura);
            wybor.ShowDialog();
            dataGrid_produkty.Items.Refresh();
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
                    baza.DodajPrawna(osobaPrawna);
                    baza.ZapiszBaze();
                    baza.OdczytajBaze();
                    comboBox_odbiorca.Items.Refresh();
                    comboBox_odbiorca.SelectedIndex = comboBox_odbiorca.Items.Count - 1;
                }
            }
            if (radioButton_fizyczny.IsChecked == true)
            {
                OsobaFizyczna osobaFizyczna = new OsobaFizyczna();
                Fizyczny f1 = new Fizyczny(osobaFizyczna);
                f1.ShowDialog();
                if(f1.DialogResult != false)
                {
                    baza.DodajFizyczna(osobaFizyczna);
                    baza.ZapiszBaze();
                    baza.OdczytajBaze();
                    comboBox_odbiorca.Items.Refresh();
                    comboBox_odbiorca.SelectedIndex = comboBox_odbiorca.Items.Count - 1;
                }
            }
        }

        private void radioButton_prawny_Checked(object sender, RoutedEventArgs e)
        {
            comboBox_odbiorca.ItemsSource = baza.listaPrawnych;
        }

        private void radioButton_fizyczny_Checked(object sender, RoutedEventArgs e)
        {
            comboBox_odbiorca.ItemsSource = baza.listaFizycznych;
        }

        private void button_Utworz_Click(object sender, RoutedEventArgs e)
        {
            if (radioButton_fizyczny.IsChecked == true)
            {
                faktura.OdbiorcaFizyczny = (OsobaFizyczna)comboBox_odbiorca.SelectedItem;
            }
            if (radioButton_prawny.IsChecked == true)
            {
                faktura.OdbiorcaPrawny = (OsobaPrawna)comboBox_odbiorca.SelectedItem;
            }
            Debug.WriteLine("Data " + faktura.DataWystawienia);
            Debug.WriteLine("Nr " + faktura.NumerFaktury);
            Debug.WriteLine("dic prod count " + faktura.Produkty.Count);
            Debug.WriteLine("Wystawca " + faktura.Wystawca);
            Debug.WriteLine("Odbiorca praw " + faktura.OdbiorcaPrawny);
            Debug.WriteLine("Odbiorca fiz " + faktura.OdbiorcaFizyczny);
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
            Faktura invoice = new Faktura();
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
    }
}