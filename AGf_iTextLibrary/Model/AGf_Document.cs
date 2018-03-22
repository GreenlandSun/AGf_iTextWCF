using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AGf_iTextLibrary.Model
{
    /// <summary>
    /// Poco objects to deliver data from WCF to iTextSharp Repository
    /// </summary>
    public class AGf_Document
    {
        /// <summary>
        /// Document metatata and Table info
        /// </summary>
        public string Header { get; set; }
        public string Footer { get; set; }

        public string Logopath { get; set; }
        public string Lefttext { get; set; }

        //pdf metadata
        public string Title { get; set; }
        public string Auther { get; set; }
        public string Keywords { get; set; }
        public string Subject { get; set; }
        
        //Content
        public List<AGf_PDFTable> Tabels { get; set; }

        
    }

    public class AGf_PDFTable
    {
        public int Order { get; set; }
        public float[] MetaWidth { get; set; }
        public int Widthpercent { get; set; }
        public string CellsOrEmbeddedTables { get; set; }
        public List<AGf_PDFCell> Cells { get; set; }
        //End with spacing or not
        public string EndString { get; set; }

        //Embedded Tabels
        public AGf_PDFTable LeftTable { get; set; }
        public AGf_PDFTable RightTable { get; set; }
    }

    public class AGf_PDFCell
    {
        //Most test goes here
        public int Order { get; set; }
        public string Phrase { get; set; }
        public string Font { get; set; }
        public string HAlign { get; set; }
        public string VAlign { get; set; }
        public string Bordercolor { get; set; }
        public string Backgroundcolor { get; set; }
    }
}
