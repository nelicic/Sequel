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
            Login();
        }
        public async Task<bool> IsAuthorized(string login)
        {
            return await db.Users.AnyAsync(x => x.Login == login);
        }
        public async Task<bool> IsAuthorized(string login, string password)
        {
            return await db.Users.AnyAsync(x => x.Login == login && x.Password == password);
        }
        public async Task<User> GetUser(string login, string password)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }

        public void Registration()
        {
            authorizationFrameContent.Navigate(new Registration());
            Title = "Registration";
        }
        public void Login()
        {
            authorizationFrameContent.Navigate(new Login());
            Title = "Login";
        }
    }
}
