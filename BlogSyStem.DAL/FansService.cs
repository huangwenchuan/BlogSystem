using BlogSyStem.IDAL;
using BlogSyStem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.DAL
{
    public class FansService : BaseService<Models.Fans>, IFansService
    {
        public FansService() : base(new BlogContext())
        {

        }
    }
}
