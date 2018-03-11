using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Author")]
    public class Author
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Surname")]
        public string Surname { get; set; }
        [XmlAttribute(AttributeName = "Index")]
        public string Index { get; set; }
    }
}