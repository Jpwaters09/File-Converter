using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;

namespace File_Converter_Utility
{
    public sealed partial class ConvertFromPNGPage : Page
    {
        public ConvertFromPNGPage()
        {
            InitializeComponent();
        }

        private void RedirectToPNGToBMPConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToBMP");
        }


        private void RedirectToPNGToEPSConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToEPS");
        }

        private void RedirectToPNGToGIFConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToGIF");
        }

        private void RedirectToPNGToICOConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToICO");
        }

        private void RedirectToPNGToJPEGConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToJPEG");
        }

        private void RedirectToPNGToJPGConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToJPG");
        }

        private void RedirectToPNGToPSDConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToPSD");
        }

        private void RedirectToPNGToSVGConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToSVG");
        }

        private void RedirectToPNGToTGAConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToTGA");
        }

        private void RedirectToPNGToTIFFConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToTIFF");
        }

        private void RedirectToPNGToWebPConversion(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;
            var imageItem = mainWindow?.NavView.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ImagePage");
            var pngItem = imageItem?.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNG");

            pngItem.IsExpanded = true;
            mainWindow.NavView.SelectedItem = pngItem.MenuItems.OfType<NavigationViewItem>().First(i => (string)i.Tag == "ConvertPNGToWebP");
        }
    }
}
