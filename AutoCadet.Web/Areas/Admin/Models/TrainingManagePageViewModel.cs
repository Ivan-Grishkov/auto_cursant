using System.ComponentModel.DataAnnotations;
using AutoCadet.Models;

namespace AutoCadet.Areas.Admin.Models
{
    public class TrainingManagePageViewModel
    {
        [Required]
        public TrainingViewModel TrainingViewModel { get; set; }
        [Required]
        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}