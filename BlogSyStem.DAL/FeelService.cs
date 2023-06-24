using BlogSyStem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSyStem.IDAL;

namespace BlogSyStem.DAL
{
    public class FeelService : BaseService<Models.Feel>, IFeelService
    {
        public FeelService() : base(new BlogContext())
        {

        }
    }
}
