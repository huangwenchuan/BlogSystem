using BlogSyStem.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.DAL
{
    public class CategoryService:BaseService<Models.Category>,ICategoryService
    {
        public CategoryService():base(new Models.BlogContext())
        {

        } 
    }
}
