using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
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

        public string[] Items => GameLiberaryData.Select(tuple => tuple.Item1).ToArray();

        #region Authors
        
        public RelayCommand ManageAuthorsCommand => new RelayCommand(() =>
        {
            Visibility valueToSet = Visibility.Collapsed;
            Visibility opositeValue = Visibility.Collapsed;
            if (PublishersEnabled == Visibility.Collapsed)
            {
                valueToSet = Visibility.Visible;
            }
            else
            {
                opositeValue = Visibility.Collapsed;
            }

            AuthorsEnabled = valueToSet;

            PublishersEnabled = opositeValue;
            ProducersEnabled = opositeValue;
            GamesEnabled = opositeValue;
            ModificationsEnabled = opositeValue;
        });
        public RelayCommand AddAuthorCommand => new RelayCommand(() => { });
        public RelayCommand DeleteAuthorCommand => new RelayCommand(() => { });
        public RelayCommand ModifyAuthorCommand => new RelayCommand(() => { });

        private Visibility authorsEnabled;
        public Visibility AuthorsEnabled
        {
            get => authorsEnabled;
            set
            {
                authorsEnabled = value;
                RaisePropertyChanged();
            }
        }

        private string authorName;
        public string AuthorName
        {
            get => authorName;
            set
            {
                authorName = value;
                RaisePropertyChanged();
            }
        }

        private string authorSurname;
        public string AuthorSurname
        {
            get => authorSurname;
            set
            {
                authorSurname = value;
                RaisePropertyChanged();
            }
        }

        private string index;
        public string Index
        {
            get => index;
            set
            {
                index = value;
                RaisePropertyChanged();
            }
        }

        public string[] Indexes
        {
            get => new []{"123","321"};
        }

        #endregion

        #region Modifications

        private Visibility modificationsEnabled;

        public RelayCommand ManageModificationsCommand => new RelayCommand(() =>
        {
            Visibility valueToSet = Visibility.Collapsed;
            Visibility opositeValue = Visibility.Collapsed;
            if (PublishersEnabled == Visibility.Collapsed)
            {
                valueToSet = Visibility.Visible;
            }
            else
            {
                opositeValue = Visibility.Collapsed;
            }

            ModificationsEnabled = valueToSet;

            PublishersEnabled = opositeValue;
            ProducersEnabled = opositeValue;
            GamesEnabled = opositeValue;
            AuthorsEnabled = opositeValue;
        });

        public Visibility ModificationsEnabled
        {
            get => modificationsEnabled;
            set
            {
                modificationsEnabled = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Games

        private Visibility gamesEnabled;

        public RelayCommand ManageGamesCommand => new RelayCommand(() =>
        {
            Visibility valueToSet = Visibility.Collapsed;
            Visibility opositeValue = Visibility.Collapsed;
            if (PublishersEnabled == Visibility.Collapsed)
            {
                valueToSet = Visibility.Visible;
            }
            else
            {
                opositeValue = Visibility.Collapsed;
            }

            GamesEnabled = valueToSet;

            PublishersEnabled = opositeValue;
            ProducersEnabled = opositeValue;
            ModificationsEnabled = opositeValue;
            AuthorsEnabled = opositeValue;
        });

        public Visibility GamesEnabled
        {
            get => gamesEnabled;
            set
            {
                gamesEnabled = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region Producers

        private Visibility producersEnabled;

        public RelayCommand ManageProducersCommand => new RelayCommand(() =>
        {
            Visibility valueToSet = Visibility.Collapsed;
            Visibility opositeValue = Visibility.Collapsed;
            if (PublishersEnabled == Visibility.Collapsed)
            {
                valueToSet = Visibility.Visible;
            }
            else
            {
                opositeValue = Visibility.Collapsed;
            }

            ProducersEnabled = valueToSet;

            PublishersEnabled = opositeValue;
            GamesEnabled = opositeValue;
            ModificationsEnabled = opositeValue;
            AuthorsEnabled = opositeValue;
        });

        public Visibility ProducersEnabled
        {
            get => producersEnabled;
            set
            {
                producersEnabled = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Publishers

        private Visibility publishersEnabled;

        public RelayCommand ManagePublishersCommand => new RelayCommand(() =>
        {
            Visibility valueToSet = Visibility.Collapsed;
            Visibility opositeValue = Visibility.Collapsed;
            if (PublishersEnabled == Visibility.Collapsed)
            {
                valueToSet = Visibility.Visible;
            }
            else
            {
                opositeValue = Visibility.Collapsed;
            }

            PublishersEnabled = valueToSet;

            ProducersEnabled = opositeValue;
            GamesEnabled = opositeValue;
            ModificationsEnabled = opositeValue;
            AuthorsEnabled = opositeValue;
        });

        public Visibility PublishersEnabled
        {
            get => publishersEnabled;
            set
            {
                publishersEnabled = value;
                RaisePropertyChanged();
            }
        }

        #endregion

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