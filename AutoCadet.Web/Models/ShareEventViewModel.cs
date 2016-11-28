using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class ShareEventViewModel
    {
        [Required]
        public string Header { get; set; }
        public string Text { get; set; }
        public string TinyText { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}