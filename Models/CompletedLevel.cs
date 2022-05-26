using System.ComponentModel.DataAnnotations;

namespace WPFUIKitProfessional.Models
{
    public class CompletedLevel
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LevelId { get; set; }
        public int Passed { get; set; }

        public CompletedLevel() { }

        public CompletedLevel(int userId, int levelId, int passed)
        {
            UserId = userId;
            LevelId = levelId;
            Passed = passed;
        }
    }
}
