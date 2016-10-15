using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCadet.Domain.Entities
{
    public class InstructorDetails
    {

        [ForeignKey("Instructor")]
        public int Id { get; set; }

        public string Description { get; set; }
        public virtual ImageFile DetailsImage { get; set; }
        public virtual ImageFile VehicleImage { get; set; }
        public string VehicleDescriprion { get; set; }
        public string VehicleTitle { get; set; }
        public string VehicleSecondaryTitle { get; set; }
        public string FuelConsumption { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Metadata Metadata { get; set; }
        public virtual ISet<Comment> Comments { get; set; }
    }
}