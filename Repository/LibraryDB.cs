using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using test.Models;
using System.Configuration;

namespace test.Repository
{
    public class LibraryDB : IDataBase
    {
        private string connString;
        public LibraryDB()
        {
            connString = ConfigurationManager.ConnectionStrings["LibDb"].ConnectionString;
        }

        public async Task<ObservableCollection<Book>> GetAllBooksAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Books.Id AS BID, Name, FirstName + ' ' + LastName AS Author, YearPress, Quantity FROM Books INNER JOIN Authors ON Books.Id_Author = Authors.Id";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Book> books = new ObservableCollection<Book>();
                while (reader.Read())
                {
                    Book book = new Book()
                    {
                        Id = Convert.ToInt32(reader["BID"]),
                        Title = reader["Name"].ToString(),
                        Author = reader["Author"].ToString(),
                        YearPress = Convert.ToInt32(reader["YearPress"]),
                        Quantity = Convert.ToInt32(reader["Quantity"])
                    };
                    books.Add(book);
                }
                reader.Close();
                return books;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<Student>> GetAllStudentsAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Students INNER JOIN Groups ON Students.Id_Group = Groups.Id";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Student> students = new ObservableCollection<Student>();
                while (reader.Read())
                {
                    Student stud = new Student()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Group = reader["Name"].ToString()
                    };
                    students.Add(stud);
                }
                reader.Close();
                return students;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<Author>> GetAllAuthorsAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Authors ORDER BY FirstName, LastName";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Author> authors = new ObservableCollection<Author>();
                while (reader.Read())
                {
                    Author a = new Author()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Firstname = reader["FirstName"].ToString(),
                        Lastname = reader["LastName"].ToString()
                    };
                    authors.Add(a);
                };
                reader.Close();
                return authors;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<Theme>> GetAllThemesAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Themes ORDER BY Name";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Theme> themes = new ObservableCollection<Theme>();
                while (reader.Read())
                {
                    Theme theme = new Theme()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Name"].ToString()
                    };
                    themes.Add(theme);
                }
                reader.Close();
                return themes;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<Category>> GetAllCategoriesAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Categories ORDER BY Name";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Category> categories = new ObservableCollection<Category>();
                while (reader.Read())
                {
                    Category category = new Category()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Name"].ToString()
                    };
                    categories.Add(category);
                }
                reader.Close();
                return categories;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<Publisher>> GetAllPublishersAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Press ORDER BY Name";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Publisher> publishers = new ObservableCollection<Publisher>();
                while (reader.Read())
                {
                    Publisher publisher = new Publisher()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Name"].ToString()
                    };
                    publishers.Add(publisher);
                }
                reader.Close();
                return publishers;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<DebtBook>> GetDebtBooksAsync(int id)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Id_Student, S_Cards.Id AS Card, Books.Id AS BookID, Books.Name, Authors.FirstName, Authors.LastName, DateOut FROM Books INNER JOIN S_Cards ON S_Cards.Id_Book = Books.Id " +
                                       "INNER JOIN Students ON S_Cards.Id_Student = Students.Id " +
                                       "INNER JOIN Authors ON Books.Id_Author = Authors.Id " +
                                       "WHERE DateIn IS NULL " +
                                       "GROUP BY Id_Student, S_Cards.Id, Books.Id, Books.Name, Authors.FirstName, Authors.LastName, DateOut  " +
                                       "HAVING Id_Student = @id";
                command.Parameters.AddWithValue("id", id);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                ObservableCollection<DebtBook> books = new ObservableCollection<DebtBook>();
                while (reader.Read())
                {
                    DebtBook book = new DebtBook()
                    {
                        IdCard = Convert.ToInt32(reader["Card"]),
                        IdBook = Convert.ToInt32(reader["BookID"]),
                        Title = reader["Name"].ToString(),
                        Author = reader["FirstName"].ToString() + ' ' + reader["LastName"].ToString(),
                        DateOut = DateTime.Parse(reader["DateOut"].ToString())
                    };
                    books.Add(book);
                }
                reader.Close();
                return books;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<Book>> SearchBookAsync(string Search)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Books.Id AS BID, Name, FirstName + ' ' + LastName AS Author, YearPress, Quantity FROM Books INNER JOIN Authors ON Books.Id_Author = Authors.Id WHERE Books.Name LIKE @Search";
                command.Parameters.AddWithValue("Search", "%" + Search + "%");
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Book> books = new ObservableCollection<Book>();
                while (reader.Read())
                {
                    Book book = new Book()
                    {
                        Id = Convert.ToInt32(reader["BID"]),
                        Title = reader["Name"].ToString(),
                        Author = reader["Author"].ToString(),
                        YearPress = Convert.ToInt32(reader["YearPress"]),
                        Quantity = Convert.ToInt32(reader["Quantity"])
                    };
                    books.Add(book);
                }
                reader.Close();
                return books;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task<ObservableCollection<Student>> SearchStudentAsync(string Search)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Students INNER JOIN Groups ON Students.Id_Group = Groups.Id WHERE FirstName + ' ' + LastName LIKE @Search";
                command.Parameters.AddWithValue("Search", "%" + Search + "%");
                SqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<Student> students = new ObservableCollection<Student>();
                while (reader.Read())
                {
                    Student stud = new Student()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Group = reader["Name"].ToString()

                    };
                    students.Add(stud);
                }
                reader.Close();
                return students;
            }
            catch (SqlException) { return null; }
            finally
            {
                connection.Close();
            }
        }
        public async Task IssueBookAsync(int bookID, int studentID)
        {
            SqlConnection connection = new SqlConnection(connString);
            //try
            //{
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO S_Cards(Id,Id_Book,Id_Student, DateOut, Id_Lib) " +
                                  "VALUES(@Id, @Id_Book, @Id_Student, @DateOut, @Id_Lib);";
            command.Parameters.AddWithValue("Id", await GetMaxS_CardIDAsync() + 1);
            command.Parameters.AddWithValue("Id_Book", bookID);
            command.Parameters.AddWithValue("Id_Student", studentID);
            command.Parameters.AddWithValue("DateOut", DateTime.Now);
            command.Parameters.AddWithValue("Id_Lib", 1);
            command.ExecuteNonQuery();
            command.CommandText = "UPDATE Books SET Quantity -=  1 WHERE Books.Id = @Id_Book";
            await command.ExecuteNonQueryAsync();
            //}
            //catch (SqlException) { }
            //finally
            //{
            connection.Close();
            //}
        }
        public async Task ReturnBookAsync(DebtBook debtBook)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE S_Cards SET DateIn = @Date WHERE S_Cards.Id = @Card";
                command.Parameters.AddWithValue("Date", DateTime.Now);
                command.Parameters.AddWithValue("Card", debtBook.IdCard);
                command.Parameters.AddWithValue("Book", debtBook.IdBook);
                command.ExecuteNonQuery();
                command.CommandText = "UPDATE Books SET Quantity +=  1 WHERE Books.Id = @Book";
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
        }
        public async Task AddBookAsync(Book book)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Books(Id, Name, YearPress, Id_Author,Id_Category,Id_Press,Id_Themes,Pages,Quantity)" +
                    " VALUES(@Id, @Name, @YearPress, @Id_Author, @Id_Category, @Id_Press, @Id_Themes, @Pages, @Quantity)";
                command.Parameters.AddWithValue("Id", book.Id);
                command.Parameters.AddWithValue("Name", book.Title);
                command.Parameters.AddWithValue("YearPress", book.YearPress);
                command.Parameters.AddWithValue("Id_Author", book.Id_Author);
                command.Parameters.AddWithValue("Id_Category", book.Id_Categry);
                command.Parameters.AddWithValue("Id_Press", book.Id_Press);
                command.Parameters.AddWithValue("Id_Themes", book.Id_Themes);
                command.Parameters.AddWithValue("Pages", book.Pages);
                command.Parameters.AddWithValue("Quantity", book.Quantity);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
        }
        public async Task AddAuthorAsync(Author author)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Authors(Id,FirstName,LastName) VALUES(@Id, @Firstname, @Lastname)";
                command.Parameters.AddWithValue("Id", author.Id);
                command.Parameters.AddWithValue("Firstname", author.Firstname);
                command.Parameters.AddWithValue("Lastname", author.Lastname);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
        }
        public async Task AddCategoryAsync(Category category)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Categories(Id,Name) VALUES(@Id, @Title)";
                command.Parameters.AddWithValue("Id", category.Id);
                command.Parameters.AddWithValue("Title", category.Title);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
        }
        public async Task AddThemeAsync(Theme theme)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Themes(Id,Name) VALUES(@Id, @Title)";
                command.Parameters.AddWithValue("Id", theme.Id);
                command.Parameters.AddWithValue("Title", theme.Title);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
        }
        public async Task AddPublisherAsync(Publisher publisher)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Press(Id,Name) VALUES(@Id, @Title)";
                command.Parameters.AddWithValue("Id", publisher.Id);
                command.Parameters.AddWithValue("Title", publisher.Title);
                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
        }
        public async Task<int> GetAuthorMaxIDAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            int maxid = 0;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(Id) AS ID FROM Authors";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    maxid = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
            return maxid;
        }
        public async Task<int> GetCategoryMaxIDAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            int maxid = 0;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(Id) AS ID FROM Categories";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    maxid = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
            return maxid;
        }
        public async Task<int> GetThemeMaxIDAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            int maxid = 0;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(Id) AS ID FROM Themes";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    maxid = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
            return maxid;
        }
        public async Task<int> GetPublisherMaxIDAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            int maxid = 0;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(Id) AS ID FROM Press";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    maxid = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
            return maxid;
        }
        public async Task<int> GetBookMaxIDAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            int maxid = 0;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(Id) AS ID FROM Books";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    maxid = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
            return maxid;
        }
        public async Task<int> GetMaxS_CardIDAsync()
        {
            SqlConnection connection = new SqlConnection(connString);
            int maxid = 0;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT MAX(Id) AS ID FROM S_Cards";
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    maxid = Convert.ToInt32(reader["ID"]);
                }
                reader.Close();
            }
            catch (SqlException) { }
            finally
            {
                connection.Close();
            }
            return maxid;
        }

    }
}
