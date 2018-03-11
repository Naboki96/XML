using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "GamesList")]
    public class GamesList
    {
        [XmlElement(ElementName = "Game")]
        public List<Games> Game { get; set; }
    }
}
