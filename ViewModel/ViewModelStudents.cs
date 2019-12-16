using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using test.Models;
using test.Repository;

namespace test.ViewModel
{
    public class ViewModelStudents : ViewModelBase
    {
        private IDataBase dataBase;
        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                RaisePropertyChanged();
            }
        }

        private Student cur_Student;
        public Student Cur_Student
        {
            get { return cur_Student; }
            set
            {
                cur_Student = value;
                Messenger.Default.Send<Student>(Cur_Student, "StudentSelectedChanged");
            }
        }

        private string search = "";
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                GettingAllStudents();
            }
        }
        public ViewModelStudents(IDataBase dataBase)
        {
            this.dataBase = dataBase;
            students = new ObservableCollection<Student>();
            GettingAllStudents();
        }



        private async void GettingAllStudents()
        {
            if (search.Length == 0)
            {
                Students = await dataBase.GetAllStudentsAsync();
            }
            else Students = await dataBase.SearchStudentAsync(Search);
        }
    }
}
