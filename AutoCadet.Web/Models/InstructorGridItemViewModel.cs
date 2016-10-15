namespace AutoCadet.Models
{
    //TODO[2016/10/15 IG]: del it
    public class InstructorGridItemViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; }
        public int SortingNumber { get; set; }

        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string UrlName { get; set; }
        public decimal Price { get; set; }
        public byte[] ThumbnailImage { get; set; }
    }
}