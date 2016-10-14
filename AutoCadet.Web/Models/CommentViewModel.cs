﻿using System.ComponentModel.DataAnnotations;

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
        public int Score { get; set; }
        public bool IsVisibleInList { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
    }
}