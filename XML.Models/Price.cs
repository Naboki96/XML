using System;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Price")]
    public class Price
    {
        [XmlAttribute(AttributeName = "Currency")]
        public String Currency { get; set; }
        [XmlText]
        public double valaue { get; set; }
    }
}