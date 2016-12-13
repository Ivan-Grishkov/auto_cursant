using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class Blog:EntityBase
    {
        [Required]
        public string UrlName { get; set; }
        public string YoutubeUrl { get; set; }
        public string DetailsText { get; set; }
        public string DetailsSectionHeader { get; set; }
        [Required]
        public string ListHeader { get; set; }
        public string ListDescription { get; set; }
        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public Metadata Metadata { get; set; }
        public string ThumbnailImageName { get; set; }
    }
}