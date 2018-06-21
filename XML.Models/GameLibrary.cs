using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "GameLibrary")]
    public class GameLibrary
    {
        [XmlElement(ElementName = "Authors")]
        public ArrayList Authors = new ArrayList();
        [XmlElement(ElementName = "ModificationsList")]
        public ArrayList ModificationsList = new ArrayList();
        [XmlElement(ElementName = "GameList")]
        public ArrayList GameList = new ArrayList();
        [XmlElement(ElementName = "ProducerList")]
        public ArrayList ProducerList = new ArrayList();
        [XmlElement(ElementName = "PublisherList")]
        public ArrayList PublisherList = new ArrayList();
    }
}