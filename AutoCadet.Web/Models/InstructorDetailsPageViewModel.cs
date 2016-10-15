using System.Collections.Generic;

namespace AutoCadet.Models
{
    public class InstructorDetailsPageViewModel
    {
        public InstructorViewModel Instructor { get; set; }
        public InstructorDetailsViewModel InstructorDetails { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
        public CommentViewModel NewComment { get; set; }
    }
}