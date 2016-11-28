using System;
using System.ComponentModel.DataAnnotations;

namespace AutoCadet.Domain.Entities
{
    public class Obuchenie:EntityBase
    {
        [Required]
        public string UrlName { get; set; }
        public string DetailText { get; set; }
        [Required]
        public string ListHeader { get; set; }
        public string ListDescription { get; set; }
        [Required]
        public string ListIcon { get; set; }
        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public Metadata Metadata { get; set; }
    }
}