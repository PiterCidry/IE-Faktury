using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace IE_Faktury
{
    class DokumentFaktury
    {

        Document document;
        
        public DokumentFaktury()
        {

        }

        /// <summary>
        /// An XML invoice based on a sample created with Microsoft InfoPath.
        /// </summary>
        readonly XmlDocument invoice;

        /// <summary>
        /// The root navigator for the XML document.
        /// </summary>
        readonly XPathNavigator navigator;

        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        TextFrame addressFrame;

        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        Table table;

        private Faktura faktura;
        /// <summary>
        /// Initializes a new instance of the class BillFrom and opens the specified XML document.
        /// </summary>
        public DokumentFaktury(string filename)
        {
            this.invoice = new XmlDocument();
            this.invoice.Load(filename);
            this.navigator = this.invoice.CreateNavigator();
        }

        /// <summary>
        /// Tworzenie dokumentu
        /// </summary>
        public Document CreateDocument(Faktura f)
        {
            this.faktura = f;
            this.document = new Document();
            this.document.Info.Title = "Faktura";
            this.document.Info.Subject = "Utworzona przy pomocy programu stworzonego na projekt zaliczeniowy";
            this.document.Info.Author = "Piotr Gretszel, Joanna Jeziorek, Katarzyna Maciocha, Joanna Motyczyńska";

            DefineStyles();
            CreatePage(f);

            return this.document;
        }

        /// <summary>
        /// Style i pozycje
        /// </summary>
        void DefineStyles()
        {
            // Styl "Normal".
            Style style = this.document.Styles["Normal"];

            style.Font.Name = "Verdana";

            style = this.document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = this.document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Styl "Table"
            style = this.document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Styl "Reference" 
            style = this.document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        }

        /// <summary>
        /// Tworzenie faktury
        /// </summary>
        void CreatePage(Faktura f)
        {

            Section section = this.document.AddSection();

            // obrazek
            Image image = section.Headers.Primary.AddImage("../../faktura.png");
            image.Height = "2.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;

            // stopka
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("IE-Faktury · Gramatyka 10 · 30-001 Kraków");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // miejsce danych na stronie
            this.addressFrame = section.AddTextFrame();
            this.addressFrame.Height = "3.0cm";
            this.addressFrame.Width = "7.0cm";
            this.addressFrame.Left = ShapePosition.Left;
            this.addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            this.addressFrame.Top = "5.0cm";
            this.addressFrame.RelativeVertical = RelativeVertical.Page;

            // dane klienta
            if (f.OdbiorcaFizyczny != null)
            {
                paragraph = this.addressFrame.AddParagraph(f.OdbiorcaFizyczny.wyswietl());           
            }
            if (f.OdbiorcaPrawny != null)
            {
                paragraph = this.addressFrame.AddParagraph(f.OdbiorcaPrawny.wyswietl());
            }
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 15;
            paragraph.Format.SpaceAfter = 3;

            // miejscowosc/data
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("Faktura nr "+f.NumerFaktury.ToString(),TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Kraków, ");
            paragraph.AddDateField(f.DataWystawienia.ToString("dd-MM-yyyy"));

            // tabela produktów
            this.table = section.AddTable();
            this.table.Style = "Table";
            this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;

            // kolumny
            Column column = this.table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = this.table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = this.table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("L.P.");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[1].AddParagraph("Nazwa");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].AddParagraph("Cena jednostkowa netto");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].AddParagraph("Podatek (%)");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[4].AddParagraph("Ilosc");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[5].AddParagraph("Suma");
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            
            int i = 0;
            double suma = 0;

            foreach (System.Collections.Generic.KeyValuePair<Produkt, int> kvp in f.Produkty)
            {
                i++;
                row = table.AddRow();
                row.Format.Alignment = ParagraphAlignment.Center;
                row.Format.Font.Bold = false;
                row.Shading.Color = TableGray;
                row.Cells[0].AddParagraph(i+".");
                row.Cells[0].Format.Font.Bold = false;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
                row.Cells[1].AddParagraph(kvp.Key.Nazwa.ToString());
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Bottom;
                row.Cells[2].AddParagraph(String.Format("{0:c}", kvp.Key.CenaJednostkowa));
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[2].VerticalAlignment = VerticalAlignment.Bottom;
                row.Cells[3].AddParagraph(kvp.Key.StawkaPodatku.ToString());
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].VerticalAlignment = VerticalAlignment.Bottom;
                row.Cells[4].AddParagraph(kvp.Value.ToString());
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].VerticalAlignment = VerticalAlignment.Bottom;
                row.Cells[5].AddParagraph(String.Format("{0:c}", kvp.Key.CenaBrutto*kvp.Value));
                suma += kvp.Key.CenaBrutto * kvp.Value;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            }
            
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Razem:");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].MergeRight = 4;
            row.Cells[5].AddParagraph(String.Format("{0:c}", suma));
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;

            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            if (f.OdbiorcaFizyczny != null)
            {
                row.Cells[0].AddParagraph("Rabat: " + String.Format("{0:0%}", (1 - f.OdbiorcaFizyczny.Rabat)));
                row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[0].MergeRight = 4;
                row.Cells[5].AddParagraph(String.Format("{0:c}", (1-f.OdbiorcaFizyczny.Rabat)*suma));
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            }
            if (f.OdbiorcaPrawny != null)
            {
                row.Cells[0].AddParagraph("Rabat: " + String.Format("{0:0%}", (1 - f.OdbiorcaPrawny.Rabat)));
                row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[0].MergeRight = 4;
                row.Cells[5].AddParagraph(String.Format("{0:c}", suma*(1-f.OdbiorcaPrawny.Rabat)));
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            }

            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Razem: ");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].MergeRight = 4;
            row.Cells[5].AddParagraph(String.Format("{0:c}", f.Razem));
            row.Cells[5].Format.Alignment = ParagraphAlignment.Right;

        }

        // kolory
#if true
        // RGB kolory
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);
#else
    // CMYK colors
    readonly static Color tableBorder = Color.FromCmyk(100, 50, 0, 30);
    readonly static Color tableBlue = Color.FromCmyk(0, 80, 50, 30);
    readonly static Color tableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif

    }
}
