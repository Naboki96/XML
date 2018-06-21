using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "GameLibrary")]
    public class GameLibrary
    {
        [XmlElement(ElementName = "Authors")]
        public List<Author> Authors { get; set; }
        [XmlElement(ElementName = "ModificationsList")]
        public List<Modification> ModificationsList { get; set; }
        [XmlElement(ElementName = "GameList")]
        public List<Game> GameList { get; set; }
        [XmlElement(ElementName = "ProducerList")]
        public List<Producer> ProducerList { get; set; }
        [XmlElement(ElementName = "PublisherList")]
        public List<Publisher> PublisherList { get; set; }
    }
}