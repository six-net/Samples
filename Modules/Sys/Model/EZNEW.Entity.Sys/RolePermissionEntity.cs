using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 角色授权
    /// </summary>
    [Serializable]
    [Entity("Sys_RolePermission", "Sys", "角色授权")]
    public class RolePermissionEntity : BaseEntity<RolePermissionEntity>
    {
        /// <summary>
        /// 角色
        /// </summary>
        [EntityField(Description = "角色", Role = FieldRole.PrimaryKey)]
        [EntityRelation(typeof(RoleEntity), nameof(RoleEntity.Id))]
        [EntityRelation(typeof(UserRoleEntity), nameof(UserRoleEntity.RoleId))]
        public long RoleId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", Role = FieldRole.PrimaryKey)]
        [EntityRelation(typeof(PermissionEntity), nameof(PermissionEntity.Id))]
        public long PermissionId { get; set; }
    }
}