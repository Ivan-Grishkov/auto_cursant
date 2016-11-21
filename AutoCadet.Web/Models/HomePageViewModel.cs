using System.Collections.Generic;

namespace AutoCadet.Models
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            NewComment = new CommentViewModel();
        }

        public IList<InstructorViewModel> InstructorGridItems { get; set; }
        public IList<VideoViewModel> VideoGridItems { get; set; }
        public IList<ObuchenieViewModel> Obuchenie { get; set; }
        public IList<BlogViewModel> Blogs { get; set; }
        public CommentViewModel NewComment { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
    }
}