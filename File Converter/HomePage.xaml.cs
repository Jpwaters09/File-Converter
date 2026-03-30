using ImageMagick;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Media;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Services.Store;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using WinRT.Interop;

namespace File_Converter_Utility
{
    public sealed partial class HomePage : Page
    {
        private static string? SourceFileFormat { get; set; } = "";
        private static string? OutputFileFormat { get; set; } = "";
        private static string? ImageFilePath { get; set; } = "";
        private static string? ImageFileName { get; set; } = "";
        private static string? ImageFileType { get; set; } = "";
        private static string? ImageOutputFilePath { get; set; } = "";

        private static string? FirstLaunchDialogTitle { get; set; } = "";
        private static string? FirstLaunchDialogContent { get; set; } = "";
        private static string? FirstLaunchDialogPrimaryButtonText { get; set; } = "";
        private static string? FirstLaunchDialogSecondaryButtonText { get; set; } = "";
        private static string? FirstLaunchDialogCloseButtonText { get; set; } = "";
        private static string? RatingDialogTitle { get; set; } = "";
        private static string? RatingDialogContent { get; set; } = "";
        private static string? RatingDialogPrimaryButtonText { get; set; } = "";
        private static string? RatingDialogSecondaryButtonText { get; set; } = "";
        private static string? RatingDialogCloseButtonText { get; set; } = "";
        private static string? FinishedDialogTitle { get; set; } = "";
        private static string? FinishedDialogCloseButtonText { get; set; } = "";
        private static string? ErrorDialogTitle { get; set; } = "";
        private static string? ErrorDialogPrimaryButtonText { get; set; } = "";
        private static string? ErrorDialogCloseButtonText { get; set; } = "";

        public HomePage()
        {
            InitializeComponent();

            var resourceLoader = new ResourceLoader();

            FirstLaunchDialogTitle = resourceLoader.GetString("FirstLaunchDialogTitle");
            FirstLaunchDialogContent = resourceLoader.GetString("FirstLaunchDialogContent");
            FirstLaunchDialogPrimaryButtonText = resourceLoader.GetString("FirstLaunchDialogPrimaryButtonText");
            FirstLaunchDialogSecondaryButtonText = resourceLoader.GetString("FirstLaunchDialogSecondaryButtonText");
            FirstLaunchDialogCloseButtonText = resourceLoader.GetString("FirstLaunchDialogCloseButtonText");
            RatingDialogTitle = resourceLoader.GetString("RatingDialogTitle");
            RatingDialogContent = resourceLoader.GetString("RatingDialogContent");
            RatingDialogPrimaryButtonText = resourceLoader.GetString("RatingDialogPrimaryButtonText");
            RatingDialogSecondaryButtonText = resourceLoader.GetString("RatingDialogSecondaryButtonText");
            RatingDialogCloseButtonText = resourceLoader.GetString("RatingDialogCloseButtonText");
            FinishedDialogTitle = resourceLoader.GetString("FinishedDialogTitle");
            FinishedDialogCloseButtonText = resourceLoader.GetString("FinishedDialogCloseButtonText");
            ErrorDialogTitle = resourceLoader.GetString("ErrorDialogTitle");
            ErrorDialogPrimaryButtonText = resourceLoader.GetString("ErrorDialogPrimaryButtonText");
            ErrorDialogCloseButtonText = resourceLoader.GetString("ErrorDialogCloseButtonText");

            Loaded += FirstLaunchPopup;
        }

        private async void FirstLaunchPopup(object sender, RoutedEventArgs e)
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            bool firstLaunch = (bool)(localSettings.Values["FirstLaunch"] ?? true);

            if (firstLaunch)
            {
                localSettings.Values["FirstLaunch"] = false;

                var FirstLaunchDialog = new ContentDialog
                {
                    Title = FirstLaunchDialogTitle,
                    Content = FirstLaunchDialogContent,
                    PrimaryButtonText = FirstLaunchDialogPrimaryButtonText,
                    SecondaryButtonText = FirstLaunchDialogSecondaryButtonText,
                    CloseButtonText = FirstLaunchDialogCloseButtonText,
                    DefaultButton = ContentDialogButton.Close,
                    XamlRoot = App.m_window?.Content.XamlRoot
                };

                var result = await FirstLaunchDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    await Launcher.LaunchUriAsync(new Uri("https://linktr.ee/jpwaters09"));
                }

                if (result == ContentDialogResult.Secondary)
                {
                    await Launcher.LaunchUriAsync(new Uri("mailto:jpwaters09.business@gmail.com"));
                }
            }
        }

        private void FileTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileTypeComboBox.SelectedItem is ComboBoxItem item)
            {
                string value = item.Content.ToString();

                switch (value)
                {
                    case "Image":
                        SourceBMPItem.Visibility = Visibility.Visible;
                        SourceGIFItem.Visibility = Visibility.Visible;
                        SourceICOItem.Visibility = Visibility.Visible;
                        SourceJPEGItem.Visibility = Visibility.Visible;
                        SourceJPGItem.Visibility = Visibility.Visible;
                        SourcePNGItem.Visibility = Visibility.Visible;
                        SourcePSDItem.Visibility = Visibility.Visible;
                        SourceSVGItem.Visibility = Visibility.Visible;
                        SourceTGAItem.Visibility = Visibility.Visible;
                        SourceTIFFItem.Visibility = Visibility.Visible;
                        SourceWebPItem.Visibility = Visibility.Visible;

                        OutputBMPItem.Visibility = Visibility.Visible;
                        OutputEPSItem.Visibility = Visibility.Visible;
                        OutputGIFItem.Visibility = Visibility.Visible;
                        OutputICOItem.Visibility = Visibility.Visible;
                        OutputJPEGItem.Visibility = Visibility.Visible;
                        OutputJPGItem.Visibility = Visibility.Visible;
                        OutputPNGItem.Visibility = Visibility.Visible;
                        OutputPSDItem.Visibility = Visibility.Visible;
                        OutputSVGItem.Visibility = Visibility.Visible;
                        OutputTGAItem.Visibility = Visibility.Visible;
                        OutputTIFFItem.Visibility = Visibility.Visible;
                        OutputWebPItem.Visibility = Visibility.Visible;
                        break;
                }

                CheckEntries();
            }
        }

        private void ConvertFromComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SourceFilePathBox.Text = "";
            OutputFolderPathBox.Text = "";

            if (ConvertFromComboBox.SelectedItem is ComboBoxItem item)
            {
                string value = item.Content.ToString();

                SourceFileFormat = value;

                CheckEntries();
            }
        }

        private void ConvertToComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConvertToComboBox.SelectedItem is ComboBoxItem item)
            {
                string value = item.Content.ToString();

                OutputFileFormat = value;

                switch (value)
                {
                    case "BMP":
                    case "EPS":
                    case "GIF":
                    case "PNG":
                    case "PSD":
                    case "SVG":
                    case "TGA":
                    case "TIFF":
                        OutputQualityTitle.Visibility = Visibility.Collapsed;
                        OutputQualityGrid.Visibility = Visibility.Collapsed;
                        IconSizeTitle.Visibility = Visibility.Collapsed;
                        IconSizeComboBox.Visibility = Visibility.Collapsed;
                        break;

                    case "ICO":
                        OutputQualityTitle.Visibility = Visibility.Collapsed;
                        OutputQualityGrid.Visibility = Visibility.Collapsed;
                        IconSizeTitle.Visibility = Visibility.Visible;
                        IconSizeComboBox.Visibility = Visibility.Visible;
                        break;

                    case "JPEG":
                    case "JPG":
                    case "WebP":
                        OutputQualityTitle.Visibility = Visibility.Visible;
                        OutputQualityGrid.Visibility = Visibility.Visible;
                        IconSizeTitle.Visibility = Visibility.Collapsed;
                        IconSizeComboBox.Visibility = Visibility.Collapsed;
                        break;
                }

                CheckEntries();
            }
        }

        private async void SourceFileSelectorButton_Click(object sender, RoutedEventArgs e)
        {
            var OpenPicker = new FileOpenPicker();
            var hwnd = WindowNative.GetWindowHandle(App.m_window);

            InitializeWithWindow.Initialize(OpenPicker, hwnd);

            OpenPicker.ViewMode = PickerViewMode.Thumbnail;
            
            switch (SourceFileFormat)
            {
                case "BMP":
                    OpenPicker.FileTypeFilter.Add(".bmp");
                    break;

                case "GIF":
                    OpenPicker.FileTypeFilter.Add(".gif");
                    break;

                case "PNG":
                    OpenPicker.FileTypeFilter.Add(".png");
                    break;

                case "PSD":
                    OpenPicker.FileTypeFilter.Add(".psd");
                    break;

                case "SVG":
                    OpenPicker.FileTypeFilter.Add(".svg");
                    break;

                case "TGA":
                    OpenPicker.FileTypeFilter.Add(".tga");
                    break;

                case "TIFF":
                    OpenPicker.FileTypeFilter.Add(".tiff");
                    break;

                case "ICO":
                    OpenPicker.FileTypeFilter.Add(".ico");
                    break;

                case "JPEG":
                    OpenPicker.FileTypeFilter.Add(".jpeg");
                    break;

                case "JPG":
                    OpenPicker.FileTypeFilter.Add(".jpg");
                    break;

                case "WebP":
                    OpenPicker.FileTypeFilter.Add(".webp");
                    break;
            }

            var file = await OpenPicker.PickSingleFileAsync();

            if (file != null)
            {
                ImageFilePath = file.Path;
                ImageFileName = file.DisplayName;
                ImageFileType = file.FileType;

                SourceFilePathBox.Text = file.Path;
            }

            else
            {
                ImageFilePath = "";


                SourceFilePathBox.Text = "";
            }

            CheckEntries();
        }

        private async void OutputFolderSelectorButton_Click(object sender, RoutedEventArgs e)
        {
            var OpenPicker = new FolderPicker();
            var hwnd = WindowNative.GetWindowHandle(App.m_window);

            InitializeWithWindow.Initialize(OpenPicker, hwnd);

            OpenPicker.ViewMode = PickerViewMode.Thumbnail;

            var folder = await OpenPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                ImageOutputFilePath = folder.Path;

                OutputFolderPathBox.Text = folder.DisplayName;
            }

            else
            {
                ImageOutputFilePath = "";

                OutputFolderPathBox.Text = "";
            }

            CheckEntries();
        }

        private void CheckEntries()
        {
            if (SourceFilePathBox.Text != "" && OutputFolderPathBox.Text != "" && FileTypeComboBox.SelectedValue != null && ConvertFromComboBox.SelectedValue != null && ConvertToComboBox.SelectedValue != null)
            {
                ConvertButton.IsEnabled = true;
            }

            else
            {
                ConvertButton.IsEnabled = false;
            }

            if (ConvertFromComboBox.SelectedValue != null)
            {
                SourceFilePathBox.IsEnabled = true;
                SourceFileSelectorButton.IsEnabled = true;
                OutputFolderPathBox.IsEnabled = true;
                OutputFolderSelectorButton.IsEnabled = true;
            }

            else
            {
                SourceFilePathBox.IsEnabled = false;
                SourceFileSelectorButton.IsEnabled = false;
                OutputFolderPathBox.IsEnabled = false;
                OutputFolderSelectorButton.IsEnabled = false;
            }
        }

        private void OutputQualitySlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (OutputQualityText == null) return;
            OutputQualityText.Text = $"{OutputQualitySlider.Value}%";
        }

        private async void ShowRatingDialog()
        {
            var RatingDialog = new ContentDialog
            {
                Title = RatingDialogTitle,
                Content = RatingDialogContent,
                PrimaryButtonText = RatingDialogPrimaryButtonText,
                SecondaryButtonText = RatingDialogSecondaryButtonText,
                CloseButtonText = RatingDialogCloseButtonText,
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = App.m_window?.Content.XamlRoot
            };

            var result = await RatingDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var hwnd = WindowNative.GetWindowHandle(App.m_window);

                StoreContext storeContext = StoreContext.GetDefault();
                InitializeWithWindow.Initialize(storeContext, hwnd);

                await storeContext.RequestRateAndReviewAppAsync();
            }

            if (result == ContentDialogResult.Secondary)
            {
                await Launcher.LaunchUriAsync(new Uri("https://github.com/jpwaters09/File-Converter/issues/new/choose"));
            }
        }

        private async void ShowFinishedDialog()
        {
            string fileExtension = "";

            switch (OutputFileFormat)
            {
                case "BMP":
                    fileExtension = "bmp";
                    break;

                case "EPS":
                    fileExtension = "eps";
                    break;

                case "GIF":
                    fileExtension = "gif";
                    break;

                case "ICO":
                    fileExtension = "ico";
                    break;

                case "JPEG":
                    fileExtension = "jpeg";
                    break;

                case "JPG":
                    fileExtension = "jpg";
                    break;

                case "PNG":
                    fileExtension = "png";
                    break;

                case "PSD":
                    fileExtension = "psd";
                    break;

                case "SVG":
                    fileExtension = "svg";
                    break;

                case "TGA":
                    fileExtension = "tga";
                    break;

                case "TIFF":
                    fileExtension = "tiff";
                    break;

                case "WebP":
                    fileExtension = "webp";
                    break;
            }

            SystemSounds.Asterisk.Play();

            ContentDialog FinishedDialog = new ContentDialog()
            {
                Title = string.Format(FinishedDialogTitle, ImageFileName, ImageFileType, fileExtension),
                CloseButtonText = FinishedDialogCloseButtonText,
                XamlRoot = App.m_window?.Content.XamlRoot
            };

            await FinishedDialog?.ShowAsync();

            var localSettings = ApplicationData.Current.LocalSettings;

            bool hasRated = (bool)(localSettings.Values["HasRated"] ?? false);
            int count = (int)(localSettings.Values["ConversionCount"] ?? 0);

            if (count == 4 && !hasRated)
            {
                localSettings.Values["HasRated"] = true;

                ShowRatingDialog();
            }
        }

        private async void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            ConvertButton.IsEnabled = false;
            ConversionProgressRing.IsActive = true;
            SourceFilePathBox.IsEnabled = false;
            SourceFileSelectorButton.IsEnabled = false;
            OutputFolderPathBox.IsEnabled = false;
            OutputFolderSelectorButton.IsEnabled = false;
            FileTypeComboBox.IsEnabled = false;
            ConvertFromComboBox.IsEnabled = false;
            ConvertToComboBox.IsEnabled = false;
            StripMetadataCheckBox.IsEnabled = false;

            switch (OutputFileFormat)
            {
                case "ICO":
                    IconSizeComboBox.IsEnabled = false;
                    break;

                case "JPEG":
                case "JPG":
                case "WebP":
                    OutputQualitySlider.IsEnabled = false;
                    break;
            }

            await Task.Delay(100);

            try
            {
                if (OutputFileFormat == "ICO" && IconSizeComboBox.SelectedIndex == 0)
                {
                    using (var collection = new MagickImageCollection())
                    {
                        int[] IconSizes = { 16, 24, 32, 48, 64, 96, 128, 192, 256 };

                        foreach (var size in IconSizes)
                        {
                            var img = new MagickImage(ImageFilePath);

                            img.Resize((uint)size, (uint)size);

                            if (StripMetadataCheckBox.IsChecked == true)
                            {
                                img.Strip();
                            }

                            collection.Add(img);
                        }

                        collection.Write($"{ImageOutputFilePath}/{ImageFileName}.ico", MagickFormat.Ico);
                    }

                    var localSettings = ApplicationData.Current.LocalSettings;
                    bool hasRated = (bool)(localSettings.Values["HasRated"] ?? false);

                    if (!hasRated)
                    {
                        int count = (int)(localSettings.Values["ConversionCount"] ?? 0) + 1;
                        localSettings.Values["ConversionCount"] = count;
                    }

                    ShowFinishedDialog();
                }

                else
                {
                    using (var image = new MagickImage(ImageFilePath))
                    {
                        if (StripMetadataCheckBox.IsChecked == true)
                        {
                            image.Strip();
                        }

                        switch (OutputFileFormat)
                        {
                            case "BMP":
                                image.Format = MagickFormat.Bmp;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.bmp");
                                break;

                            case "EPS":
                                image.Format = MagickFormat.Eps;
                                image.BackgroundColor = MagickColors.White;
                                image.Alpha(AlphaOption.Remove);

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.eps");
                                break;

                            case "GIF":
                                image.Format = MagickFormat.Gif;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.gif");
                                break;

                            case "ICO":
                                uint iconSize = uint.Parse((IconSizeComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString() ?? "32");
                                
                                image.Format = MagickFormat.Ico;
                                image.Resize(iconSize, iconSize);

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.ico");
                                break;

                            case "JPEG":
                                image.Format = MagickFormat.Jpeg;
                                image.Quality = (uint)OutputQualitySlider.Value;
                                image.BackgroundColor = MagickColors.White;
                                image.Alpha(AlphaOption.Remove);

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.jpeg");
                                break;

                            case "JPG":
                                image.Format = MagickFormat.Jpg;
                                image.Quality = (uint)OutputQualitySlider.Value;
                                image.BackgroundColor = MagickColors.White;
                                image.Alpha(AlphaOption.Remove);

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.jpg");
                                break;

                            case "PNG":
                                image.Format = MagickFormat.Png;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.png");
                                break;

                            case "PSD":
                                image.Format = MagickFormat.Psd;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.psd");
                                break;

                            case "SVG":
                                image.Format = MagickFormat.Svg;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.svg");
                                break;

                            case "TGA":
                                image.Format = MagickFormat.Tga;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.tga");
                                break;

                            case "TIFF":
                                image.Format = MagickFormat.Tiff;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.tiff");
                                break;

                            case "WebP":
                                image.Format = MagickFormat.WebP;
                                image.Quality = (uint)OutputQualitySlider.Value;

                                image.Write($"{ImageOutputFilePath}/{ImageFileName}.webp");
                                break;
                        }
                    }

                    var localSettings = ApplicationData.Current.LocalSettings;
                    bool hasRated = (bool)(localSettings.Values["HasRated"] ?? false);
                    
                    if (!hasRated)
                    {
                        int count = (int)(localSettings.Values["ConversionCount"] ?? 0) + 1;
                        localSettings.Values["ConversionCount"] = count;
                    }

                    ShowFinishedDialog();
                }
            }

            catch (Exception ex)
            {
                SystemSounds.Asterisk.Play();

                var ErrorDialog = new ContentDialog
                {
                    Title = ErrorDialogTitle,
                    Content = ex.Message,
                    PrimaryButtonText = ErrorDialogPrimaryButtonText,
                    CloseButtonText = ErrorDialogCloseButtonText,
                    XamlRoot = App.m_window?.Content.XamlRoot
                };

                var result = await ErrorDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var dataPackage = new DataPackage();

                    dataPackage.SetText(ex.Message);
                    Clipboard.SetContent(dataPackage);
                }
            }

            finally
            {
                ConvertButton.IsEnabled = true;
                ConversionProgressRing.IsActive = false;
                SourceFilePathBox.IsEnabled = true;
                SourceFileSelectorButton.IsEnabled = true;
                OutputFolderPathBox.IsEnabled = true;
                OutputFolderSelectorButton.IsEnabled = true;
                FileTypeComboBox.IsEnabled = true;
                ConvertFromComboBox.IsEnabled = true;
                ConvertToComboBox.IsEnabled = true;
                StripMetadataCheckBox.IsEnabled = true;

                switch (OutputFileFormat)
                {
                    case "ICO":
                        IconSizeComboBox.IsEnabled = true;
                        break;

                    case "JPEG":
                    case "JPG":
                    case "PNG":
                    case "WebP":
                        OutputQualitySlider.IsEnabled = true;
                        break;
                }
            }
        }
    }
}