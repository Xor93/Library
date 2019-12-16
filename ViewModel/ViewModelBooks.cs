using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using test.Models;
using test.Repository;

namespace test.ViewModel
{

    public class ViewModelBooks : ViewModelBase
    {

        private IDataBase dataBase;
        private IWindowService windowService;
        private ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            get { return books; }
            set
            {
                books = value;
                RaisePropertyChanged();
            }
        }


        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set { selectedBook = value; }
        }

        private Student selectedStudent;
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; }
        }


        private string search = "";
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                GettingAllBooks();
            }
        }

        public ViewModelBooks(IWindowService windowService, IDataBase dataBase)
        {
            SelectedStudent = null;
            this.windowService = windowService;
            this.dataBase = dataBase;
            Books = new ObservableCollection<Book>();
            Messenger.Default.Register<Book>(this, "BookAdded", (obj) => Books.Add(obj));
            Messenger.Default.Register<Student>(this, "StudentSelectedChanged", obj => SelectedStudent = obj);
            Messenger.Default.Register<string>(this, "BooksUpdate", async (obj) => {await GettingAllBooks(); });
        }

        private async Task GettingAllBooks()
        {
            if (search.Length == 0)
            {
                Books = await dataBase.GetAllBooksAsync();
            }
            else Books = await dataBase.SearchBookAsync(Search);
        }

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ?? (addCommand = new RelayCommand(() => windowService.AddBook()));

        private ICommand issueBook;
        public ICommand IssueBook => issueBook ?? (issueBook = new RelayCommand(async () =>
        {
            if (SelectedStudent != null && SelectedBook != null)
            {
                if (SelectedBook.Quantity == 0)
                {
                    MessageBox.Show("No copies of the book left");
                }
                else
                {
                    await dataBase.IssueBookAsync(SelectedBook.Id, SelectedStudent.Id);
                    GettingAllBooks();
                    Messenger.Default.Send<string>("Issued", "Issued");
                }
            }
            else MessageBox.Show("Choose a student and a book");
        }, SelectedStudent != null || SelectedBook != null));
    }
}