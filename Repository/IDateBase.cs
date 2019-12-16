using System.Collections.ObjectModel;
using System.Threading.Tasks;
using test.Models;

namespace test.Repository
{
    public interface IDataBase
    {
        Task<ObservableCollection<Book>> GetAllBooksAsync();
        Task<ObservableCollection<Student>> GetAllStudentsAsync();
        Task<ObservableCollection<DebtBook>> GetDebtBooksAsync(int id);
        Task<ObservableCollection<Author>> GetAllAuthorsAsync();
        Task<ObservableCollection<Theme>> GetAllThemesAsync();
        Task<ObservableCollection<Publisher>> GetAllPublishersAsync();
        Task<ObservableCollection<Category>> GetAllCategoriesAsync();
        Task<ObservableCollection<Book>> SearchBookAsync(string Search);
        Task<ObservableCollection<Student>> SearchStudentAsync(string Search);

        Task AddAuthorAsync(Author author);
        Task AddCategoryAsync(Category category);
        Task AddPublisherAsync(Publisher publisher);
        Task AddThemeAsync(Theme theme);
        Task AddBookAsync(Book book);
        Task ReturnBookAsync(DebtBook debtBook);
        Task IssueBookAsync(int bookID, int studentID);


        Task<int> GetAuthorMaxIDAsync();
        Task<int> GetCategoryMaxIDAsync();
        Task<int> GetThemeMaxIDAsync();
        Task<int> GetPublisherMaxIDAsync();
        Task<int> GetBookMaxIDAsync();
        Task<int> GetMaxS_CardIDAsync();
    }
}
