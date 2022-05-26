using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUIKitProfessional.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Path { get; set; }
        public int Visible { get; set; }
        public string SQLanswer { get; set; }
        public Level() { }
    }
}
