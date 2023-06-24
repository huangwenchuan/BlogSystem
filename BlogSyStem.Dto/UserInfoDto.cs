using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Dto
{
    public class UserInfoDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 粉丝数量
        /// </summary>
        public int FansCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int FocusCount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
