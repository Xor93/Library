using test.View;

namespace test.Repository
{
    public class WindowService : IWindowService
    {
        public void AddAuthor()
        {
            AddAuthorView win = new AddAuthorView();
            win.ShowDialog();
        }

        public void AddBook()
        {
            AddBookView win = new AddBookView();
            win.ShowDialog();
        }

        public void AddCategory()
        {
            AddCategoryView win = new AddCategoryView();
            win.ShowDialog();
        }
        public void AddPublisher()
        {
            AddPublisherView win = new AddPublisherView();
            win.ShowDialog();
        }

        public void AddTheme()
        {
            AddThemeView win = new AddThemeView();
            win.ShowDialog();
        }
    }
}
