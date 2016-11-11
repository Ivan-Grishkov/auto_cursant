using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class CallMeViewModel
    {
        public CallMeViewModel()
        {
            CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public int? InstructorId { get; set; }
        public string InstructorName { get; set; }

        [Required]
        public string Phone { get; set; }
        public string RequesterName { get; set; }
        public bool IsHandled { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}