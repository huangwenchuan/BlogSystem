using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Dto
{
    public class ArticleDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name ="标题")]
        public string Title { get; set; }

        /// <summary>
        /// 主要内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [Display(Name ="邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 赞成数量
        /// </summary>
        [Display(Name = "赞成")]
        public int GoodCount { get; set; }

        /// <summary>
        /// 反对数量
        /// </summary>
        [Display(Name = "反对")]
        public int BadCount { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; } 

        /// <summary>
        /// 用户头像
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string[] CategoryName { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public Guid[] CategoryId { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int ReadingVolume { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 评论量
        /// </summary>
        public int CommentVolume { get; set; }

        /// <summary>
        /// 用户感觉
        /// </summary>
        public Enumeration.FeelEnum? Feel { get; set; }
    }
}
