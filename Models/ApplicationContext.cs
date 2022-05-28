using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUIKitProfessional.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("Account")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<CompletedLevel> CompletedLevels { get; set; }
    }
}
