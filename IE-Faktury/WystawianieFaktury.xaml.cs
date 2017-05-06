
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
        Faktura f;

        public WystawianieFaktury()
        {
            InitializeComponent();
            textBox_nr.Text = faktura.NumerFaktury;
            textBox_data.Text = faktura.DataWystawienia.ToString("yyyy-MM-dd");
            dataGrid_produkty.ItemsSource = faktura.Produkty;
        }
        public WystawianieFaktury(Faktura faktura)
        {
            this.f = faktura;
            baza = (BazaOdbiorcow)baza.OdczytajBaze();
            if (radioButton_prawny.IsChecked == true)
            {
                comboBox_odbiorca.ItemsSource = baza.listaPrawnych;
            }
            if (radioButton_fizyczny.IsChecked == true)
            {

                comboBox_odbiorca.ItemsSource = baza.listaFizycznych;
            }


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
                Prawny p1 = new Prawny();
                p1.ShowDialog();
            }
            if (radioButton_fizyczny.IsChecked == true)
            {
                Fizyczny f1 = new Fizyczny();
                f1.ShowDialog();
            }
        }







        private void Button_Click(object sender, RoutedEventArgs e)
        {
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




        private void textBox_nr_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBox_odbiorca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


     
    }
}








