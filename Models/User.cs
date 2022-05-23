using System;
using System.ComponentModel.DataAnnotations;

namespace WPFUIKitProfessional.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
        public User() { }
        public User(string login, string password)
        {
            Login = login;
            Password = password;
            Date = DateTime.Now;
        }
    }
}
