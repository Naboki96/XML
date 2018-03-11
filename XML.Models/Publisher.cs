using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Publisher")]
    public class Publisher
    {
        [XmlElement(ElementName = "Release")]
        public List<Release> Release { get; set; }
        [XmlAttribute(AttributeName = "PublisherId")]
        public string PublisherId { get; set; }
    }
}

