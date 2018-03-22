using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AGf_iTextLibrary.Model;

namespace AGf_iTextWcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAGfservice
    {
        [OperationContract]
        byte[] getAGf_Document(AGf_Document document);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "AGf_iTextWcfServiceLibrary.ContractType".
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public AGf_PDFCell agf_pdfcell
        {
            get { return agf_pdfcell; }
            set { agf_pdfcell = value; }
        }
        [DataMember]
        public AGf_PDFTable agf_pdftable
        {
            get { return agf_pdftable; }
            set { agf_pdftable = value; }
        }
        [DataMember] AGf_Document agf_document
        {
            get { return agf_document; }
            set { agf_document = value; }
        }
        [DataMember] byte[] bytearray
        {
            get { return bytearray; }
            set { bytearray = value; }
        }
    }
}
