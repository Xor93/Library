using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using test.Models;
using test.Repository;

namespace test.ViewModel
{
    public class ViewModelAddBook : ViewModelBase
    {
        private IDataBase dataBase;
        private IWindowService windowService;
        private ObservableCollection<Author> authors;
        private ObservableCollection<Theme> themes;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Publisher> publishers;
        private Author selectedAuthor;
        private Theme selectedTheme;
        private Category selectedCategory;
        private Publisher selectedPublisher;
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private int yearPress;

        public int YearPress
        {
            get { return yearPress; }
            set { yearPress = value; }
        }


        private int pages;

        public int Pages
        {
            get { return pages; }
            set { pages = value; }
        }

        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }


        public ObservableCollection<Author> Authors
        {
            get { return authors; }
            set
            {
                authors = value;
                RaisePropertyChanged();
            }
        }

        public Author SelectedAuthor
        {
            get { return selectedAuthor; }
            set
            {
                selectedAuthor = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                RaisePropertyChanged();
            }
        }

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Theme> Themes
        {
            get { return themes; }
            set
            {
                themes = value;
                RaisePropertyChanged();
            }
        }

        public Theme SelectedTheme
        {
            get { return selectedTheme; }
            set
            {
                selectedTheme = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Publisher> Publishers
        {
            get { return publishers; }
            set
            {
                publishers = value;
                RaisePropertyChanged();
            }
        }

        public Publisher SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                selectedPublisher = value;
                RaisePropertyChanged();
            }
        }

        public ViewModelAddBook(IWindowService windowService, IDataBase dataBase)
        {
            this.windowService = windowService;
            this.dataBase = dataBase;
            RegistrationForMessages();
            SetProperties();
        }

        private async void SetProperties()
        {
            Authors = await dataBase.GetAllAuthorsAsync();
            Themes = await dataBase.GetAllThemesAsync();
            Categories = await dataBase.GetAllCategoriesAsync();
            Publishers = await dataBase.GetAllPublishersAsync();
            if (Authors.Count > 0)
                SelectedAuthor = Authors[0];

            if (Publishers.Count > 0)
                SelectedPublisher = Publishers[0];

            if (Themes.Count > 0)
                SelectedTheme = themes[0];

            if (Categories.Count > 0)
                SelectedCategory = Categories[0];
        }

        private void RegistrationForMessages()
        {
            Messenger.Default.Register<Author>(this, "AuthorAdded", obj =>
            {
                Authors.Add(obj);
                Authors = new ObservableCollection<Author>(Authors.OrderBy(n => n.Firstname).ThenBy(n => n.Lastname));
                SelectedAuthor = obj;
            });
            Messenger.Default.Register<Category>(this, "CategoryAdded", obj =>
            {
                Categories.Add(obj);
                Categories = new ObservableCollection<Category>(Categories.OrderBy(n => n.Title));
                SelectedCategory = obj;
            });
            Messenger.Default.Register<Publisher>(this, "PublisherAdded", obj =>
            {
                Publishers.Add(obj);
                Publishers = new ObservableCollection<Publisher>(Publishers.OrderBy(n => n.Title));
                SelectedPublisher = obj;
            });
            Messenger.Default.Register<Theme>(this, "ThemeAdded", obj =>
            {
                Themes.Add(obj);
                Themes = new ObservableCollection<Theme>(Themes.OrderBy(n => n.Title));
                SelectedTheme = obj;
            });
        }

        private ICommand addAuthor;
        public ICommand AddAuthor => addAuthor ?? (addAuthor = new RelayCommand(() =>
        {
            windowService.AddAuthor();
        }
        ));

        private ICommand addCategory;
        public ICommand AddCategory => addCategory ?? (addCategory = new RelayCommand(() =>
        {
            windowService.AddCategory();
        }
        ));

        private ICommand addPublisher;
        public ICommand AddPublisher => addPublisher ?? (addPublisher = new RelayCommand(() =>
        {
            windowService.AddPublisher();
        }
        ));

        private ICommand addTheme;
        public ICommand AddTheme => addTheme ?? (addTheme = new RelayCommand(() =>
        {
            windowService.AddTheme();
        }
        ));

        private ICommand close;
        public ICommand Close => close ?? (close = new RelayCommand<Window>((win) => win.Close()));

        private ICommand add;
        public ICommand Add => add ?? (add = new RelayCommand<Window>(async (win) =>
        {

            if (Title != "" && Pages >= 0 && Quantity >= 0)
            {
                Book book = new Book()
                {
                    Id = await dataBase.GetBookMaxIDAsync() + 1,
                    Title = this.Title,
                    YearPress = this.YearPress,
                    Id_Themes = SelectedTheme.Id,
                    Id_Categry = SelectedCategory.Id,
                    Id_Press = SelectedPublisher.Id,
                    Id_Author = SelectedAuthor.Id,
                    Pages = Convert.ToInt32(this.Pages),
                    Author = SelectedAuthor.FullName,
                    Quantity = Convert.ToInt32(this.Quantity),
                };
                await dataBase.AddBookAsync(book);
                MessageBox.Show("Books added");
                Messenger.Default.Send<Book>(book,"BookAdded");
                win.Close();
            }
            else MessageBox.Show("Fill in all the fields");
        }));

    }
}
