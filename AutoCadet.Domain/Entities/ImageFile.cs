using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class ImageFile:EntityBase
    {
        [Required]
        public byte[] Bytes { get; set; }
    }
}