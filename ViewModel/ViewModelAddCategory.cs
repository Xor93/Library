using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Input;
using test.Models;
using test.Repository;

namespace test.ViewModel
{
    public class ViewModelAddCategory : ViewModelBase
    {
        private IDataBase dataBase;
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        public ViewModelAddCategory(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        private ICommand close;
        public ICommand Close => close ?? (close = new RelayCommand<Window>((win) => win.Close()));

        private ICommand add;
        public ICommand Add => add ?? (add = new RelayCommand<Window>(async (win) =>
        {

            if (Title != "" )
            {
                Category category = new Category()
                {
                    Id = await dataBase.GetCategoryMaxIDAsync() + 1,
                    Title = this.Title
                };
                await dataBase.AddCategoryAsync(category);
                Messenger.Default.Send<Category>(category, "CategoryAdded");
                win.Close();
            }
            else MessageBox.Show("Fill in all the fields");
        }));
    }
}
