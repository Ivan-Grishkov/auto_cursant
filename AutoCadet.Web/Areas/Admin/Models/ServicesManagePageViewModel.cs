using System.ComponentModel.DataAnnotations;
using AutoCadet.Models;

namespace AutoCadet.Areas.Admin.Models
{
    public class ServicesManagePageViewModel
    {
        [Required]
        public ServiceViewModel ServiceViewModel { get; set; }
        [Required]
        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}