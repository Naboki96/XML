using System.Collections.Generic;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "GameLibrary")]
    public class GameLibrary
    {
        [XmlElement(ElementName = "GameList")]
        public GamesList GamesList { get; set; }
        [XmlElement(ElementName = "PublishersList")]
        public PublishersList PublishersList { get; set; }
        [XmlElement(ElementName = "Publisher")]
        public List<Publisher> Publisher { get; set; }
        [XmlElement(ElementName = "AuthorsList")]
        public AuthorsList AuthorsList { get; set; }
    }
}