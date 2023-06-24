using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSyStem.Demo.Models.ArticleViewModels
{
    public class ArticleViewModel
    {
        [Required,StringLength(120,MinimumLength =2),Display(Name ="标题")]
        public string Title { get; set; }
        
        public DateTime CreateTime { get; set; }

        [Required, MinLength(5), Display(Name = "文章")]
        public string Context { get; set; }

        [Display(Name ="用户文章分类")]
        public Guid[] CategoryIds { get; set; }
    }
}