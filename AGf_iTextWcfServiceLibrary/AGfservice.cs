using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AGf_iTextLibrary.Model;

namespace AGf_iTextWcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class AGfservice : IAGfservice
    {
        AGf_PDRrepository repository = new AGf_PDRrepository();
        public byte[] getAGf_Document(AGf_Document document)
        {
            byte[] b2r = repository.getPDF(document);
            return b2r;
        }
    }
}
