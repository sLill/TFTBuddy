using Syncfusion.SfSkinManager;
using Syncfusion.Themes.Windows11Dark.WPF;
using System.Windows;

namespace TFTBuddy.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoadTheme();
            InitializeComponent();
        }

        private void LoadTheme()
        {
            Windows11DarkThemeSettings themeSettings = new Windows11DarkThemeSettings();
            themeSettings.Palette = Windows11Palette.YellowGold;

            SfSkinManager.RegisterThemeSettings("Windows11Dark", themeSettings);

            SfSkinManager.SetTheme(this, new Theme() { ThemeName = "Windows11Dark" });
            SfSkinManager.ApplyStylesOnApplication = true;
        }
    }
}
