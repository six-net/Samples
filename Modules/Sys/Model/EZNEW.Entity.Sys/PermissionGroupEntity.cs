using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 权限分组
    /// </summary>
    [Serializable]
    [Entity("Sys_PermissionGroup", "Sys", "权限分组")]
    public class PermissionGroupEntity : BaseEntity<PermissionGroupEntity>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 上级分组
        /// </summary>
        [EntityField(Description = "上级分组")]
        public long Parent { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [EntityField(Description = "说明")]
        public string Remark { get; set; }
    }
}