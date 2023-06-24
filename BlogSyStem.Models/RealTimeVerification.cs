using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.Models
{
    [Description("实时验证")]
    public class RealTimeVerification:BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [ForeignKey(nameof(User)), Description("用户Id")]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Description("验证码")]
        public string Code { get; set; }

        [Column(TypeName = "varchar"), StringLength(maximumLength: 40), EmailAddress]
        public string Email { get; set; }
    }
}
