using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFUIKitProfessional.Models;
using WPFUIKitProfessional.Pages;

namespace WPFUIKitProfessional.Authorization
{
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void LoginBtn_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.IBeam;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var account = Window.GetWindow(this);
            (account as Account).Registration();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == string.Empty || pb.Password == string.Empty)
            {
                alert.Foreground = Brushes.Red;
                alert.Text = "Fill all fields";
                return;
            }
            alert.Text = string.Empty;

            var account = (Account)Window.GetWindow(this);
            if (account.IsAuthorized(login.Text).Result)
            {
                if (account.IsAuthorized(login.Text, pb.Password).Result)
                {
                    (App.Current.MainWindow as MainWindow).CurrentUser = account.GetUser(login.Text, pb.Password).Result;
                    account.Close();
                }
                else
                {
                    alert.Foreground = Brushes.Red;
                    alert.Text = "Incorrect login or password";
                }
            }
            else
            {
                alert.Foreground = Brushes.Red;
                alert.Text = "Invalid login or password";
            }
        }
    }
}