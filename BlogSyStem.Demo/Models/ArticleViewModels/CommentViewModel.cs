using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSyStem.Demo.Models.ArticleViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}