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
    /// <summary>
    /// [Table("Template")]表名称
    /// </summary>
    [Table("Template")]
    public class Template : BaseEntity
    {
        [Display(Name ="短整型"), Description("短整型")]
        public short? SmallInt { get; set; }

        [Display(Name = "超短整型")]
        public byte? TinyInt { get; set; }

        [Column(TypeName = "text"), Display(Name = "文本类型")]
        public string Content { get; set; }

        [Column(TypeName ="bigint"), Display(Name = "长整型")]
        public int? BigInt { get; set; }

        
    }
}
