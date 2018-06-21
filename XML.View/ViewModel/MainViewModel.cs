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
        #region DataProperties

        private Authors authorsList = new Authors();
        public Authors AuthorsList
        {
            get { return authorsList; }
            set
            {
                authorsList = value;
                RaisePropertyChanged();
            }
        }

        private GameList gamesList = new GameList();
        public GameList GamesList
        {
            get { return gamesList; }
            set
            {
                gamesList = value;
                RaisePropertyChanged();
            }
        }

        private ModificationsList modifications = new ModificationsList();
        public ModificationsList Modifications
        {
            get { return modifications; }
            set
            {
                modifications = value;
                RaisePropertyChanged();
            }
        }

        private ProducerList producersList = new ProducerList();
        public ProducerList ProducersList
        {
            get { return producersList; }
            set
            {
                producersList = value;
                RaisePropertyChanged();
            }
        }

        private PublisherList publishersList = new PublisherList();
        public PublisherList PublishersList
        {
            get { return publishersList; }
            set
            {
                publishersList = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        
        public MainViewModel()
        {
            Deserialize.Execute(null);
            SelectedAuthorIndex = Indexes[0];
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
        public RelayCommand AddAuthorCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrEmpty(AuthorName) ||
                string.IsNullOrEmpty(AuthorSurname) ||
                string.IsNullOrEmpty(AuthorIndex))
            {
                MessageBox.Show("Missing data to add new Author", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                AuthorsList.Author.Add(new Author
                {
                    AuthorName = AuthorName,
                    Index = AuthorIndex,
                    Surname = authorSurname
                });
                MessageBox.Show("Author added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        });
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

        private string authorIndex;
        public string AuthorIndex
        {
            get => authorIndex;
            set
            {
                authorIndex = value;
                RaisePropertyChanged();
            }
        }

        public string[] Indexes
        {
            get
            {
                List<string> output = new List<string> { "None" };
                output.AddRange(AuthorsList.Author.Select(author => author.Index));
                return output.ToArray();
            }
        }

        private string selectedAuthorIndex;
        public string SelectedAuthorIndex
        {
            get => selectedAuthorIndex;
            set
            {
                if (value == "None")
                {
                    AuthorIndex = AuthorName = AuthorSurname = "";
                }
                else
                {
                    Author a = AuthorsList.Author.Find(author => author.Index == value);
                    AuthorIndex = a.Index;
                    AuthorName = a.AuthorName;
                    AuthorSurname = a.Surname;
                }
                selectedAuthorIndex = value;
                RaisePropertyChanged();
            }
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

        private string modificationDate;
        public string ModificationDate
        {
            get => ModificationDate;
            set
            {
                ModificationDate = value;
                RaisePropertyChanged();
            }
        }

        private string note;
        public string Note
        {
            get => Note;
            set
            {
                Note = value;
                RaisePropertyChanged();
            }
        }

        private string authorId;
        public string AuthorId
        {
            get => AuthorId;
            set
            {
                AuthorId = value;
                RaisePropertyChanged();
            }
        }

        //public string[] AuthorID
        //{
        //    get => new[] { "123", "321" };
        //}

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

        private string image;
        public string Image
        {
            get => Image;
            set
            {
                Image = value;
                RaisePropertyChanged();
            }
        }

        private string title;
        public string Title
        {
            get => Title;
            set
            {
                Title = value;
                RaisePropertyChanged();
            }
        }

        private string productKey;
        public string ProductKey
        {
            get => ProductKey;
            set
            {
                ProductKey = value;
                RaisePropertyChanged();
            }
        }

        private Price price;
        public Price Price
        {
            get => Price;
            set
            {
                Price = value;
                RaisePropertyChanged();
            }
        }

        private string curency;
        public string Curency
        {
            get => Price.Curency;
            set
            {
                Curency = Price.Curency;
                RaisePropertyChanged();
            }
        }

        private string text;
        public string Text
        {
            get => Price.Text;
            set
            {
                Text = Price.Text;
                RaisePropertyChanged();
            }
        }

        private string ageRating;
        public string AgeRating
        {
            get => AgeRating;
            set
            {
                AgeRating = value;
                RaisePropertyChanged();
            }
        }

        private string releaseDate;
        public string ReleaseDate
        {
            get => ReleaseDate;
            set
            {
                ReleaseDate = value;
                RaisePropertyChanged();
            }
        }

        private string description;
        public string Description
        {
            get => Description;
            set
            {
                Description = value;
                RaisePropertyChanged();
            }
        }

        private string gameId;
        public string GameId
        {
            get => GameId;
            set
            {
                GameId = value;
                RaisePropertyChanged();
            }
        }

        private string genre;
        public string Genre
        {
            get => Genre;
            set
            {
                Genre = value;
                RaisePropertyChanged();
            }
        }

        private ProducerId producerId;
        public ProducerId ProducerId
        {
            get => ProducerId;
            set
            {
                ProducerId = value;
                RaisePropertyChanged();
            }
        }

        private string idref;
        public string Idref
        {
            get => ProducerId.Idref;
            set
            {
                Idref = producerId.Idref;
                RaisePropertyChanged();
            }
        }

        private PublisherId publisherId;
        public PublisherId PublisherId
        {
            get => PublisherId;
            set
            {
                PublisherId = value;
                RaisePropertyChanged();
            }
        }

        private string iIdref;
        public string IIdref
        {
            get => PublisherId.Idref;
            set
            {
                IIdref = publisherId.Idref;
                RaisePropertyChanged();
            }
        }

        private Statistics statistics;
        public Statistics Statistics
        {
            get => Statistics;
            set
            {
                Statistics = value;
                RaisePropertyChanged();
            }
        }

        private string timePlayed;
        public string TimePlayed
        {
            get => statistics.TimePlayed;
            set
            {
                TimePlayed = statistics.TimePlayed;
                RaisePropertyChanged();
            }
        }

        private string lastSessionDate;
        public string LastSessionDate
        {
            get => Statistics.LastSessionDate;
            set
            {
                LastSessionDate = statistics.LastSessionDate;
                RaisePropertyChanged();
            }
        }

        private Achievements achievements;
        public Achievements Achievements
        {
            get => Achievements;
            set
            {
                Achievements = value;
                RaisePropertyChanged();
            }
        }

        private string completed;
        public string Completed
        {
            get => Achievements.Completed;
            set
            {
                Completed = Achievements.Completed;
                RaisePropertyChanged();
            }
        }

        private string count;
        public string Count
        {
            get => Achievements.Count;
            set
            {
                Count = Achievements.Count;
                RaisePropertyChanged();
            }
        }


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

        private string producerName;
        public string ProducerName
        {
            get => ProducerName;
            set
            {
                ProducerName = value;
                RaisePropertyChanged();
            }
        }

        private string producersId;
        public string ProducersId
        {
            get => ProducersId;
            set
            {
                ProducersId = value;
                RaisePropertyChanged();
            }
        }

        private ProducedGames producedGames;
        public ProducedGames ProducedGames
        {
            get => ProducedGames;
            set
            {
                ProducedGames = value;
                RaisePropertyChanged();
            }
        }

        private List<GameId> gamesId;
        public List<GameId> GamesId
        {
            get => producedGames.GameId;
            set
            {
                GamesId = producedGames.GameId;
                RaisePropertyChanged();
            }
        }

        private Publishers publishers;
        public Publishers Publishers
        {
            get => Publishers;
            set
            {
                Publishers = value;
                RaisePropertyChanged();
            }
        }

        private List<PublisherId> publisheriId;
        public List<PublisherId> PublisheriId
        {
            get => Publishers.PublisherId;
            set
            {
                PublisheriId = Publishers.PublisherId;
                RaisePropertyChanged();
            }
        }



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

        private string publisherName;
        public string PublisherName
        {
            get => PublisherName;
            set
            {
                PublisherName = value;
                RaisePropertyChanged();
            }
        }

        private string publishersId;
        public string PublishersId
        {
            get => PublishersId;
            set
            {
                PublishersId = value;
                RaisePropertyChanged();
            }
        }

        private PublishedGames publishedGames;
        public PublishedGames PublishedGames
        {
            get => PublishedGames;
            set
            {
                PublishedGames = value;
                RaisePropertyChanged();
            }
        }

        private List<GameId> gamesiId;
        public List<GameId> GamesiId
        {
            get => PublishedGames.GameId;
            set
            {
                GamesiId = PublishedGames.GameId;
                RaisePropertyChanged();
            }
        }

        private Producers producers;
        public Producers Producers
        {
            get => Producers;
            set
            {
                Producers = value;
                RaisePropertyChanged();
            }
        }

        private List<ProducerId> produceriId;
        public List<ProducerId> ProduceriId
        {
            get => Producers.ProducerId;
            set
            {
                ProduceriId = Producers.ProducerId;
                RaisePropertyChanged();
            }
        }

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