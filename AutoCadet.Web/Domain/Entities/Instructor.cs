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
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        [Required]
        public string UrlName { get; set; }
        public int? Price { get; set; }
        public int SortingNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
        public string ThumbnailImageName { get; set; }
        public virtual InstructorDetails InstructorDetails { get; set; }
        public virtual ISet<Comment> Comments { get; set; }
    }
}