using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class Instructor:EntityBase
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        [Required]
        public string UrlName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int SortingNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public ImageFile ThumbnailImage { get; set; }
        public ISet<Comment> Comments { get; set; }
    }
}