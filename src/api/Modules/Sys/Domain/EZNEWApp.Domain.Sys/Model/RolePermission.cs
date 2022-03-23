using System;
using System.Collections.Generic;
using EZNEW.Development.Entity;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 角色授权
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_RolePermission", Group = "Sys", Description = "角色授权")]
    public class RolePermission : ModelEntity<RolePermission>
    {
        /// <summary>
        /// 角色
        /// </summary>
        [EntityField(Description = "角色", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Role), nameof(Role.Id), RelationBehavior.CascadingRemove)]
        [EntityRelationField(typeof(UserRole), nameof(UserRole.RoleId))]
        public long RoleId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Permission), nameof(Permission.Id), RelationBehavior.CascadingRemove)]
        public long PermissionId { get; set; }
    }
}