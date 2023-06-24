using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Models
{
    [Description("阅读信息表")]
    public class ReadingInfo : BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [ForeignKey(nameof(User)), Description("用户Id")]
        public Guid UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 文章Id
        /// </summary>
        [ForeignKey(nameof(Article)), Description("文章Id")]
        public Guid ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
