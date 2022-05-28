using Haley.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFUIKitProfessional.Authorization;
using WPFUIKitProfessional.Themes;

namespace WPFUIKitProfessional.Pages
{
    public partial class Users : Page
    {
        public Users()
        {
            InitializeComponent();

            // Culture
            LangUtils.Register();
            ChangeCulture("en");
            Languages.Items.Add("English");
            Languages.Items.Add("Ukrainian");
            // ----------------------
        }

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            else
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = (App.Current.MainWindow as MainWindow);
            MainWindow.CurrentUser = null;
            MainWindow.Visibility = Visibility.Hidden;
            Account account = new Account();
            account.Show();
            account.authorizationFrameContent.Navigate(new Login());
        }

        public static void ChangeCulture(string code)
        {
            LangUtils.ChangeCulture(code);
        }

        private void Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Languages.SelectedValue.ToString().Equals("English"))
                ChangeCulture("en");
            else
                ChangeCulture("uk");
        }
    }
}