using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class VideoLesson:EntityBase
    {
        [Required]
        public string UrlName { get; set; }
        [Required]
        public string YoutubeUrl { get; set; }
        public string Text { get; set; }
        public Metadata Metadata { get; set; }
    }
}