using GalaSoft.MvvmLight;

namespace test.Models
{
    public class Book : ViewModelBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearPress { get; set; }
        public int Pages { get; set; }
        public int Id_Themes { get; set; }
        public int Id_Categry { get; set; }
        public int Id_Press { get; set; }
        public int Id_Author { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                RaisePropertyChanged();
            }

        }

        public override string ToString()
        {
            return $"{Id}\n" +
                $"{Title}\n" +
                $"{Id_Author}\n" +
                $"{YearPress.ToString()}\n" +
                $"{Pages.ToString()}\n" +
                $"{Id_Themes.ToString()}\n" +
                $"{Id_Categry.ToString()}\n" +
                $"{Id_Press.ToString()}\n" +
                $"{Quantity.ToString()}\n";
        }
    }
}
