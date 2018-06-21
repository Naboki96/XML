using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Author")]
    public class Author
    {
        [XmlElement(ElementName = "Index")]
        public string Index { get; set; }
        [XmlElement(ElementName = "Surname")]
        public string AuthorName { get; set; }
        [XmlAttribute(AttributeName = "Surname")]
        public string Surname { get; set; }
    }
}