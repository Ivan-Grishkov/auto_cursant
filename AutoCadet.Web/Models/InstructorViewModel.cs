using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Models
{
    public class InstructorViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Email { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        [Required]
        public string UrlName { get; set; }
        public int SortingNumber { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public decimal Price { get; set; }
        public byte[] ThumbnailImage { get; set; }
        [UIHint("Score")]
        public double? AverageScore { get; set; }
    }
}