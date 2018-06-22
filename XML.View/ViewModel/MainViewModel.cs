using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xml2CSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace XML.View.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region DataProperties

        private GameLibrary gameLibrary;

        public ObservableCollection<Author> AuthorsList
        {
            get { return new ObservableCollection<Author>(gameLibrary.Authors.Author);}
            set
            {
                gameLibrary.Authors.Author = value.ToList();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Game> GamesList
        {
            get { return new ObservableCollection<Game>(gameLibrary.GameList.Game); }
            set
            {
                gameLibrary.GameList.Game = value.ToList();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Modification> Modifications
        {
            get { return new ObservableCollection<Modification>(gameLibrary.ModificationsList.Modification); }
            set
            {
                gameLibrary.ModificationsList.Modification = value.ToList();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Producer> ProducersList
        {
            get { return new ObservableCollection<Producer>(gameLibrary.ProducerList.Producer); }
            set
            {
                gameLibrary.ProducerList.Producer = value.ToList();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Publisher> PublishersList
        {
            get { return new ObservableCollection<Publisher>(gameLibrary.PublisherList.Publisher); }
            set
            {
                gameLibrary.PublisherList.Publisher = value.ToList();
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

            gameLibrary = data;
        });

        public void CreatePDF(object obj)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream("PDF.pdf", FileMode.Create));
            doc.Open();
            Paragraph paragraph = new Paragraph();
            doc.Add(paragraph);
            doc.Close();
        }

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
                List <Author> output = AuthorsList.ToList();
                output.Add(new Author
                {
                    AuthorName = AuthorName,
                    Index = AuthorIndex,
                    Surname = authorSurname
                });
                AuthorsList = new ObservableCollection<Author>(output);
                MessageBox.Show("Author added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                RaisePropertyChanged("Indexes");
                SelectedAuthorIndex = Indexes[0];
            }
        });
        public RelayCommand DeleteAuthorCommand => new RelayCommand(() =>
        {
            if (SelectedAuthorIndex == "None")
            {
                MessageBox.Show("Select Index of Author you want to delete.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < AuthorsList.Count; index++)
            {
                if (AuthorsList[index].Index == SelectedAuthorIndex)
                {
                    List<Author> output = AuthorsList.ToList();
                    output.RemoveAt(index);
                    AuthorsList = new ObservableCollection<Author>(output);
                    MessageBox.Show("Author deleted successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("Indexes");
                    SelectedAuthorIndex = Indexes[0];
                    return;
                }
            }
        });
        public RelayCommand ModifyAuthorCommand => new RelayCommand(() =>
        {
            if (SelectedAuthorIndex == "None")
            {
                MessageBox.Show("Select Index of Author you want to modify.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < AuthorsList.Count; index++)
            {
                if (AuthorsList[index].Index == SelectedAuthorIndex)
                {
                    var output = AuthorsList.ToList();
                    output[index] = new Author
                    {
                        AuthorName = AuthorName,
                        Index = AuthorIndex,
                        Surname = authorSurname
                    };
                    AuthorsList = new ObservableCollection<Author>(output);
                    MessageBox.Show("Author Modified successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("Indexes");
                    SelectedAuthorIndex = Indexes[0];
                    return;
                }
            }
        });

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
                output.AddRange(AuthorsList.Select(author => author.Index));
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
                    Author a = AuthorsList.First(author => author.Index == value);
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
        public RelayCommand AddModificationCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrEmpty(ModificationDate) ||
                string.IsNullOrEmpty(Note) ||
                string.IsNullOrEmpty(AuthorId))
            {
                MessageBox.Show("Missing data to add new Modification", "Modification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var output = Modifications.ToList();
                output.Add(new Modification
                {
                    ModificationDate = ModificationDate,
                    Note = Note,
                    AuthorId = AuthorId
                });
                Modifications = new ObservableCollection<Modification>(output);
                MessageBox.Show("Modification added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        });

        public RelayCommand DeleteModificationCommand => new RelayCommand(() =>
        {
            if (SelectedModificationAuthorId == "None")
            {
                MessageBox.Show("Select Author of Modification you want to delete.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < Modifications.Count; index++)
            {
                if (Modifications[index].AuthorId == SelectedModificationAuthorId)
                {
                    List<Modification> output = Modifications.ToList();
                    output.RemoveAt(index);
                    Modifications = new ObservableCollection<Modification>(output);
                    MessageBox.Show("Modification deleted successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("Indexes");
                    SelectedModificationAuthorId = Indexes[0];
                    return;
                }
            }
        });
        public RelayCommand ModifyModificationCommand => new RelayCommand(() =>
        {
            if (SelectedModificationAuthorId == "None")
            {
                MessageBox.Show("Select Author of Modification you want to modify.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < Modifications.Count; index++)
            {
                if (Modifications[index].AuthorId == SelectedModificationAuthorId)
                {
                    var output = Modifications.ToList();
                    output[index] = new Modification()
                    {
                        ModificationDate = ModificationDate,
                        AuthorId = AuthorId,
                        Note = Note
                    };
                    Modifications = new ObservableCollection<Modification>(output);
                    MessageBox.Show("Modification changed successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("Indexes");
                    SelectedModificationAuthorId = Indexes[0];
                    return;
                }
            }
        });

        public string ModificationDate => DateTime.Now.ToString("yyyy-MM-dd");

        private string note;
        public string Note
        {
            get => note;
            set
            {
                note = value;
                RaisePropertyChanged();
            }
        }

        private string authorId;
        public string AuthorId
        {
            get => authorId;
            set
            {
                authorId = value;
                RaisePropertyChanged();
            }
        }

        private string selectedModificationAuthorId;
        public string SelectedModificationAuthorId
        {
            get => SelectedModificationAuthorId;
            set
            {
                if (value == "None")
                {
                    authorId = modificationDate = note = "";
                }
                else
                {
                    Modification a = Modifications.First(modifacation => modifacation.AuthorId == value);
                    authorId = a.AuthorId;
                    modificationDate = a.ModificationDate;
                    note = a.Note;
                }
                SelectedModificationAuthorId = value;
                RaisePropertyChanged();
            }
        }

        private Visibility modificationsEnabled;
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
        public RelayCommand AddGameCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrEmpty(GameImage) ||
                string.IsNullOrEmpty(GameTitle) ||
                string.IsNullOrEmpty(GameProductKey) ||
                string.IsNullOrEmpty(GameAgeRating) ||
                string.IsNullOrEmpty(GameReleaseDate) ||
                string.IsNullOrEmpty(GameDescription) ||
                string.IsNullOrEmpty(GameId) ||
                string.IsNullOrEmpty(GameGenre) ||
                string.IsNullOrEmpty(GamePriceCurency) ||
                string.IsNullOrEmpty(GamePriceValue) ||
                string.IsNullOrEmpty(GameProducerIdref) ||
                string.IsNullOrEmpty(GamePublisherIdref) ||
                string.IsNullOrEmpty(GameStatisticsTimePlayed) ||
                string.IsNullOrEmpty(GameStatisticsLastSessionDate) ||
                string.IsNullOrEmpty(GameAchievementsCompleted) ||
                string.IsNullOrEmpty(GameAchievementsCount))
            {
                MessageBox.Show("Missing data to add new Game", "Modification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var output = GamesList.ToList();
                output.Add(new Game
                {
                    Image = GameImage,
                    Title = GameTitle,
                    ProductKey = GameProductKey,
                    AgeRating = GameAgeRating,
                    ReleaseDate = GameReleaseDate,
                    Description = GameDescription,
                    GameId = GameId,
                    Genre = GameGenre,
                    Price = new Price{Curency = GamePriceCurency, Text = GamePriceValue},
                    ProducerId = new ProducerId{Idref = GameProducerIdref},
                    PublisherId = new PublisherId{Idref = GamePublisherIdref},
                    Statistics = new Statistics 
                    {
                        Achievements = new Achievements { Completed = GameAchievementsCompleted, Count = GameAchievementsCount},
                        LastSessionDate = GameStatisticsLastSessionDate, TimePlayed = gameStatisticsTimePlayed
                    }
                });
                GamesList = new ObservableCollection<Game>(output);
                MessageBox.Show("Game added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        });

        private string gameImage;
        public string GameImage
        {
            get => gameImage;
            set
            {
                gameImage = value;
                RaisePropertyChanged();
            }
        }

        private string gameTitle;
        public string GameTitle
        {
            get => gameTitle;
            set
            {
                gameTitle = value;
                RaisePropertyChanged();
            }
        }

        private string gameProductKey;
        public string GameProductKey
        {
            get => gameProductKey;
            set
            {
                gameProductKey = value;
                RaisePropertyChanged();
            }
        }

        private string gamePriceCurency;
        public string GamePriceCurency
        {
            get => gamePriceCurency;
            set
            {
                gamePriceCurency = value;
                RaisePropertyChanged();
            }
        }

        private string gamePriceValue;
        public string GamePriceValue
        {
            get => gamePriceValue;
            set
            {
                gamePriceValue = value;
                RaisePropertyChanged();
            }
        }

        private string gameAgeRating;
        public string GameAgeRating
        {
            get => gameAgeRating;
            set
            {
                gameAgeRating = value;
                RaisePropertyChanged();
            }
        }

        private string gameReleaseDate;
        public string GameReleaseDate
        {
            get => gameReleaseDate;
            set
            {
                gameReleaseDate = value;
                RaisePropertyChanged();
            }
        }

        private string gameDescription;
        public string GameDescription
        {
            get => gameDescription;
            set
            {
                gameDescription = value;
                RaisePropertyChanged();
            }
        }

        private string gameId;
        public string GameId
        {
            get => gameId;
            set
            {
                gameId = value;
                RaisePropertyChanged();
            }
        }

        private string gameGenre;
        public string GameGenre
        {
            get => gameGenre;
            set
            {
                gameGenre = value;
                RaisePropertyChanged();
            }
        }

        private string gameProducerIdref;
        public string GameProducerIdref
        {
            get => gameProducerIdref;
            set
            {
                gameProducerIdref = value;
                RaisePropertyChanged();
            }
        }

        private string gamePublisherIdref;
        public string GamePublisherIdref
        {
            get => gamePublisherIdref;
            set
            {
                gamePublisherIdref = value;
                RaisePropertyChanged();
            }
        }

        private string gameStatisticsTimePlayed;
        public string GameStatisticsTimePlayed
        {
            get => gameStatisticsTimePlayed;
            set
            {
                gameStatisticsTimePlayed = value;
                RaisePropertyChanged();
            }
        }

        private string gameStatisticsLastSessionDate;
        public string GameStatisticsLastSessionDate
        {
            get => gameStatisticsLastSessionDate;
            set
            {
                gameStatisticsLastSessionDate = value;
                RaisePropertyChanged();
            }
        }

        private string gameAchievementsCompleted;
        public string GameAchievementsCompleted
        {
            get => gameAchievementsCompleted;
            set
            {
                gameAchievementsCompleted = value;
                RaisePropertyChanged();
            }
        }

        private string gameAchievementsCount;
        public string GameAchievementsCount
        {
            get => gameAchievementsCount;
            set
            {
                gameAchievementsCount = value;
                RaisePropertyChanged();
            }
        }

        private Visibility gamesEnabled;
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
            get => producerName;
            set
            {
                producerName = value;
                RaisePropertyChanged();
            }
        }

        private string producersId;
        public string ProducersId
        {
            get => producersId;
            set
            {
                producersId = value;
                RaisePropertyChanged();
            }
        }

        private string producedGames;
        public string ProducedGames
        {
            get => producedGames;
            set
            {
                producedGames = value;
                RaisePropertyChanged();
            }
        }

        private string publishers;
        public string Publishers
        {
            get => publishers;
            set
            {
                publishers = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddProducerCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrEmpty(ProducerName) ||
                string.IsNullOrEmpty(ProducersId) ||
                string.IsNullOrEmpty(ProducedGames) ||
                string.IsNullOrEmpty(ProducersId))
            {
                MessageBox.Show("Missing data to add new Producer", "Modification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var output = ProducersList.ToList();
                output.Add(new Producer
                {
                    ProducerName = ProducerName,
                    ProducerId = ProducersId,
                    ProducedGames = new ProducedGames(),
                    Publishers = new Publishers()
                });
                ProducersList = new ObservableCollection<Producer>(output);
                MessageBox.Show("Producer added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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

        private string publisherName;
        public string PublisherName
        {
            get => publisherName;
            set
            {
                publisherName = value;
                RaisePropertyChanged();
            }
        }

        private string publishersId;
        public string PublishersId
        {
            get => publishersId;
            set
            {
                publishersId = value;
                RaisePropertyChanged();
            }
        }

        private string publishedGames;
        public string PublishedGames
        {
            get => publishedGames;
            set
            {
                publishedGames = value;
                RaisePropertyChanged();
            }
        }

        private string producers;
        public string Producers
        {
            get => producers;
            set
            {
                producers = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddPublisherCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrEmpty(PublisherName) ||
                string.IsNullOrEmpty(PublishersId) ||
                string.IsNullOrEmpty(PublishedGames) ||
                string.IsNullOrEmpty(Producers))
            {
                MessageBox.Show("Missing data to add new Publisher", "Modification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var output = PublishersList.ToList();
                output.Add(new Publisher
                {
                    PublisherName = PublisherName,
                    PublisherId = PublishersId,
                    PublishedGames = new PublishedGames(),
                    Producers = new Producers()
                });
                PublishersList = new ObservableCollection<Publisher>(output);
                MessageBox.Show("Publisher added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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