using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUIKitProfessional.Models
{
    internal class PlayListContext : DbContext
    {
        public PlayListContext() : base("PlayList")
        { }
    }
}
