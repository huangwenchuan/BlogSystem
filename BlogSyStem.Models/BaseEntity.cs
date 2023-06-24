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
    /// 公共实体类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键 Id默认主键 如果Id类型为Int就是标识列自增 
        /// 用ProjectId就要用Key 来表示这个字段是主键
        /// </summary>
        [Required,Key,Description("主键")]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否被删除（伪删除）
        /// </summary>
        [Description("是否删除")]
        public bool IsRemoved { get; set; } = false;

        /// <summary>
        /// 标识列自增字段
        /// </summary>
        [Description("序号")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerialNO { get; set; }
    }
}
