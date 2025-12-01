using ImageMagick;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Media;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using WinRT.Interop;
using Microsoft.Windows.ApplicationModel.Resources;

namespace File_Converter_Utility
{
    public sealed partial class ConvertPNGToEPSPage : Page
    {
        private static string? ImageFilePath { get; set; } = "";
        private static string? ImageFileName { get; set; } = "";
        private static string? ImageOutputFilePath { get; set; } = "";
        private static string? PNGToESPFinishedDialogTitle { get; set; } = "";
        private static string? FinishedDialogCloseButtonText { get; set; } = "";
        private static string? FinishedDialogErrorTitle { get; set; } = "";

        public ConvertPNGToEPSPage()
        {
            InitializeComponent();

            var resourceLoader = new ResourceLoader();

            PNGToESPFinishedDialogTitle = resourceLoader.GetString("PNGToESPFinishedDialogTitle");
            FinishedDialogCloseButtonText = resourceLoader.GetString("FinishedDialogCloseButtonText");
            FinishedDialogErrorTitle = resourceLoader.GetString("FinishedDialogErrorTitle");
        }

        private async void SelectImage(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();

            var hWnd = WindowNative.GetWindowHandle(App.m_window);

            InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add(".png");

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                ImageFilePath = file.Path;
                ImageFileName = file.DisplayName;

                ImagePath.Text = file.Path;
            }

            else
            {
                ImageFilePath = "";

                ImagePath.Text = "";
            }

            CheckEntries();
        }

        private async void SelectOutputImage(object sender, RoutedEventArgs e)
        {
            var openPicker = new FolderPicker();

            var hWnd = WindowNative.GetWindowHandle(App.m_window);

            InitializeWithWindow.Initialize(openPicker, hWnd);

            openPicker.ViewMode = PickerViewMode.Thumbnail;

            var folder = await openPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                ImageOutputFilePath = folder.Path;

                OutputFolderPath.Text = folder.Path;
            }

            else
            {
                ImageOutputFilePath = "";

                OutputFolderPath.Text = "";
            }

            CheckEntries();
        }

        private void CheckEntries()
        {
            if (ImageFilePath != "" && ImageOutputFilePath != "")
            {
                ConvertButton.IsEnabled = true;
            }

            else
            {
                ConvertButton.IsEnabled = false;
            }
        }

        private void ShowFinishedDialog()
        {
            PNGToESPFinishedDialogTitle = string.Format(PNGToESPFinishedDialogTitle, ImageFileName);

            SystemSounds.Asterisk.Play();

            ContentDialog FinishedDialog = new ContentDialog()
            {
                Title = PNGToESPFinishedDialogTitle,
                CloseButtonText = FinishedDialogCloseButtonText,
                XamlRoot = App.m_window?.Content.XamlRoot
            };

            FinishedDialog?.ShowAsync();
        }

        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            ConvertButton.IsEnabled = false;
            progressRing.IsActive = true;
            ImagePath.IsEnabled = false;
            SelectImageButton.IsEnabled = false;
            OutputFolderPath.IsEnabled = false;
            SelectOutputFolderButton.IsEnabled = false;

            await Task.Delay(100);

            try
            {
                using (var image = new MagickImage(ImageFilePath))
                {
                    image.Format = MagickFormat.Eps;
                    image.BackgroundColor = MagickColors.White;
                    image.Alpha(AlphaOption.Remove);

                    if (StripMetadataCheckBox.IsChecked == true)
                    {
                        image.Strip();
                    }

                    image.Write($"{ImageOutputFilePath}/{ImageFileName}.eps");
                }

                ShowFinishedDialog();
            }

            catch (Exception ex)
            {
                SystemSounds.Asterisk.Play();

                ContentDialog FinishedDialog = new ContentDialog()
                {
                    Title = FinishedDialogErrorTitle,
                    Content = ex.Message,
                    CloseButtonText = FinishedDialogCloseButtonText,
                    XamlRoot = App.m_window.Content.XamlRoot
                };

                FinishedDialog?.ShowAsync();
            }

            finally
            {
                ConvertButton.IsEnabled = true;
                progressRing.IsActive = false;
                ImagePath.IsEnabled = true;
                SelectImageButton.IsEnabled = true;
                OutputFolderPath.IsEnabled = true;
                SelectOutputFolderButton.IsEnabled = true;
            }
        }
    }
}
