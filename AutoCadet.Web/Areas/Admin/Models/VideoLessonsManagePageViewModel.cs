using System.ComponentModel.DataAnnotations;
using AutoCadet.Models;

namespace AutoCadet.Areas.Admin.Models
{
    public class VideoManagePageViewModel
    {
        [Required]
        public VideoViewModel VideoViewModel { get; set; }
        [Required]
        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}