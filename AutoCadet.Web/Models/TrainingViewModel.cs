using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AutoCadet.Models
{
    public class ObuchenieViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UrlName { get; set; }
        [AllowHtml]
        public string DetailText { get; set; }
        [Required]
        public string ListHeader { get; set; }
        public string ListDescription { get; set; }
        [Required]
        public string ListIcon { get; set; }
        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public MetadataInfoViewModel Metadata { get; set; }
        public byte[] ThumbnailImageFile { get; set; }
    }
}