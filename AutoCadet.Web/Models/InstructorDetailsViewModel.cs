using System.Web.Mvc;

namespace AutoCadet.Models
{
    public class InstructorDetailsViewModel
    {
        public int Id { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public byte[] DetailsImage { get; set; }
        public byte[] VehicleImage { get; set; }
        [AllowHtml]
        public string VehicleDescriprion { get; set; }
        public string VehicleTitle { get; set; }
        public string VehicleSecondaryTitle { get; set; }
        public int FuelConsumption { get; set; }

        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}