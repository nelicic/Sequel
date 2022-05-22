using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFUIKitProfessional.Models;

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
            account.Title = "Login";
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
            alert.Text = string.Empty;


            var account = (Account)Window.GetWindow(this);
            if (!(account.IsAuthorized(login.Text).Result))
            {
                account.AddUser(new User(login.Text, pb.Password));
                alert.Foreground = Brushes.Green;
                alert.Text = "Successful registration";
            }
            else
            {
                alert.Foreground = Brushes.Red;
                alert.Text = "Login is already used";
            }
                
        }
    }
}
