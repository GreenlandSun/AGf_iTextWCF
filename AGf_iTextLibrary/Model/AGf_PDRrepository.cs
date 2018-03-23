using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGf_iTextLibrary.Model
{
    public class AGf_PDRrepository
    {
        /// <summary>
        /// Passes data fra WCF poco objects to iTextSharp objects and refurns a PDF file
        /// </summary>
        Font headerFont = FontFactory.GetFont("Verdana", 10, BaseColor.WHITE);
        Font rowfont = FontFactory.GetFont("Verdana", 10, BaseColor.DARK_GRAY);
        Font _font = FontFactory.GetFont("Verdana", 8, BaseColor.DARK_GRAY);
        Font rFont = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.NORMAL);
        Font mFont = new Font(Font.FontFamily.TIMES_ROMAN, 6, Font.NORMAL);
        Font rbFont = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD);
        Font boldFont = new Font(Font.FontFamily.TIMES_ROMAN, 18, Font.BOLD);

        public byte[] getPDF(AGf_Document data)
        {
            MemoryStream ms = new MemoryStream();
            Document document = new Document(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            writer.PageEvent = new PDFFooter(data.Header, data.Footer);
            document.AddTitle(data.Title);
            document.AddAuthor(data.Auther);
            document.AddCreator("agFeatures powered by iTextSharp");
            document.AddKeywords(data.Keywords);
            document.AddSubject(data.Subject);
            document.Open();
            addIconTable(document, data.Logopath, data.Lefttext);
            addtables(document, data.Tabels);
            document.Close();
            writer.Close();
            return ms.ToArray();// document;
        }

        private void addIconTable(Document document, string Logopath, string LeftText)
        {
            if (Logopath.Trim() != "")
            {
                if (Logopath.Trim().ToLower() == "default")
                    Logopath = ConfigurationManager.AppSettings["defaultlogo"];
                PdfPTable tworows = new PdfPTable(2);
                tworows.WidthPercentage = 100;
                PdfPCell cell = new PdfPCell(new Phrase(LeftText, rFont));
                cell.BorderColor = BaseColor.WHITE;
                cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
                tworows.AddCell(cell);
                Image gif = Image.GetInstance(Logopath);
                gif.ScalePercent(65);
                cell = new PdfPCell(gif);
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.BorderColor = BaseColor.WHITE;
                tworows.AddCell(cell);
                document.Add(tworows);
            }
        }

        private void addtables(Document document, List<AGf_PDFTable> tabels)
        {
            tabels = tabels.OrderByDescending(o => o.Order).ToList();
            foreach(AGf_PDFTable tabel in tabels)
            {
                PdfPTable tab = new PdfPTable(tabel.MetaWidth);
                tab.WidthPercentage = tabel.Widthpercent;
                tab.DefaultCell.BorderColor = BaseColor.WHITE;
                if(tabel.CellsOrEmbeddedTables.Trim().ToLower() == "tabels" && tabel.MetaWidth.Length == 2)
                {
                    PdfPTable embTable = new PdfPTable(tabel.LeftTable.MetaWidth);
                    addcells(embTable, tabel.LeftTable.Cells);
                    tab.AddCell(embTable);
                    embTable = new PdfPTable(tabel.RightTable.MetaWidth);
                    addcells(embTable, tabel.RightTable.Cells);
                    tab.AddCell(embTable);
                }
                else
                {
                    addcells(tab, tabel.Cells);
                }
                document.Add(tab);
                if(tabel.EndString.Trim().ToLower() != "non")
                    document.Add(new Paragraph(tabel.EndString)); //space linie
            }
        }

        private void addcells(PdfPTable table, List<AGf_PDFCell> cells)
        {
            cells = cells.OrderByDescending(o => o.Order).ToList();
            foreach(AGf_PDFCell c in cells)
            {
                Phrase phrase = null;
                switch (c.Font.Trim().ToLower())
                {
                    case "headerFont":
                        phrase = new Phrase(c.Phrase, headerFont);
                        break;
                    case "rowfont":
                        phrase = new Phrase(c.Phrase, rowfont);
                        break;
                    case "_font":
                        phrase = new Phrase(c.Phrase, _font);
                        break;
                    case "mFont":
                        phrase = new Phrase(c.Phrase, mFont);
                        break;
                    case "rbFont":
                        phrase = new Phrase(c.Phrase, rbFont);
                        break;
                    case "boldFont":
                        phrase = new Phrase(c.Phrase, boldFont);
                        break;
                    default:
                        phrase = new Phrase(c.Phrase, rFont);
                        break;
                }
                if (phrase != null)
                {
                    PdfPCell cell = new PdfPCell(phrase);
                    switch (c.HAlign.Trim().ToLower())
                    {
                        case "right":
                            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                            break;
                        case "center":
                            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            break;
                    }
                    switch (c.VAlign.Trim().ToLower())
                    {
                        case "top":
                            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            break;
                    }
                    switch (c.Bordercolor.Trim().ToLower())
                    {
                        case "white":
                            cell.BorderColor = BaseColor.WHITE;
                            break;
                    }
                    switch (c.Backgroundcolor.Trim().ToLower())
                    {
                        case "light_grey":
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            break;
                    }
                    table.AddCell(cell);
                }
            }
        }
    }

    public class PDFFooter : PdfPageEventHelper
    {
        private string header, footer;
        public PDFFooter(string Header, string Footer)
        {
            header = Header;
            footer = Footer;
        }
        private Font _font = FontFactory.GetFont("Verdana", 8, BaseColor.DARK_GRAY);
        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            //tabFot.SpacingAfter = 10F;
            PdfPCell cell;
            tabFot.TotalWidth = 500F;
            cell = new PdfPCell(new Phrase(header, _font));
            cell.BorderWidth = 0;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 50, document.Top, writer.DirectContent);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            tabFot.TotalWidth = 500F;
            PdfPCell cell;

            cell = new PdfPCell(new Phrase(footer, _font));
            cell.BorderWidth = 0;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 50, document.Bottom, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}
