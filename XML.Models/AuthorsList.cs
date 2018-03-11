using System.Collections.Generic;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "AuthorsList")]
    public class AuthorsList
    {
        [XmlElement(ElementName = "Author")]
        public List<Author> Author { get; set; }
    }
}