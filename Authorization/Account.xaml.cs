using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using WPFUIKitProfessional.Models;

namespace WPFUIKitProfessional.Authorization
{
    public partial class Account : Window
    {
        ApplicationContext db = new ApplicationContext();
        public Account()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Users.Load();
            DataContext = db.Users.Local.ToBindingList();
        }

        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public async Task<bool> IsAuthorized(string login)
        {
            var containsLogin = await db.Users.AnyAsync(x => x.Login == login);
            return containsLogin;
        }
        public void Registration()
        {
            authorizationFrameContent.Navigate(new Registration());
        }
        public void Login()
        {
            authorizationFrameContent.Navigate(new Login());
        }
    }
}
