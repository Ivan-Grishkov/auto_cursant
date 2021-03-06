﻿using System.Collections.Generic;

namespace AutoCadet.Models
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            NewComment = new CommentViewModel();
        }

        public IList<InstructorViewModel> InstructorGridItems { get; set; }
        public CommentViewModel NewComment { get; set; }
    }
}