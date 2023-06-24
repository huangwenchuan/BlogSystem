using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSyStem.Demo.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name = "邮箱：")]
        public string Email { get; set; }

        [Required, Display(Name = "密码："),DataType(dataType:DataType.Password)]
        [StringLength(50,MinimumLength =6)]
        public string PassWord { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}