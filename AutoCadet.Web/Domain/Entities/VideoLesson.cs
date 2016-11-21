using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class Video:EntityBase
    {
        [Required]
        public string UrlName { get; set; }
        [Required]
        public string YoutubeUrl { get; set; }
        public string Text { get; set; }
        public string ListHeader { get; set; }
        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public Metadata Metadata { get; set; }
        public ImageFile ThumbnailImageFile { get; set; }
    }
}