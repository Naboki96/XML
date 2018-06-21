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
        public Authors AuthorsList = new Authors();
        public GameList GamesList = new GameList();
        public ModificationsList Modifications = new ModificationsList();
        public ProducerList ProducersList = new ProducerList();
        public PublisherList PublishersList = new PublisherList();

        public MainViewModel()
        {
            
        }

        public RelayCommand Deserialize => new RelayCommand(() =>
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(GameLibrary));
            TextReader reader = new StreamReader(@"./File1.xml");
            GameLibrary data = deserializer.Deserialize(reader) as GameLibrary;
            reader.Close();

            AuthorsList = data.Authors;
            GamesList = data.GameList;
            Modifications = data.ModificationsList;
            ProducersList = data.ProducerList;
            PublishersList = data.PublisherList;
        });

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