using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class Comment:EntityBase
    {
        [Required]
        public string Text { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Score { get; set; }
        [Required]
        public Instructor Instructor { get; set; }
    }
}