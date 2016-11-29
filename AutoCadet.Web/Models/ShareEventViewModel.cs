using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AutoCadet.Models
{
    public class ShareEventViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [AllowHtml]
        public string Text { get; set; }
        public string TinyText { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}