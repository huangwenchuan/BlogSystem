using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSyStem.Demo.Models.UserViewModels
{
    public class RegisterViewModel
    {
        [Required,EmailAddress, Display(Name = "邮箱：")]
        public string Email { get; set; }

        [Required,StringLength(maximumLength:16,MinimumLength =6), Display(Name = "密码：")]
        public string PassWord { get; set; }


        [Required,Compare(otherProperty:nameof(PassWord)), Display(Name = "确认密码：")]
        public string Confirm { get; set; }
    }
}