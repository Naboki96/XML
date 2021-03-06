﻿/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Xml2CSharp
{
    [XmlRoot(ElementName = "Author", Namespace = "http://www.w3schools.com")]
    public class Author
    {
        [XmlElement(ElementName = "AuthorName", Namespace = "http://www.w3schools.com")]
        public string AuthorName { get; set; }
        [XmlElement(ElementName = "Surname", Namespace = "http://www.w3schools.com")]
        public string Surname { get; set; }
        [XmlAttribute(AttributeName = "Index")]
        public string Index { get; set; }

        public override string ToString()
        {
            return $"Author: {AuthorName} {Surname}, Index: {Index}.";
        }
    }

    [XmlRoot(ElementName = "Authors", Namespace = "http://www.w3schools.com")]
    public class Authors
    {
        [XmlElement(ElementName = "Author", Namespace = "http://www.w3schools.com")]
        public List<Author> Author { get; set; }
    }

    [XmlRoot(ElementName = "Modification", Namespace = "http://www.w3schools.com")]
    public class Modification
    {
        [XmlElement(ElementName = "ModificationDate", Namespace = "http://www.w3schools.com")]
        public string ModificationDate { get; set; }
        [XmlElement(ElementName = "Note", Namespace = "http://www.w3schools.com")]
        public string Note { get; set; }
        [XmlAttribute(AttributeName = "AuthorId")]
        public string AuthorId { get; set; }

        public override string ToString()
        {
            return $"Modification: {Note}, by: {AuthorId}, Date: {ModificationDate}.";
        }
    }

    [XmlRoot(ElementName = "ModificationsList", Namespace = "http://www.w3schools.com")]
    public class ModificationsList
    {
        [XmlElement(ElementName = "Modification", Namespace = "http://www.w3schools.com")]
        public List<Modification> Modification { get; set; }
    }

    [XmlRoot(ElementName = "Price", Namespace = "http://www.w3schools.com")]
    public class Price
    {
        [XmlAttribute(AttributeName = "Curency")]
        public string Curency { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ProducerId", Namespace = "http://www.w3schools.com")]
    public class ProducerId
    {
        [XmlAttribute(AttributeName = "idref")]
        public string Idref { get; set; }

        public override string ToString()
        {
            return Idref;
        }
    }

    [XmlRoot(ElementName = "PublisherId", Namespace = "http://www.w3schools.com")]
    public class PublisherId
    {
        [XmlAttribute(AttributeName = "idref")]
        public string Idref { get; set; }

        public override string ToString()
        {
            return Idref;
        }
    }

    [XmlRoot(ElementName = "Achievements", Namespace = "http://www.w3schools.com")]
    public class Achievements
    {
        [XmlElement(ElementName = "Completed", Namespace = "http://www.w3schools.com")]
        public string Completed { get; set; }
        [XmlAttribute(AttributeName = "Count")]
        public string Count { get; set; }
    }

    [XmlRoot(ElementName = "Statistics", Namespace = "http://www.w3schools.com")]
    public class Statistics
    {
        [XmlElement(ElementName = "TimePlayed", Namespace = "http://www.w3schools.com")]
        public string TimePlayed { get; set; }
        [XmlElement(ElementName = "LastSessionDate", Namespace = "http://www.w3schools.com")]
        public string LastSessionDate { get; set; }
        [XmlElement(ElementName = "Achievements", Namespace = "http://www.w3schools.com")]
        public Achievements Achievements { get; set; }
    }

    [XmlRoot(ElementName = "Game", Namespace = "http://www.w3schools.com")]
    public class Game
    {
        [XmlElement(ElementName = "Image", Namespace = "http://www.w3schools.com")]
        public string Image { get; set; }
        [XmlElement(ElementName = "Title", Namespace = "http://www.w3schools.com")]
        public string Title { get; set; }
        [XmlElement(ElementName = "ProductKey", Namespace = "http://www.w3schools.com")]
        public string ProductKey { get; set; }
        [XmlElement(ElementName = "Price", Namespace = "http://www.w3schools.com")]
        public Price Price { get; set; }
        [XmlElement(ElementName = "AgeRating", Namespace = "http://www.w3schools.com")]
        public string AgeRating { get; set; }
        [XmlElement(ElementName = "ReleaseDate", Namespace = "http://www.w3schools.com")]
        public string ReleaseDate { get; set; }
        [XmlElement(ElementName = "ProducerId", Namespace = "http://www.w3schools.com")]
        public ProducerId ProducerId { get; set; }
        [XmlElement(ElementName = "PublisherId", Namespace = "http://www.w3schools.com")]
        public PublisherId PublisherId { get; set; }
        [XmlElement(ElementName = "Description", Namespace = "http://www.w3schools.com")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Statistics", Namespace = "http://www.w3schools.com")]
        public Statistics Statistics { get; set; }
        [XmlAttribute(AttributeName = "GameId")]
        public string GameId { get; set; }
        [XmlAttribute(AttributeName = "Genre")]
        public string Genre { get; set; }

        public override string ToString()
        {
            return $"Game: {Title}, {Genre},  {GameId}," +
                   $"{Environment.NewLine} {Image}, {ProductKey},  Price: {Price}, {AgeRating},  ReleaseDate: {ReleaseDate}" +
                   $"{Environment.NewLine} Publisher: {PublisherId}, Producer: {ProducerId}" +
                   $"{Environment.NewLine} {Description}" +
                   $"{Environment.NewLine} Statistics: {Statistics}";
        }
    }

    [XmlRoot(ElementName = "GameList", Namespace = "http://www.w3schools.com")]
    public class GameList
    {
        [XmlElement(ElementName = "Game", Namespace = "http://www.w3schools.com")]
        public List<Game> Game { get; set; }
    }

    [XmlRoot(ElementName = "GameId", Namespace = "http://www.w3schools.com")]
    public class GameId
    {
        [XmlAttribute(AttributeName = "idref")]
        public string Idref { get; set; }

        public override string ToString()
        {
            return Idref;
        }
    }

    [XmlRoot(ElementName = "ProducedGames", Namespace = "http://www.w3schools.com")]
    public class ProducedGames
    {
        public ProducedGames()
        {
            GameId = new List<GameId>();
        }

        public ProducedGames(string producedGames)
        {
            GameId = producedGames.Split(',', ' ').Select(s => new GameId { Idref = s }).ToList();
        }

        [XmlElement(ElementName = "GameId", Namespace = "http://www.w3schools.com")]
        public List<GameId> GameId { get; set; }

        public override string ToString()
        {
            return string.Join(", ", GameId);
        }
    }

    [XmlRoot(ElementName = "Publishers", Namespace = "http://www.w3schools.com")]
    public class Publishers
    {
        public Publishers()
        {
            PublisherId = new List<PublisherId>();
        }

        public Publishers(string producerPublishers)
        {
            PublisherId = producerPublishers.Split(',', ' ').Select(s => new PublisherId { Idref = s }).ToList();
        }

        [XmlElement(ElementName = "PublisherId", Namespace = "http://www.w3schools.com")]
        public List<PublisherId> PublisherId { get; set; }

        public override string ToString()
        {
            return string.Join(", ", PublisherId);
        }
    }

    [XmlRoot(ElementName = "Producer", Namespace = "http://www.w3schools.com")]
    public class Producer
    {
        [XmlElement(ElementName = "ProducerName", Namespace = "http://www.w3schools.com")]
        public string ProducerName { get; set; }
        [XmlElement(ElementName = "ProducedGames", Namespace = "http://www.w3schools.com")]
        public ProducedGames ProducedGames { get; set; }
        [XmlElement(ElementName = "Publishers", Namespace = "http://www.w3schools.com")]
        public Publishers Publishers { get; set; }
        [XmlAttribute(AttributeName = "ProducerId")]
        public string ProducerId { get; set; }

        public override string ToString()
        {
            return $"Producer: {ProducerName} {ProducerId}. " +
                   $"{Environment.NewLine} Games: {ProducedGames.ToString()}. " +
                   $"{Environment.NewLine} Publishers: {Publishers.ToString()}";
        }

    }

    [XmlRoot(ElementName = "ProducerList", Namespace = "http://www.w3schools.com")]
    public class ProducerList
    {
        [XmlElement(ElementName = "Producer", Namespace = "http://www.w3schools.com")]
        public List<Producer> Producer { get; set; }
    }

    [XmlRoot(ElementName = "PublishedGames", Namespace = "http://www.w3schools.com")]
    public class PublishedGames
    {
        public PublishedGames()
        {
            GameId = new List<GameId>();
        }

        public PublishedGames(string publishedGames)
        {
            GameId = publishedGames.Split(',', ' ').Select(s => new GameId {Idref = s}).ToList();
        }

        [XmlElement(ElementName = "GameId", Namespace = "http://www.w3schools.com")]
        public List<GameId> GameId { get; set; }

        public override string ToString()
        {
            return string.Join(", ", GameId);
        }
    }

    [XmlRoot(ElementName = "Producers", Namespace = "http://www.w3schools.com")]
    public class Producers
    {
        public Producers()
        {
            ProducerId = new List<ProducerId>();
        }

        public Producers(string publisherProducers)
        {
            ProducerId = publisherProducers.Split(',', ' ').Select(s => new ProducerId {Idref = s}).ToList();
        }

        [XmlElement(ElementName = "ProducerId", Namespace = "http://www.w3schools.com")]
        public List<ProducerId> ProducerId { get; set; }

        public override string ToString()
        {
            return string.Join(", ", ProducerId);
        }
    }

    [XmlRoot(ElementName = "Publisher", Namespace = "http://www.w3schools.com")]
    public class Publisher
    {
        [XmlElement(ElementName = "PublisherName", Namespace = "http://www.w3schools.com")]
        public string PublisherName { get; set; }
        [XmlElement(ElementName = "PublishedGames", Namespace = "http://www.w3schools.com")]
        public PublishedGames PublishedGames { get; set; }
        [XmlElement(ElementName = "Producers", Namespace = "http://www.w3schools.com")]
        public Producers Producers { get; set; }
        [XmlAttribute(AttributeName = "PublisherId")]
        public string PublisherId { get; set; }

        public override string ToString()
        {
            return $"Publisher: {PublisherName} {PublisherId}. " +
                   $"{Environment.NewLine} Games: {PublishedGames}. " +
                   $"{Environment.NewLine} Producers: {Producers}";
        }
    }

    [XmlRoot(ElementName = "PublisherList", Namespace = "http://www.w3schools.com")]
    public class PublisherList
    {
        [XmlElement(ElementName = "Publisher", Namespace = "http://www.w3schools.com")]
        public List<Publisher> Publisher { get; set; }
    }

    [XmlRoot(ElementName = "GameLibrary", Namespace = "http://www.w3schools.com")]
    public class GameLibrary
    {
        [XmlElement(ElementName = "Authors", Namespace = "http://www.w3schools.com")]
        public Authors Authors { get; set; }
        [XmlElement(ElementName = "ModificationsList", Namespace = "http://www.w3schools.com")]
        public ModificationsList ModificationsList { get; set; }
        [XmlElement(ElementName = "GameList", Namespace = "http://www.w3schools.com")]
        public GameList GameList { get; set; }
        [XmlElement(ElementName = "ProducerList", Namespace = "http://www.w3schools.com")]
        public ProducerList ProducerList { get; set; }
        [XmlElement(ElementName = "PublisherList", Namespace = "http://www.w3schools.com")]
        public PublisherList PublisherList { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
    }

}
