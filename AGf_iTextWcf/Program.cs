using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGf_iTextLibrary.Model;
using System.IO;

namespace AGf_iTextWcf
{
    class Program
    {
        static void Main(string[] args)
        {
            AGf_PDRrepository repository = new AGf_PDRrepository();
            AGf_Document doc = new AGf_Document
            {
                Auther = "eee",
                Footer = "rrr",
                Header = "ttt",
                Keywords = "fff",
                Lefttext = "ddd",
                Logopath = "default",
                Subject = "www",
                Title = "xxx",
                Tabels = new List<AGf_PDFTable>()
            };
            AGf_PDFTable tab = new AGf_PDFTable
            {
                MetaWidth = new float[] { 1 },
                CellsOrEmbeddedTables = "cells",
                Cells = new List<AGf_PDFCell>(),
                EndString = "non",
                LeftTable = null,
                RightTable = null,
                Order = 1,
                Widthpercent = 100
            };
            AGf_PDFCell cel = new AGf_PDFCell
            {
                Backgroundcolor = "",
                Bordercolor = "white",
                Font = "",
                HAlign = "",
                VAlign = "",
                Order = 1,
                Phrase = "Hejsa"
            };
            tab.Cells.Add(cel);
            doc.Tabels.Add(tab);

            byte[] d = repository.getPDF(doc);

            using (FileStream fs = new FileStream(@"c:\util\output1.pdf", FileMode.OpenOrCreate))
            {
                fs.Write(d, 0, d.Length);

            }


        }
    }
}
