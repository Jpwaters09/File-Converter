using Microsoft.UI.Xaml;
using WinRT.Interop;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Storage;
using Microsoft.UI.Xaml.Controls;
using System;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Composition.SystemBackdrops;

namespace File_Converter_Utility
{
    public sealed partial class MainWindow : Window
    {
        public NavigationView NavView => NavBar;
        public Frame AppFrame => ContentFrame;

        public MainWindow()
        {
            InitializeComponent();

            ThemeHelper.ActiveWindows.Add(this);

            string? savedTheme = ApplicationData.Current.LocalSettings.Values["AppTheme"]?.ToString();
            string? savedNavViewPosition = ApplicationData.Current.LocalSettings.Values["NavViewPosition"]?.ToString();

            if (savedTheme != null)
            {
                ThemeHelper.RootTheme = File_Converter_Utility.App.GetEnum<ElementTheme>(savedTheme);
            }

            if (savedNavViewPosition == null)
            {
                NavBar.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;
            }

            else if (savedNavViewPosition == "Left")
            {
                NavBar.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;
            }

            else if (savedNavViewPosition == "Top")
            {
                NavBar.PaneDisplayMode = NavigationViewPaneDisplayMode.Top;
            }

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(TitleBar);
            this.AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

            UpdateTitleBarColors();

            if (this.Content is FrameworkElement rootElement)
            {
                rootElement.ActualThemeChanged += RootElement_ActualThemeChanged;
            }

            ContentFrame.Navigate(typeof(HomePage));
            NavBar.SelectedItem = NavBar.MenuItems[0];
        }

        private void RootElement_ActualThemeChanged(FrameworkElement sender, object args)
        {
            UpdateTitleBarColors();
        }

        private ElementTheme GetCurrentTheme()
        {
            if (this.Content is FrameworkElement rootElement)
            {
                return rootElement.ActualTheme;
            }
            return ElementTheme.Default;
        }

        private SystemBackdropTheme GetSystemBackdropTheme()
        {
            switch (GetCurrentTheme())
            {
                case ElementTheme.Dark:
                    return SystemBackdropTheme.Dark;
                case ElementTheme.Light:
                    return SystemBackdropTheme.Light;
                default:
                    return SystemBackdropTheme.Default;
            }
        }

        public void UpdateTitleBarColors()
        {
            var theme = GetCurrentTheme();
            var hwnd = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            var titleBar = appWindow.TitleBar;

            if (theme == ElementTheme.Dark)
            {
                titleBar.ButtonForegroundColor = Colors.White;
            }
            else if (theme == ElementTheme.Light)
            {
                titleBar.ButtonForegroundColor = Colors.Black;
            }
            else
            {
                titleBar.ButtonForegroundColor = null;
            }
        }

        private void NavBarSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            FrameNavigationOptions navOptions = new FrameNavigationOptions();
            navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;

            var selectedItem = (NavigationViewItem)args.SelectedItem;
            var tag = selectedItem.Tag?.ToString();

            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                navOptions.IsNavigationStackEnabled = false;
            }

            Type? pageType = null;

            if (args.IsSettingsSelected)
            {
                pageType = typeof(SettingsPage);
            }

            switch (tag)
            {
                case "HomePage":
                    pageType = typeof(HomePage);
                    break;

                case "ImagePage":
                    pageType = typeof(ImagePage);
                    break;

                case "ConvertPNG":
                    pageType = typeof(ConvertFromPNGPage);
                    break;

                case "ConvertPNGToJPEG":
                    pageType = typeof(ConvertPNGToJPEGPage);
                    break;

                case "ConvertPNGToBMP":
                    pageType = typeof(ConvertPNGToBMPPage);
                    break;

                case "ConvertPNGToEPS":
                    pageType = typeof(ConvertPNGToEPSPage);
                    break;

                case "ConvertPNGToGIF":
                    pageType = typeof(ConvertPNGToGIFPage);
                    break;

                case "ConvertPNGToICO":
                    pageType = typeof(ConvertPNGToICOPage);
                    break;
            }

            ContentFrame.NavigateToType(pageType, null, navOptions);
        }
    }
}
