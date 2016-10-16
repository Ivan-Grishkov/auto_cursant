using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class CommentViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        [UIHint("Score2")]
        [Required]
        public double? Score { get; set; }

        [UIHint("Score")]
        [Required]
        public double? DisplayScore
        {
            get
            {
                return Score;
            }
        }

        public bool IsVisibleInList { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public int InstructorId { get; set; }

        public string InstructorName { get; set; }
    }
}
