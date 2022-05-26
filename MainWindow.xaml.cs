using System.Windows;
using WPFUIKitProfessional.Themes;
using WPFUIKitProfessional.Pages;
using WPFUIKitProfessional.Authorization;
using WPFUIKitProfessional.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUIKitProfessional
{
    public partial class MainWindow : Window
    {
        public User CurrentUser { get; set; }
        public Constructor Constructor { get; set; }
        public Guide Guide { get; set; }
        public Levels Levels { get; set; }
        public Users Users { get; set; }
        public Pages.Level Level { get; set; }

        public MainWindow()
        {
            Constructor = new Constructor();
            Guide = new Guide();
            Levels = new Levels();
            Users = new Users();
            Level = new Pages.Level();

            Account account = new Account();
            account.Show();
            account.authorizationFrameContent.Navigate(new Login());

            Visibility = Visibility.Hidden;
            //Visibility = Visibility.Visible;
            InitializeComponent();
        }

        #region Culture

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

        private async void rdLevels_Click(object sender, RoutedEventArgs e)
        {
            var db = new ApplicationContext();
            db.Levels.Load();
            db.CompletedLevels.Load();
            DataContext = db.Levels.Local.ToBindingList();
            DataContext = db.CompletedLevels.Local.ToBindingList();
            List<int> levelId = await db.Levels.Select(x => x.Id).ToListAsync();
            List<CompletedLevel> completedLevel = await db.CompletedLevels.Select(x => x).ToListAsync();

            var a = Levels.sidebar.Children;
            System.Collections.IList list = a;
            for (int i = 0; i < list.Count; i++)
            {
                Button level = (Button)list[i];
                var id = int.Parse(level.Content.ToString());
                level.Background = (Brush)FindResource("SecundaryBackgroundColor");
                level.Foreground = new SolidColorBrush(Colors.Black);

                foreach (var item in completedLevel)
                    if (item.UserId == CurrentUser.Id && item.LevelId == id && item.Passed == 1)
                    {
                        level.Background = Brushes.Green;
                        level.Foreground = Brushes.White;
                    }
            }

            
            frameContent.Navigate(Levels);
        }

        private void rdGuide_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(Guide);
        }

        private void rdConstructor_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(Constructor);
        }

        private void rdUsers_Click(object sender, RoutedEventArgs e)
        {
            // probably don't need it
            User user = (App.Current.MainWindow as MainWindow).CurrentUser;
            if (user != null)
            {
                Users.id.Text = user.Id.ToString();
                Users.login.Text = user.Login;
                Users.date.Text = user.Date.ToString();
            }
            else
            {
                Users.id.Text = Users.login.Text = Users.date.Text = string.Empty;
            }
            // -----------------------
            frameContent.Navigate(Users);
        }
    }
}
