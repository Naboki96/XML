using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Producer")]
    public class Producer
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }
}