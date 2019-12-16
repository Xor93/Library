namespace test.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FullName { get { return Firstname + ' ' + Lastname; } private set { } }
    }
}
