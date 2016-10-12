using System.Collections.Generic;

namespace AutoCadet.Models
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            CommentViewModel = new CommentNewViewModel();
        }

        public IList<InstructorGridItemViewModel> InstructorGridItemViewModels { get; set; }
        public CommentNewViewModel CommentViewModel { get; set; }
    }
}