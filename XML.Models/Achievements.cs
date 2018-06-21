using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Achievements")]
    public class Achievements
    {
        [XmlAttribute(AttributeName = "Count")]
        public string Count { get; set; }
        [XmlAttribute(AttributeName = "Completed")]
        public string Completed { get; set; }
    }
}