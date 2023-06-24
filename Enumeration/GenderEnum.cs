using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Enumeration
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum GenderEnum
    {
        [Display(Name ="未知")]
        None,
        [Display(Name = "男")]
        Male,
        [Display(Name = "女")]
        Female
    }
}
