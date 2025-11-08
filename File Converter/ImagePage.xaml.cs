using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Linq;

namespace File_Converter_Utility
{
    public sealed partial class ImagePage : Page
    {
        public ImagePage()
        {
            InitializeComponent();
        }

        private void RedirectToFromPNGConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");

            imageItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = imageItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");
        }
    }
}
