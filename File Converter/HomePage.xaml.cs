using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System.Linq;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace File_Converter_Utility
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void RedirectToImageConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;

            mainWindow.NavView.SelectedItem = mainWindow.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
        }
    }
}
