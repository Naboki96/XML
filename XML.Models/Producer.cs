using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Producer")]
    public class Producer
    {

        [XmlElement(ElementName = "ProducerName")]
        public string ProducerName { get; set; }
        [XmlElement(ElementName = "ProducedGames")]
        public ProducedGames ProducedGames { get; set; }
        [XmlElement(ElementName = "Publishers")]
        public Publishers Publishers { get; set; }
        [XmlAttribute(AttributeName = "ProducerId")]
        public string ProducerId { get; set; }
    }
}