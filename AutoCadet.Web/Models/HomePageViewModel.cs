using System.Collections.Generic;

namespace AutoCadet.Models
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            NewComment = new CommentViewModel();
        }

        public IList<InstructorGridItemViewModel> InstructorGridItems { get; set; }
        public CommentViewModel NewComment { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
    }
}