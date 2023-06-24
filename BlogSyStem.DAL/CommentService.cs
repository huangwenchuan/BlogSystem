﻿using BlogSyStem.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.DAL
{
    public class CommentService : BaseService<Models.Comment>, ICommentService
    {
        public CommentService() : base(new Models.BlogContext())
        {

        }
    }
}
