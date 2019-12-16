using System.Windows;
using test.ViewModel;

namespace test.View
{

    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            Closing += TestWindow_Closing; 
        }

        private void TestWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ViewModelLocator.Cleanup();
        }
    }
}
