using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSyStem.Demo.Models.ArticleViewModels
{
    public class CategoryViewModel
    {
        [Required,StringLength(20,MinimumLength =2),Display(Name ="分类名称")]
        public string CategoryName { get; set; }
    }
}