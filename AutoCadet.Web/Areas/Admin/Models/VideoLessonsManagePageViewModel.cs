using System.ComponentModel.DataAnnotations;
using AutoCadet.Models;

namespace AutoCadet.Areas.Admin.Models
{
    public class VideoLessonsManagePageViewModel
    {
        [Required]
        public VideoLessonViewModel VideoLessonViewModel { get; set; }
        [Required]
        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}