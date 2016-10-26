using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class CallMeViewModel
    {
        public int Id { get; set; }
        public int? InstructorId { get; set; }
        [Required]
        public string Phone { get; set; }
        public bool IsHandled { get; set; }
    }
}