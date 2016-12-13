using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class InstructorViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }
        [Required]
        public string UrlName { get; set; }
        public int SortingNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
        [Required]
        public int Price { get; set; }
        public string ThumbnailImageName { get; set; }
        [UIHint("Score")]
        public double? AverageScore { get; set; }
    }
}