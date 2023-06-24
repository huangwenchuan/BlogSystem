using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Models
{
    /// <summary>
    /// 文章类别表
    /// </summary>
    public class ArticleToCategory:BaseEntity
    {
        /// <summary>
        /// 类别编号
        /// </summary>
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
