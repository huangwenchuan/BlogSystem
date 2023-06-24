using BlogSyStem.IDAL;
using BlogSyStem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.DAL
{
    public class ReadingInfoService : BaseService<Models.ReadingInfo>, IReadingInfoService
    {
        public ReadingInfoService() : base(new BlogContext())
        {

        }
    }
}
