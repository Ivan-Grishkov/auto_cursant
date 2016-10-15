namespace AutoCadet.Models
{
    public class InstructorDetailsViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] DetailsImage { get; set; }
        public byte[] VehicleImage { get; set; }
        public string VehicleDescriprion { get; set; }
        public string VehicleTitle { get; set; }
        public string VehicleSecondaryTitle { get; set; }
        public string FuelConsumption { get; set; }

        public MetadataInfoViewModel MetadataInfo { get; set; }
    }
}