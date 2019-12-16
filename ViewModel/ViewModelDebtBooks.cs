using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using test.Models;
using test.Repository;

namespace test.ViewModel
{
    public class ViewModelDebtBooks : ViewModelBase
    {
        private IDataBase dataBase;
        private DebtBook selectedBook;
        public DebtBook SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
            }
        }

        private Student curStudent;
        public Student CurStudent
        {
            get { return curStudent; }
            set
            {
                curStudent = value;
                GettingDebtBooks();
            }
        }

        private ICommand returnBook;
        public ICommand ReturnCommand => returnBook ?? (returnBook = new RelayCommand(() =>
        {
            if (SelectedBook != null)
                ReturSelectrdnBook();
        }));

        private ObservableCollection<DebtBook> debtBooks;
        public ObservableCollection<DebtBook> DebtBooks
        {
            get { return debtBooks; }
            set
            {
                debtBooks = value;
                RaisePropertyChanged();
            }
        }
        public ViewModelDebtBooks(IDataBase dataBase)
        {
            SelectedBook = null;
            this.dataBase = dataBase;
            Messenger.Default.Register<Student>(this, "StudentSelectedChanged", (CurStudent) =>
            {
                this.CurStudent = CurStudent;
            });
            Messenger.Default.Register<string>(this, "Issued", async (obj) => { await GettingDebtBooks(); });
        }
        private async Task GettingDebtBooks()
        {
            DebtBooks = await dataBase.GetDebtBooksAsync(CurStudent.Id);
        }
        private async void ReturSelectrdnBook()
        {
            MessageBoxResult result = MessageBox.Show("Further actions can not be undone, what do you want to do?", "Return Book", MessageBoxButton.YesNo, MessageBoxImage.Question); ;
            if (result == MessageBoxResult.Yes)
            {
                await dataBase.ReturnBookAsync(SelectedBook);
                DebtBooks.Remove(SelectedBook);
                Messenger.Default.Send<string>("BooksUpdate", "BooksUpdate");
            }
        }
    }
}
