using System;
using System.Collections.Generic;
using EZNEW.Development.Entity;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 已授权的操作
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_OperationPermission", Group = "Sys", Description = "已授权的操作")]
    public class OperationPermission : ModelEntity<OperationPermission>
    {
        /// <summary>
        /// 操作功能
        /// </summary>
        [EntityField(Description = "操作功能", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Operation), nameof(Operation.Id), RelationBehavior.CascadingRemove)]
        public long OperationId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Permission), nameof(Permission.Id), RelationBehavior.CascadingRemove)]
        public long PermissionId { get; set; }
    }
}