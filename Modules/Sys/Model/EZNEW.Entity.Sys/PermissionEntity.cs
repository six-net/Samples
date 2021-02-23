using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;
using EZNEW.Module.Sys;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    [Entity("Sys_Permission", "Sys", "权限")]
    public class PermissionEntity : BaseEntity<PermissionEntity>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [EntityField(Description = "权限编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [EntityField(Description = "权限类型")]
        public PermissionType Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public PermissionStatus Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 权限分组
        /// </summary>
        [EntityField(Description = "权限分组")]
        [EntityRelation(typeof(PermissionGroupEntity), nameof(PermissionGroupEntity.Id))]
        public long Group { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [EntityField(Description = "说明")]
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityField(Description = "添加时间")]
        public DateTime CreateDate { get; set; }
    }
}