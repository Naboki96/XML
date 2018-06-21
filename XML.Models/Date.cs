using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Date")]
    public class Date
    {
        [XmlAttribute(AttributeName = "Day")]
        public string Day { get; set; }
        [XmlAttribute(AttributeName = "Mounth")]
        public string Mounth { get; set; }
        [XmlAttribute(AttributeName = "Year")]
        public string Year { get; set; }
    }
}