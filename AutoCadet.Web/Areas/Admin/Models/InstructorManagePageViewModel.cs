using System.ComponentModel.DataAnnotations;
using AutoCadet.Models;

namespace AutoCadet.Areas.Admin.Models
{
    public class InstructorManagePageViewModel
    {
        [Required]
        public InstructorDetailsViewModel InstructorDetails { get; set; }
        [Required]
        public InstructorViewModel Instructor { get; set; }
        [Required]
        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}