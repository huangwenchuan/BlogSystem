using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enumeration;

namespace BlogSyStem.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User:BaseEntity
    {
        [Required,Column(TypeName ="varchar"),StringLength(maximumLength:40),EmailAddress]
        public string Email { get; set; }

        [Required, Column(TypeName = "varchar"), StringLength(maximumLength: 16)]
        public string PassWord { get; set; }

        public string ImagePath { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(maximumLength: 50)]
        public string NickName { get; set; } = "用户_" + (new Random().Next(1, 999999999)).ToString();

        /// <summary>
        /// 性别
        /// </summary>
        public GenderEnum? Sex { get; set; }
    }
}
