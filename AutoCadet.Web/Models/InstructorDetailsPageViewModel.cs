using System.Collections.Generic;

namespace AutoCadet.Models
{
    public class InstructorDetailsPageViewModel
    {
        public InstructorManageViewModel Instructor { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
        public CommentViewModel NewComment { get; set; }
    }
}