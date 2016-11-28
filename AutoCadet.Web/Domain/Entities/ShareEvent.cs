using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class ShareEvent:EntityBase
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