using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class CommentNewViewModel
    {
        [Required]
        public string Text { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        [Required]
        public int InstructorId { get; set; }
    }
}