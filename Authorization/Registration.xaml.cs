using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFUIKitProfessional.Models;
using WPFUIKitProfessional.Service;
using System.Text.RegularExpressions;

namespace WPFUIKitProfessional.Authorization
{
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void LoginBtn_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void LoginBtn_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.IBeam;
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var account = Window.GetWindow(this);
            (account as Account).Login();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == string.Empty || pb.Password == string.Empty || pb2.Password == string.Empty)
            {
                alert.Foreground = Brushes.Red;
                alert.Text = "Fill all fields";
                return;
            }
            if (!pb.Password.Equals(pb2.Password) && pb.Password != string.Empty)
            {
                alert.Foreground = Brushes.Red;
                alert.Text = "Passwords are different";
                return;
            }

            alert.TextWrapping = TextWrapping.Wrap;
            var password = pb.Password;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            if (!(hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password)))
            {
                alert.Foreground = Brushes.Red;
                alert.Text = "Password must include at least one capital letter and one number";
                return;
            }
            alert.Text = string.Empty;

            var account = (Account)Window.GetWindow(this);
            if (!(account.IsAuthorized(login.Text).Result))
            {
                account.AddUser(new User(login.Text, Encryption.GetHashString(pb.Password)));
            }
            else
            {
                alert.Foreground = Brushes.Red;
                alert.Text = "Login is already used";
            }
        }
    }
}
