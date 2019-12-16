using GalaSoft.MvvmLight.Threading;
using System.Windows;
using test.Repository;

namespace test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

    }
}
