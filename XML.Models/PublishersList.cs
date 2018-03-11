using System.Collections.Generic;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "PublishersList")]
    public class PublishersList
    {
        [XmlElement(ElementName = "Publisher")]
        public List<InternationalPublishers> Publisher { get; set; }
    }
}