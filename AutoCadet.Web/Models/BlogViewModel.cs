using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AutoCadet.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UrlName { get; set; }
        public string YoutubeUrl { get; set; }
        [AllowHtml]
        public string DetailsText { get; set; }
        public string DetailsSectionHeader { get; set; }
        public string ListHeader { get; set; }
        public string ListDescription { get; set; }
        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public MetadataInfoViewModel Metadata { get; set; }
        public byte[] ThumbnailImageFile { get; set; }
    }
}