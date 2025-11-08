using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel;
using Windows.System;
using Windows.Storage;

namespace File_Converter_Utility
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            var packageVersion = Package.Current.Id.Version;

            VersionText.Text = $"{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}";

            Loaded += OnSettingsPageLoaded;
        }

        private void OnSettingsPageLoaded(object sender, RoutedEventArgs e)
        {
            var currentTheme = ThemeHelper.RootTheme;

            switch (currentTheme)
            {
                case ElementTheme.Light:
                    ThemeSelector.SelectedIndex = 1;
                    break;

                case ElementTheme.Dark:
                    ThemeSelector.SelectedIndex = 2;
                    break;

                case ElementTheme.Default:
                    ThemeSelector.SelectedIndex = 0;
                    break;
            }

            var mainWindow = App.m_window as MainWindow;
            var currentNavViewPosition = mainWindow?.NavView.PaneDisplayMode;

            switch (currentNavViewPosition)
            {
                case NavigationViewPaneDisplayMode.Left:
                    NavViewPosition.SelectedIndex = 0;
                    break;

                case NavigationViewPaneDisplayMode.Top:
                    NavViewPosition.SelectedIndex = 1;
                    break;
            }
        }

        private async void SendEmail(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("mailto:jpwaters09.business@gmail.com"));
        }

        private async void FileBugOrRequest(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/jpwaters09/File-Converter/issues/new/choose"));
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            var selectedTheme = ((ComboBoxItem)ThemeSelector.SelectedItem)?.Tag?.ToString();

            if (selectedTheme != null)
            {
                ThemeHelper.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
            }
        }

        private void ChangeNavViewPosition(object sender, RoutedEventArgs e)
        {
            var mainWindow = App.m_window as MainWindow;

            if (NavViewPosition.SelectedIndex == 0)
            {
                mainWindow.NavView.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;
                ApplicationData.Current.LocalSettings.Values["NavViewPosition"] = "Left";
            }

            if (NavViewPosition.SelectedIndex == 1)
            {
                mainWindow.NavView.PaneDisplayMode = NavigationViewPaneDisplayMode.Top;
                ApplicationData.Current.LocalSettings.Values["NavViewPosition"] = "Top";
            }
        }
    }
}
