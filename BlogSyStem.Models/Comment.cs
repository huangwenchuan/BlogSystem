using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Models
{
    /// <summary>
    /// 博客评论表
    /// </summary>
    public class Comment:BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 评论内容  maximumLength对应对应数据库的最大长度
        /// </summary>
        [Required,StringLength(maximumLength:600)]
        public string Content { get; set; }

        /// <summary>
        /// 评论文章Id
        /// </summary>
        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
