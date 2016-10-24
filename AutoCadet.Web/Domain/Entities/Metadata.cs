namespace AutoCadet.Domain.Entities
{
    public class Metadata:EntityBase
    {
        public string Description { get; set; }
        public string Info { get; set; }
        public string Keywords { get; set; }
        public string Title { get; set; }
        public string H1 { get; set; }
    }
}