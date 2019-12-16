using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Input;
using test.Models;
using test.Repository;

namespace test.ViewModel
{
    public class ViewModelAddAuthor : ViewModelBase
    {
        private IDataBase dataBase;
        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }


        public ViewModelAddAuthor(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        private ICommand close;
        public ICommand Close => close ?? (close = new RelayCommand<Window>((win) => win.Close()));

        private ICommand add;
        public ICommand Add => add ?? (add = new RelayCommand<Window>(async (win) =>
        {

            if (Firstname != "" && Lastname != "")
            {
                Author author = new Author()
                {
                    Id = await dataBase.GetAuthorMaxIDAsync() + 1,
                    Firstname = this.Firstname,
                    Lastname = this.lastname
                };
                await dataBase.AddAuthorAsync(author);
                Messenger.Default.Send<Author>(author, "AuthorAdded");
                win.Close();
            }
            else MessageBox.Show("Fill in all the fields");
        }));
    }
}
