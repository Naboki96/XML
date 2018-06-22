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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1;

namespace XML.View.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region DataProperties

        private GameLibrary gameLibrary;

        public ObservableCollection<Author> AuthorsList
        {
            get { return new ObservableCollection<Author>(gameLibrary.Authors.Author); }
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

            AuthorsEnabled = GamesEnabled =
                ModificationsEnabled = ProducersEnabled = PublishersEnabled = Visibility.Collapsed;
        }

        public RelayCommand Deserialize => new RelayCommand(() =>
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(GameLibrary));
            TextReader reader = new StreamReader(@"./File1.xml");
            GameLibrary data = deserializer.Deserialize(reader) as GameLibrary;
            reader.Close();

            gameLibrary = data;
        });


        public RelayCommand ToPdfCommand => new RelayCommand(() =>
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writ = PdfWriter.GetInstance(doc, new FileStream("PDF.pdf", FileMode.Create));
            doc.Open();
            Paragraph paragraph = new Paragraph();
            paragraph.Add($"GameLibrary:\n" +
                          $"\tAuthors:\n" +
                          $"\t\t{string.Join("\n\t\t", gameLibrary.Authors.Author)}\n" +
                          $"\tModifications:\n" +
                          $"\t\t{string.Join("\n\t\t", gameLibrary.ModificationsList.Modification)}\n" +
                          $"\tGames:\n" +
                          $"\t\t{string.Join("\n\t\t", gameLibrary.GameList.Game)}\n" +
                          $"\tPublishers:\n" +
                          $"\t\t{string.Join("\n\t\t", gameLibrary.PublisherList.Publisher)}\n" +
                          $"\tProducers:\n" +
                          $"\t\t{string.Join("\n\t\t", gameLibrary.ProducerList.Producer)}");
            doc.Add(paragraph);
            doc.Close();

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
            if (AuthorsEnabled == Visibility.Collapsed)
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
                List<Author> output = AuthorsList.ToList();
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
                    AuthorName = AuthorSurname = "";
                    AuthorIndex = "None";
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
            if (ModificationsEnabled == Visibility.Collapsed)
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

        //public RelayCommand DeleteModificationCommand => new RelayCommand(() =>
        //{
        //    if (SelectedModificationAuthorId == "None")
        //    {
        //        MessageBox.Show("Select Author of Modification you want to delete.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }
        //    for (var index = 0; index < Modifications.Count; index++)
        //    {
        //        if (Modifications[index].AuthorId == SelectedModificationAuthorId)
        //        {
        //            List<Modification> output = Modifications.ToList();
        //            output.RemoveAt(index);
        //            Modifications = new ObservableCollection<Modification>(output);
        //            MessageBox.Show("Modification deleted successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        //            RaisePropertyChanged("Indexes");
        //            SelectedModificationAuthorId = Indexes[0];
        //            return;
        //        }
        //    }
        //});
        //public RelayCommand ModifyModificationCommand => new RelayCommand(() =>
        //{
        //    if (SelectedModificationAuthorId == "None")
        //    {
        //        MessageBox.Show("Select Author of Modification you want to modify.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }
        //    for (var index = 0; index < Modifications.Count; index++)
        //    {
        //        if (Modifications[index].AuthorId == SelectedModificationAuthorId)
        //        {
        //            var output = Modifications.ToList();
        //            output[index] = new Modification()
        //            {
        //                ModificationDate = ModificationDate,
        //                AuthorId = AuthorId,
        //                Note = Note
        //            };
        //            Modifications = new ObservableCollection<Modification>(output);
        //            MessageBox.Show("Modification changed successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        //            RaisePropertyChanged("Indexes");
        //            SelectedModificationAuthorId = Indexes[0];
        //            return;
        //        }
        //    }
        //});

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

        //private string selectedModificationAuthorId;
        //public string SelectedModificationAuthorId
        //{
        //    get => SelectedModificationAuthorId;
        //    set
        //    {
        //        if (value == "None")
        //        {
        //            authorId = modificationDate = note = "";
        //        }
        //        else
        //        {
        //            Modification a = Modifications.First(modifacation => modifacation.AuthorId == value);
        //            authorId = a.AuthorId;
        //            modificationDate = a.ModificationDate;
        //            note = a.Note;
        //        }
        //        SelectedModificationAuthorId = value;
        //        RaisePropertyChanged();
        //    }
        //}

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
            if (GamesEnabled == Visibility.Collapsed)
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
                string.IsNullOrEmpty(GameSelectedProducerId) ||
                string.IsNullOrEmpty(GameSelectedPublisherId) ||
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
                var g = new Game
                {
                    Image = GameImage,
                    Title = GameTitle,
                    ProductKey = GameProductKey,
                    AgeRating = GameAgeRating,
                    ReleaseDate = GameReleaseDate,
                    Description = GameDescription,
                    GameId = GameId,
                    Genre = GameGenre,
                    Price = new Price { Curency = GamePriceCurency, Text = GamePriceValue },
                    ProducerId = new ProducerId { Idref = GameSelectedProducerId },
                    PublisherId = new PublisherId { Idref = GameSelectedPublisherId },
                    Statistics = new Statistics
                    {
                        Achievements =
                            new Achievements { Completed = GameAchievementsCompleted, Count = GameAchievementsCount },
                        LastSessionDate = GameStatisticsLastSessionDate,
                        TimePlayed = gameStatisticsTimePlayed
                    }
                };
                output.Add(g);

                try
                {
                    ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).ProducedGames.GameId.Add(new GameId { Idref = g.GameId });
                    ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).Publishers.PublisherId.Add(new PublisherId { Idref = g.PublisherId.Idref });
                    PublishersList.First(publisher => publisher.PublisherId == g.ProducerId.Idref).PublishedGames.GameId.Add(new GameId { Idref = g.GameId });
                    PublishersList.First(publisher => publisher.PublisherId == g.ProducerId.Idref).Producers.ProducerId.Add(new ProducerId { Idref = g.ProducerId.Idref });
                }
                catch (Exception e)
                {
                }
                GamesList = new ObservableCollection<Game>(output);
                MessageBox.Show("Game added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                RaisePropertyChanged("GameIds");
                RaisePropertyChanged("ProducersList");
                RaisePropertyChanged("PublishersList");
                SelectedGameId = GameIds[0];
            }
        });
        public RelayCommand DeleteGameCommand => new RelayCommand(() =>
          {
              if (SelectedGameId == "None")
              {
                  MessageBox.Show("Select Index of Game you want to delete.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                  return;
              }
              for (var index = 0; index < GamesList.Count; index++)
              {
                  if (GamesList[index].GameId == SelectedGameId)
                  {
                      List<Game> output = GamesList.ToList();
                      var g = output[index];
                      output.RemoveAt(index);

                      try
                      {
                          ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).ProducedGames
                                  .GameId =
                              ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).ProducedGames
                                  .GameId.Where(id => id.Idref != g.GameId).ToList();

                          ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).Publishers
                                  .PublisherId =
                              ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).Publishers
                                  .PublisherId.Where(id => id.Idref != g.PublisherId.Idref).ToList();


                          PublishersList.First(publisher => publisher.PublisherId == g.PublisherId.Idref)
                              .PublishedGames
                              .GameId = PublishersList.First(publisher => publisher.PublisherId == g.PublisherId.Idref)
                              .PublishedGames.GameId.Where(id => id.Idref != g.GameId).ToList();

                          PublishersList.First(publisher => publisher.PublisherId == g.PublisherId.Idref)
                              .Producers
                              .ProducerId = PublishersList.First(publisher => publisher.PublisherId == g.PublisherId.Idref)
                              .Producers.ProducerId.Where(id => id.Idref != g.ProducerId.Idref).ToList();
                      }
                      catch (Exception e)
                      {

                      }

                      GamesList = new ObservableCollection<Game>(output);
                      MessageBox.Show("Author deleted successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                      RaisePropertyChanged("GameIds");
                      RaisePropertyChanged("ProducersList");
                      RaisePropertyChanged("PublishersList");
                      SelectedGameId = GameIds[0];
                      return;
                  }
              }
          });
        public RelayCommand ModifyGameCommand => new RelayCommand(() =>
        {
            if (SelectedGameId == "None")
            {
                MessageBox.Show("Select Index of game you want to modify.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < GamesList.Count; index++)
            {
                if (GamesList[index].GameId == SelectedGameId)
                {
                    var output = GamesList.ToList();
                    var oldG = output[index];

                    try
                    {
                        ProducersList.First(producer => producer.ProducerId == oldG.ProducerId.Idref).ProducedGames
                                .GameId =
                            ProducersList.First(producer => producer.ProducerId == oldG.ProducerId.Idref).ProducedGames
                                .GameId.Where(id => id.Idref != oldG.GameId).ToList();

                        ProducersList.First(producer => producer.ProducerId == oldG.ProducerId.Idref).Publishers
                                .PublisherId =
                            ProducersList.First(producer => producer.ProducerId == oldG.ProducerId.Idref).Publishers
                                .PublisherId.Where(id => id.Idref != oldG.PublisherId.Idref).ToList();


                        PublishersList.First(publisher => publisher.PublisherId == oldG.PublisherId.Idref)
                            .PublishedGames
                            .GameId = PublishersList.First(publisher => publisher.PublisherId == oldG.PublisherId.Idref)
                            .PublishedGames.GameId.Where(id => id.Idref != oldG.GameId).ToList();

                        PublishersList.First(publisher => publisher.PublisherId == oldG.PublisherId.Idref)
                            .Producers
                            .ProducerId = PublishersList.First(publisher => publisher.PublisherId == oldG.PublisherId.Idref)
                            .Producers.ProducerId.Where(id => id.Idref != oldG.ProducerId.Idref).ToList();
                    }
                    catch (Exception e)
                    {

                    }

                    var g = new Game
                    {
                        Image = GameImage,
                        Title = GameTitle,
                        ProductKey = GameProductKey,
                        AgeRating = GameAgeRating,
                        ReleaseDate = GameReleaseDate,
                        Description = GameDescription,
                        GameId = this.GameId,
                        Genre = GameGenre,
                        Price = new Price
                        {
                            Curency = GamePriceCurency,
                            Text = GamePriceValue
                        },
                        ProducerId = new ProducerId { Idref = GameSelectedProducerId },
                        PublisherId = new PublisherId { Idref = GameSelectedPublisherId },
                        Statistics = new Statistics
                        {
                            Achievements = new Achievements
                            {
                                Count = GameAchievementsCount,
                                Completed = GameAchievementsCompleted
                            },
                            LastSessionDate = GameStatisticsLastSessionDate,
                            TimePlayed = GameStatisticsTimePlayed
                        }
                    };
                    output[index] = g;
                    try
                    {
                        ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).ProducedGames.GameId.Add(new GameId { Idref = g.GameId });
                        ProducersList.First(producer => producer.ProducerId == g.ProducerId.Idref).Publishers.PublisherId.Add(new PublisherId{Idref = g.PublisherId.Idref});
                        PublishersList.First(publisher => publisher.PublisherId == g.ProducerId.Idref).PublishedGames.GameId.Add(new GameId { Idref = g.GameId });
                        PublishersList.First(publisher => publisher.PublisherId == g.ProducerId.Idref).Producers.ProducerId.Add(new ProducerId{ Idref = g.ProducerId.Idref});
                    }
                    catch (Exception e)
                    {

                    }
                    GamesList = new ObservableCollection<Game>(output);
                    MessageBox.Show("Author Modified successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("GameIds");
                    RaisePropertyChanged("ProducersList");
                    RaisePropertyChanged("PublishersList");
                    SelectedGameId = GameIds[0];
                    return;
                }
            }
        });

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

        public string[] GameIds
        {
            get
            {
                var output = new List<string> { "None" };
                output.AddRange(gameLibrary.GameList.Game.Select(game => game.GameId));
                return output.ToArray();
            }
        }

        private string selectedGameId;
        public string SelectedGameId
        {
            get { return selectedGameId; }
            set
            {
                if (value == "None")
                {
                    GameImage = GameTitle = GameProductKey = GameAgeRating = GameReleaseDate =
                        GameDescription = GameGenre = GamePriceCurency = GamePriceValue = GameStatisticsTimePlayed =
                            GameStatisticsLastSessionDate = GameAchievementsCompleted = GameAchievementsCount = "";
                    GameSelectedProducerId = GameSelectedPublisherId = GameId = "None";
                }
                else
                {
                    Game g = GamesList.First(game => game.GameId == value);
                    GameImage = g.Image;
                    GameTitle = g.Title;
                    GameProductKey = g.ProductKey;
                    GameAgeRating = g.AgeRating;
                    GameReleaseDate = g.ReleaseDate;
                    GameDescription = g.Description;
                    GameId = g.GameId;
                    GameGenre = g.Genre;
                    GamePriceCurency = g.Price.Curency;
                    GamePriceValue = g.Price.Text;
                    GameSelectedProducerId = g.ProducerId.Idref;
                    GameSelectedPublisherId = g.PublisherId.Idref;
                    GameStatisticsTimePlayed = g.Statistics.TimePlayed;
                    GameStatisticsLastSessionDate = g.Statistics.LastSessionDate;
                    GameAchievementsCompleted = g.Statistics.Achievements.Completed;
                    GameAchievementsCount = g.Statistics.Achievements.Count;

                }
                selectedGameId = value;
                RaisePropertyChanged();
            }
        }

        private string gameSelectedProducerId;
        public string GameSelectedProducerId
        {
            get { return gameSelectedProducerId; }
            set
            {
                gameSelectedProducerId = value;
                RaisePropertyChanged();
            }
        }

        private string gameSelectedPublisherId;
        public string GameSelectedPublisherId
        {
            get { return gameSelectedPublisherId; }
            set
            {
                gameSelectedPublisherId = value;
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
            if (ProducersEnabled == Visibility.Collapsed)
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
        public RelayCommand AddProducerCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrEmpty(ProducerName) ||
                string.IsNullOrEmpty(ProducerId))
            {
                MessageBox.Show("Missing data to add new Producer", "Modification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var output = ProducersList.ToList();
                output.Add(new Producer
                {
                    ProducerName = ProducerName,
                    ProducerId = ProducerId,
                    ProducedGames = new ProducedGames(),
                    Publishers = new Publishers()
                });
                ProducersList = new ObservableCollection<Producer>(output);
                MessageBox.Show("Producer added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                RaisePropertyChanged("ProducerIds");
                SelectedProducerId = ProducerIds[0];
            }
        });
        public RelayCommand DeleteProducerCommand => new RelayCommand(() =>
        {
            if (SelectedProducerId == "None")
            {
                MessageBox.Show("Select Index of producer you want to delete.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < ProducersList.Count; index++)
            {
                if (ProducersList[index].ProducerId == SelectedProducerId)
                {
                    List<Producer> output = ProducersList.ToList();
                    output.RemoveAt(index);
                    ProducersList = new ObservableCollection<Producer>(output);
                    MessageBox.Show("Producer deleted successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("ProducerIds");
                    SelectedProducerId = ProducerIds[0];
                    return;
                }
            }
        });
        public RelayCommand ModifyProducerCommand => new RelayCommand(() =>
        {
            if (SelectedProducerId == "None")
            {
                MessageBox.Show("Select Index of Author you want to modify.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < ProducersList.Count; index++)
            {
                if (ProducersList[index].ProducerId == SelectedProducerId)
                {
                    var output = ProducersList.ToList();
                    output[index] = new Producer
                    {
                        ProducerId = ProducerId,
                        ProducerName = ProducerName,
                        Publishers = new Publishers(ProducerPublishers),
                        ProducedGames = new ProducedGames(ProducedGames)
                    };
                    ProducersList = new ObservableCollection<Producer>(output);
                    MessageBox.Show("Producer Modified successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("ProducerIds");
                    SelectedProducerId = ProducerIds[0];
                    return;
                }
            }
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

        private string producerId;
        public string ProducerId
        {
            get => producerId;
            set
            {
                producerId = value;
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

        private string producerPublishers;

        public string ProducerPublishers
        {
            get { return producerPublishers; }
            set
            {
                producerPublishers = value;
                RaisePropertyChanged();
            }
        }

        public string[] ProducerIds
        {
            get
            {
                var output = new List<string> { "None" };
                output.AddRange(gameLibrary.ProducerList.Producer.Select(producer => producer.ProducerId));
                return output.ToArray();
            }
        }

        private string selectedProducerId;
        public string SelectedProducerId
        {
            get { return selectedProducerId; }
            set
            {
                if (value == "None")
                {
                    ProducerName = ProducedGames = ProducerPublishers = "";
                    ProducerId = "None";
                }
                else
                {
                    Producer p = ProducersList.First(producer => producer.ProducerId == value);
                    ProducerId = p.ProducerId;
                    ProducerName = p.ProducerName;
                    ProducerPublishers = p.Publishers.ToString();
                    ProducedGames = p.ProducedGames.ToString();
                }
                selectedProducerId = value;
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
        public RelayCommand AddPublisherCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrEmpty(PublisherName) ||
                string.IsNullOrEmpty(PublisherId))
            {
                MessageBox.Show("Missing data to add new Publisher", "Modification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var output = PublishersList.ToList();
                output.Add(new Publisher
                {
                    PublisherName = PublisherName,
                    PublisherId = PublisherId,
                    PublishedGames = new PublishedGames(),
                    Producers = new Producers()
                });
                PublishersList = new ObservableCollection<Publisher>(output);
                MessageBox.Show("Publisher added successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                RaisePropertyChanged("PublisherIds");
                SelectedPublisherId = PublisherIds[0];
            }
        });
        public RelayCommand DeletePublisherCommand => new RelayCommand(() =>
        {
            if (SelectedPublisherId == "None")
            {
                MessageBox.Show("Select Index of Publisher you want to delete.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < PublishersList.Count; index++)
            {
                if (PublishersList[index].PublisherId == SelectedPublisherId)
                {
                    List<Publisher> output = PublishersList.ToList();
                    output.RemoveAt(index);
                    PublishersList = new ObservableCollection<Publisher>(output);
                    MessageBox.Show("Publisher deleted successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("PublisherIds");
                    SelectedPublisherId = PublisherIds[0];
                    return;
                }
            }
        });
        public RelayCommand ModifyPublisherCommand => new RelayCommand(() =>
        {
            if (SelectedPublisherId == "None")
            {
                MessageBox.Show("Select Index of Author you want to modify.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (var index = 0; index < PublishersList.Count; index++)
            {
                if (PublishersList[index].PublisherId == SelectedPublisherId)
                {
                    var output = PublishersList.ToList();
                    output[index] = new Publisher
                    {
                        PublisherId = SelectedPublisherId,
                        PublisherName = PublisherName,
                        Producers = new Producers(PublisherProducers),
                        PublishedGames = new PublishedGames(PublishedGames)
                    };
                    PublishersList = new ObservableCollection<Publisher>(output);
                    MessageBox.Show("Producer Modified successfuly", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    RaisePropertyChanged("PublisherIds");
                    SelectedPublisherId = PublisherIds[0];
                    return;
                }
            }
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

        public string PublisherId
        {
            get { return publisherId; }
            set
            {
                publisherId = value; 
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

        private string publisherProducers;

        public string PublisherProducers
        {
            get => publisherProducers;
            set
            {
                publisherProducers = value;
                RaisePropertyChanged();
            }
        }

        public string[] PublisherIds
        {
            get
            {
                var output = new List<string> { "None" };
                output.AddRange(gameLibrary.PublisherList.Publisher.Select(publisher => publisher.PublisherId));
                return output.ToArray();
            }
        }

        private string selectedPublisherId;
        private string publisherId;

        public string SelectedPublisherId
        {
            get { return selectedPublisherId; }
            set
            {
                if (value == "None")
                {
                    PublisherName = PublishedGames = PublisherProducers = "";
                    PublisherId = "None";
                }
                else
                {
                    Publisher p = PublishersList.First(publisher => publisher.PublisherId == value);
                    PublisherId = p.PublisherId;
                    PublisherName = p.PublisherName;
                    PublisherProducers = p.Producers.ToString();
                    PublishedGames = p.PublishedGames.ToString();
                }
                selectedPublisherId = value; 
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
}