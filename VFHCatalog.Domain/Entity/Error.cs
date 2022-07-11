using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.Domain.Common;
using System.Xml.Serialization;

namespace VFHCatalog.Domain.Entity
{
    public class Error:BaseEntity
    {
        [XmlElement("Message")]
        public string Message { get; set; }

        [XmlElement("Source")]
        public string Source { get; set; }

        [XmlElement("StackTrace")]
        public string StackTrace { get; set; }

        //[XmlElement("InnerException")]
        //public string InnerException { get; set; }

       
    }
}
