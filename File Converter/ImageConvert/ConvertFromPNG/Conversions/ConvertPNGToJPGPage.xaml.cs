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
    public sealed partial class ConvertPNGToJPGPage : Page
    {
        private static string? ImageFilePath { get; set; } = "";
        private static string? ImageFileName { get; set; } = "";
        private static string? ImageOutputFilePath { get; set; } = "";
        private static string? PNGToJPGFinishedDialogTitle { get; set; } = "";
        private static string? FinishedDialogCloseButtonText { get; set; } = "";
        private static string? FinishedDialogErrorTitle { get; set; } = "";

        public ConvertPNGToJPGPage()
        {
            InitializeComponent();

            var resourceLoader = new ResourceLoader();

            PNGToJPGFinishedDialogTitle = resourceLoader.GetString("PNGToJPGFinishedDialogTitle");
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
            PNGToJPGFinishedDialogTitle = string.Format(PNGToJPGFinishedDialogTitle, ImageFileName);

            SystemSounds.Asterisk.Play();

            ContentDialog FinishedDialog = new ContentDialog()
            {
                Title = PNGToJPGFinishedDialogTitle,
                CloseButtonText = FinishedDialogCloseButtonText,
                XamlRoot = App.m_window?.Content.XamlRoot
            };

            FinishedDialog?.ShowAsync();
        }

        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            ConvertButton.IsEnabled = false;
            progressRing.IsActive = true;
            QualitySlider.IsEnabled = false;
            ImagePath.IsEnabled = false;
            SelectImageButton.IsEnabled = false;
            OutputFolderPath.IsEnabled = false;
            SelectOutputFolderButton.IsEnabled = false;

            await Task.Delay(100);

            try
            {
                using (var image = new MagickImage(ImageFilePath))
                {
                    image.Format = MagickFormat.Jpg;
                    image.Quality = (uint)QualitySlider.Value;
                    image.BackgroundColor = MagickColors.White;
                    image.Alpha(AlphaOption.Remove);

                    if (StripMetadataCheckBox.IsChecked == true)
                    {
                        image.Strip();
                    }

                    image.Write($"{ImageOutputFilePath}/{ImageFileName}.jpg");
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
                QualitySlider.IsEnabled = true;
                ImagePath.IsEnabled = true;
                SelectImageButton.IsEnabled = true;
                OutputFolderPath.IsEnabled = true;
                SelectOutputFolderButton.IsEnabled = true;
            }
        }

        private void QualitySlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (QualityText == null) return;
            QualityText.Text = $"{QualitySlider.Value}%";
        }
    }
}
