using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        [Display(Name ="分类名称")]
        public string CategoryName { get; set; }
    }
}
