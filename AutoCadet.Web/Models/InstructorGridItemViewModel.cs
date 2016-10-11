namespace AutoCadet.Models
{
    public class InstructorGridItemViewModel: InstructorGridItemBaseViewModel
    {
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string UrlName { get; set; }
        public decimal Price { get; set; }
        public byte[] ThumbnailImage { get; set; }
    }
}