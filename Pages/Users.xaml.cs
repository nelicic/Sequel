using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFUIKitProfessional.Authorization;

namespace WPFUIKitProfessional.Pages
{
    public partial class Users : Page
    {
        public Users()
        {
            InitializeComponent();
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
    }
}
