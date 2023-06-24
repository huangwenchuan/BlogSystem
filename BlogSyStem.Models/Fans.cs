using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Models
{
    /// <summary>
    /// 粉丝表
    /// </summary>
    public class Fans : BaseEntity
    {
        /// <summary>
        /// 用户自己的Id
        /// </summary>
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public User User { get; set; }


        /// <summary>
        /// 用户关注的人的Id
        /// </summary>
        [ForeignKey(nameof(FocusUser))]
        public Guid FocusUserId { get;set;}

        public User FocusUser { get; set; }
    }
}
