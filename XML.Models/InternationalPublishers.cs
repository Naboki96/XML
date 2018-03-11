using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "InternationalPublishers")]
    public class InternationalPublishers
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }
}