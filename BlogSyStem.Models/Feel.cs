using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enumeration;

namespace BlogSyStem.Models
{
    /// <summary>
    /// 用户点赞文章表
    /// </summary>
    public class Feel:BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 文章ID
        /// </summary>
        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }

        public Article Article { get; set; }

        public FeelEnum? FeelType { get; set; }
    }
}
