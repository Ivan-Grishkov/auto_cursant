using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class Comment:EntityBase
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public bool IsVisibleInList { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public Instructor Instructor { get; set; }
    }
}