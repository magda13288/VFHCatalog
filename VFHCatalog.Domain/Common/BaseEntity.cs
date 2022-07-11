using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
namespace VFHCatalog.Domain.Common
{
    public class BaseEntity
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlElement("DateTime")]
        public DateTime DateTime { get; set; }
    }
}
