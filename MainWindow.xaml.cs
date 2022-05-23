using System.Windows;
using System.Windows.Controls;
using WPFUIKitProfessional.Themes;
using WPFUIKitProfessional.Pages;
using Haley.Utils;
using WPFUIKitProfessional.Authorization;
using WPFUIKitProfessional.Models;

namespace WPFUIKitProfessional
{
    public partial class MainWindow : Window
    {
        public User CurrentUser { get; set; }
        public MainWindow()
        {
            Account account = new Account();
            account.Show();
            account.authorizationFrameContent.Navigate(new Login());

            InitializeComponent();

            // Culture
            LangUtils.Register();
            ChangeCulture("en");
            Languages.Items.Add("en");
            Languages.Items.Add("uk");
            // ----------------------
        }

        #region Culture
        public static void ChangeCulture(string code)
        {
            LangUtils.ChangeCulture(code);
        }

        private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCulture(Languages.SelectedValue.ToString());
        }
        #endregion

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            else
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdLevels_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Levels());
        }

        private void rdGuide_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Guide());
        }

        private void rdConstructor_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Constructor());
        }

        private void rdUsers_Click(object sender, RoutedEventArgs e)
        {
            Users usersPage = new Users();
            User user = (App.Current.MainWindow as MainWindow).CurrentUser;
            usersPage.id.Text = user.Id.ToString();
            usersPage.login.Text = user.Login;
            usersPage.password.Text = user.Password;
            frameContent.Navigate(usersPage);
        }
    }
}
