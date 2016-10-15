using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
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