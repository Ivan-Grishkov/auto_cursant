using System.ComponentModel.DataAnnotations;
using AutoCadet.Models;

namespace AutoCadet.Areas.Admin.Models
{
    public class ObuchenieManagePageViewModel
    {
        [Required]
        public ObuchenieViewModel ObuchenieViewModel { get; set; }
        [Required]
        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}