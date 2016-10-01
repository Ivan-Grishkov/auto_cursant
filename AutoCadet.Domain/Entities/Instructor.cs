using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Index("UX_UniqueUrl", IsUnique = true)]
        [Required]
        public string UrlName { get; set; }
        public bool IsActive { get; set; }
        public ImageFile ThumbnailImage { get; set; }
        public ISet<Comment> Comments { get; set; }
    }
}