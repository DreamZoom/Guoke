using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Guoke
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// 
    [InheritedExport]
    public class EntityBase 
    {
        public EntityBase()
        {
            this.Sort = 99;
            this.CreateTime = DateTime.Now;
        }
        
        /// <summary>
        /// 编号，唯一id，递增列
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "编号")]
        public int ID { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [Display(Name = "排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 记录创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
