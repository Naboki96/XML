using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Game")]
    public class Game
    {
        [XmlAttribute(AttributeName = "GameId")]
        public string GameId { get; set; }
        [XmlAttribute(AttributeName = "Genre")]
        public string Genre { get; set; }
        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }
        [XmlAttribute(AttributeName = "ProductKey")]
        public string ProductKey { get; set; }
        [XmlAttribute(AttributeName = "Price")]
        public Price Price { get; set; }
        [XmlAttribute(AttributeName = "AgeRating")]
        public string AgeRating { get; set; }
        [XmlAttribute(AttributeName = "ReleaseDate")]
        public Date ReleaseDate { get; set; }
        [XmlAttribute(AttributeName = "ProducerId")]
        public string ProducerId { get; set; }
        [XmlAttribute(AttributeName = "PublisherId")]
        public string PublisherId { get; set; }
        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }
        [XmlAttribute(AttributeName = "Statistics")]
        public Statistics Statistics { get; set; }
    }
}