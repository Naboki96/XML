using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xml2CSharp;

namespace XML.View.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public List<Tuple<string, XmlType, object>> GameLiberaryData = new List<Tuple<string, XmlType, object>>();

        public MainViewModel()
        {
            
        }

        public RelayCommand Deserialize => new RelayCommand(() =>
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(GameLibrary));
            TextReader reader = new StreamReader(@"./File1.xml");
            GameLibrary data = deserializer.Deserialize(reader) as GameLibrary;
            reader.Close();

            GameLiberaryData.AddRange(
                data.Authors.Author.Select(
                    author => new Tuple<string, XmlType, object>(author.AuthorName + author.Surname, XmlType.Author, author)));

            GameLiberaryData.AddRange(
                data.GameList.Game.Select(
                    game => new Tuple<string, XmlType, object>(game.Title, XmlType.Game, game)));

            GameLiberaryData.AddRange(
                data.ModificationsList.Modification.Select(
                    modification => new Tuple<string, XmlType, object>(modification.AuthorId, XmlType.Modification, modification)));

            GameLiberaryData.AddRange(
                data.ProducerList.Producer.Select(
                    producer => new Tuple<string, XmlType, object>(producer.ProducerName, XmlType.Producer, producer)));

            GameLiberaryData.AddRange(
                data.PublisherList.Publisher.Select(
                    publisher => new Tuple<string, XmlType, object>(publisher.PublisherName, XmlType.Publisher, publisher)));
        });

        public string[] Items => GameLiberaryData.Select(tuple => tuple.Item1).ToArray();

    }

    public enum XmlType
    {
        Author,
        Modification,
        Game,
        Producer,
        Publisher
    }
}