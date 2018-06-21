using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Publisher")]
    public class Publisher
    {
        [XmlElement(ElementName = "PublisherName")]
        public string PublisherName { get; set; }
        [XmlElement(ElementName = "PublishedGames")]
        public PublishedGames PublishedGames { get; set; }
        [XmlElement(ElementName = "Producers")]
        public Producers Producers { get; set; }
        [XmlAttribute(AttributeName = "PublisherId")]
        public string PublisherId { get; set; }
    }
}

