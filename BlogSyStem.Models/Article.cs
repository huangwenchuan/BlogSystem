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
    /// 博客文章表
    /// </summary>
    public class Article : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 主体内容
        /// </summary>
        [Column(TypeName ="ntext"), Required]
        public string Content { get; set; }

        /// <summary>
        /// 主体内容样式
        /// </summary>
        public string ContentStyle { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int GoodCount { get; set; }

        /// <summary>
        /// 踩数量
        /// </summary>
        public int BadCount { get; set; }
    }
}
