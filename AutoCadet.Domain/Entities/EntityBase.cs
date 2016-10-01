using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; } 
    }
}