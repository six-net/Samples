using System;
using System.Collections.Generic;
using EZNEW.Development.Entity;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_Permission", Group = "Sys", Description = "权限")]
    public class Permission : ModelRecordEntity<Permission>
    {
        #region 属性

        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [EntityField(Description = "编码")]
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
        [EntityRelationField(typeof(PermissionGroup), nameof(PermissionGroup.Id), RelationBehavior.CascadingRemove)]
        public long Group { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [EntityField(Description = "说明")]
        public string Remark { get; set; }

        #endregion
    }
}