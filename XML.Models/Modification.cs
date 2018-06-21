using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Modification")]
    public class Modification
    {
        [XmlElement(ElementName = "AuthorId")]
        public string AuthorId { get; set; }
        [XmlElement(ElementName = "ModificationDate")]
        public Date ModificationDate { get; set; }
        [XmlAttribute(AttributeName = "Note")]
        public string Note { get; set; }
    }
}
