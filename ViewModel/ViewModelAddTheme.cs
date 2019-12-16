using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Input;
using test.Models;
using test.Repository;

namespace test.ViewModel
{
    public class ViewModelAddTheme : ViewModelBase
    {
        private IDataBase dataBase;
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        public ViewModelAddTheme(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        private ICommand close;
        public ICommand Close => close ?? (close = new RelayCommand<Window>((win) => win.Close()));

        private ICommand add;
        public ICommand Add => add ?? (add = new RelayCommand<Window>(async (win) =>
        {

            if (Title != "")
            {
                Theme theme = new Theme()
                {
                    Id = await dataBase.GetThemeMaxIDAsync() + 1,
                    Title = this.Title
                };
                await dataBase.AddThemeAsync(theme);
                Messenger.Default.Send<Theme>(theme, "ThemeAdded");
                win.Close();
            }
            else MessageBox.Show("Fill in all the fields");
        }));
    }
}
