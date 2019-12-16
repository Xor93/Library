using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using test.Repository;

namespace test.ViewModel
{

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IDataBase, LibraryDB>();   
            SimpleIoc.Default.Register<IWindowService, WindowService>();              
            SimpleIoc.Default.Register<ViewModelBooks>();
            SimpleIoc.Default.Register<ViewModelStudents>();        
            SimpleIoc.Default.Register<ViewModelDebtBooks>();
        }

        public ViewModelBooks Books
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModelBooks>();
            }
        }

        public ViewModelAddTheme AddTheme
        {
            get
            {
                SimpleIoc.Default.Unregister<ViewModelAddTheme>();
                SimpleIoc.Default.Register<ViewModelAddTheme>();
                return ServiceLocator.Current.GetInstance<ViewModelAddTheme>();
            }
        }

        public ViewModelAddPublisher AddPublisher
        {
            get
            {
                SimpleIoc.Default.Unregister<ViewModelAddPublisher>();
                SimpleIoc.Default.Register<ViewModelAddPublisher>();
                return ServiceLocator.Current.GetInstance<ViewModelAddPublisher>();
            }
        }

        public ViewModelAddAuthor AddAuthor
        {
            get
            {
                SimpleIoc.Default.Unregister<ViewModelAddAuthor>();
                SimpleIoc.Default.Register<ViewModelAddAuthor>();
                return ServiceLocator.Current.GetInstance<ViewModelAddAuthor>();
            }
        }
        public ViewModelAddCategory AddCategory
        {
            get
            {
                SimpleIoc.Default.Unregister<ViewModelAddCategory>();
                SimpleIoc.Default.Register<ViewModelAddCategory>();
                return ServiceLocator.Current.GetInstance<ViewModelAddCategory>();
            }
        }

        public ViewModelAddBook AddBook
        {
            get
            {
                SimpleIoc.Default.Unregister<ViewModelAddBook>();
                SimpleIoc.Default.Register<ViewModelAddBook>();
                return ServiceLocator.Current.GetInstance<ViewModelAddBook>();
            }
        }
        public ViewModelDebtBooks DebtBooks
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModelDebtBooks>();
            }
        }

        public ViewModelStudents Students
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModelStudents>();
            }
        }
        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<ViewModelBooks>();
            SimpleIoc.Default.Unregister<ViewModelDebtBooks>();
            SimpleIoc.Default.Unregister<ViewModelStudents>();
            SimpleIoc.Default.Unregister<ViewModelAddAuthor>();
            SimpleIoc.Default.Unregister<ViewModelAddCategory>();
            SimpleIoc.Default.Unregister<ViewModelAddPublisher>();
            SimpleIoc.Default.Unregister<ViewModelAddTheme>();
            SimpleIoc.Default.Unregister<ViewModelAddBook>();
        }
    }
}