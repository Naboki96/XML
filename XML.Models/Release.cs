using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Release")]
    public class Release
    {
        [XmlElement(ElementName = "ReleaseDate")]
        public ReleaseDate ReleaseDate { get; set; }
        [XmlElement(ElementName = "Price")]
        public Price Price { get; set; }
        [XmlElement(ElementName = "Producer")]
        public Producer Producer { get; set; }
        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "ProducktKey")]
        public string ProducktKey { get; set; }
    }
}