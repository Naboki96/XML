using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "ZdefiniowanyDział")]
    public class ZdefiniowanyDział
    {
        [XmlAttribute(AttributeName = "Nazwa")]
        public string Nazwa { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "ListaDziałów")]
    public class ListaDziałów
    {
        [XmlElement(ElementName = "ZdefiniowanyDział")]
        public List<ZdefiniowanyDział> ZdefiniowanyDział { get; set; }
    }

    [XmlRoot(ElementName = "DataZatrudnienia")]
    public class DataZatrudnienia
    {
        [XmlAttribute(AttributeName = "Dzień")]
        public string Dzień { get; set; }
        [XmlAttribute(AttributeName = "Miesiąc")]
        public string Miesiąc { get; set; }
        [XmlAttribute(AttributeName = "Rok")]
        public string Rok { get; set; }
    }

    [XmlRoot(ElementName = "Płaca")]
    public class Płaca
    {
        [XmlAttribute(AttributeName = "Waluta")]
        public string Waluta { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Etat")]
    public class Etat
    {
        [XmlAttribute(AttributeName = "RodzajUmowy")]
        public string RodzajUmowy { get; set; }
    }

    [XmlRoot(ElementName = "Zatrudnienie")]
    public class Zatrudnienie
    {
        [XmlElement(ElementName = "DataZatrudnienia")]
        public DataZatrudnienia DataZatrudnienia { get; set; }
        [XmlElement(ElementName = "Stanowisko")]
        public string Stanowisko { get; set; }
        [XmlElement(ElementName = "Płaca")]
        public Płaca Płaca { get; set; }
        [XmlElement(ElementName = "Etat")]
        public Etat Etat { get; set; }
        [XmlAttribute(AttributeName = "IdOsoby")]
        public string IdOsoby { get; set; }
        [XmlAttribute(AttributeName = "Imię")]
        public string Imię { get; set; }
        [XmlAttribute(AttributeName = "Nazwisko")]
        public string Nazwisko { get; set; }
    }

    [XmlRoot(ElementName = "Dział")]
    public class Dział
    {
        [XmlElement(ElementName = "Zatrudnienie")]
        public List<Zatrudnienie> Zatrudnienie { get; set; }
        [XmlAttribute(AttributeName = "IdDziału")]
        public string IdDziału { get; set; }
    }

    [XmlRoot(ElementName = "Autor")]
    public class Autor
    {
        [XmlElement(ElementName = "Imię")]
        public string Imię { get; set; }
        [XmlElement(ElementName = "Nazwisko")]
        public string Nazwisko { get; set; }
        [XmlAttribute(AttributeName = "Indeks")]
        public string Indeks { get; set; }
    }

    [XmlRoot(ElementName = "Autorzy")]
    public class Autorzy
    {
        [XmlElement(ElementName = "Autor")]
        public List<Autor> Autor { get; set; }
    }

    [XmlRoot(ElementName = "Firma")]
    public class Firma
    {
        [XmlElement(ElementName = "ListaOsób")]
        public ListaOsób ListaOsób { get; set; }
        [XmlElement(ElementName = "ListaDziałów")]
        public ListaDziałów ListaDziałów { get; set; }
        [XmlElement(ElementName = "Dział")]
        public List<Dział> Dział { get; set; }
        [XmlElement(ElementName = "Autorzy")]
        public Autorzy Autorzy { get; set; }
    }

    public class ListaOsób
    {
    }
}

