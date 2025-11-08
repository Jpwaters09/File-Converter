using Microsoft.UI.Xaml;
using System;

namespace File_Converter_Utility
{
    public partial class App : Application
    {
        public static Window? m_window { get; private set; }

        public App()
        {
            this.InitializeComponent();

            m_window = new MainWindow();
            m_window.Activate();
        }

        public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }
    }
}
