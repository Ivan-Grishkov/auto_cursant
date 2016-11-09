using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UrlName { get; set; }
        public string DetailText { get; set; }
        [Required]
        public string ListHeader { get; set; }
        [Required]
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