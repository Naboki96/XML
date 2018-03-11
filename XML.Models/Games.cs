using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Games")]
    public class Games
    {
        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "ProducktKey")]
        public string ProducktKey { get; set; }
    }
}