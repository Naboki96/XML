using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Price")]
    public class Price
    {
        [XmlAttribute(AttributeName = "Currency")]
        public string Currency { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}