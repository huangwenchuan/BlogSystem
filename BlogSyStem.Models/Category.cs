﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Models
{
    /// <summary>
    /// 博客分类表
    /// </summary>
    public class Category:BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 用户外键
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
