using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enumeration
{
    /// <summary>
    /// 感觉枚举
    /// </summary>
    public enum FeelEnum
    {
        [Display(Name = "无")]
        None,
        [Display(Name = "赞成")]
        Good,
        [Display(Name = "反对")]
        Bad
    }
}