using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class CallMe:EntityBase
    {
        [Required]
        public string Phone { get; set; }
        public bool IsHandled { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}