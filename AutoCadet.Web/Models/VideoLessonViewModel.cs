using System.ComponentModel.DataAnnotations;
using AutoCadet.Domain.Entities;

namespace AutoCadet.Models
{
    public class VideoLessonViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UrlName { get; set; }
        [Required]
        public string YoutubeUrl { get; set; }
        public string Text { get; set; }
        public Metadata Metadata { get; set; }
    }
}