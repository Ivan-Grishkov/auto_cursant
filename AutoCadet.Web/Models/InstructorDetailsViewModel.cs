using System.Web.Mvc;

namespace AutoCadet.Models
{
    public class InstructorDetailsViewModel
    {
        public int Id { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string VehicleDescriprion { get; set; }
        public string VehicleTitle { get; set; }
        public string VehicleSecondaryTitle { get; set; }

        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}