using System;

namespace test.Models
{
    public class DebtBook
    {
        public int IdCard { get; set; }
        public int IdBook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateOut { get; set; }

    }
}
