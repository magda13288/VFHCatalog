using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VFHCatalog.Domain.Common
{
    public class BasePlantEntity
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlElement("IdType")]
        public int IdType { get; set; }
        [XmlElement("GroupId")]
        public int GroupId { get; set; }
        [XmlElement("NameId")]
        public int NameId { get; set; }
        [XmlElement("FullName")]
        public string FullName { get; set; }
      
    }

}
