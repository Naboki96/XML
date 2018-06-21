using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Statistics")]
    public class Statistics
    {
        [XmlAttribute(AttributeName = "TimePlayed")]
        public string TimePlayed { get; set; }
        [XmlAttribute(AttributeName = "LastSessionDate")]
        public Date LastSessionDate { get; set; }
        [XmlAttribute(AttributeName = "Achievements")]
        public Achievements Achievements { get; set; }
    }
}