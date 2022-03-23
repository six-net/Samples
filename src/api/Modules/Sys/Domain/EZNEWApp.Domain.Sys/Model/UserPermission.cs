using System;
using System.Collections.Generic;
using EZNEW.Development.Entity;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 用户授权
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_UserPermission", Group = "Sys", Description = "用户授权")]
    public class UserPermission : ModelEntity<UserPermission>
    {
        /// <summary>
        /// 用户
        /// </summary>
        [EntityField(Description = "用户", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(User), nameof(User.Id), RelationBehavior.CascadingRemove)]
        public long UserId { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Permission), nameof(Permission.Id), RelationBehavior.CascadingRemove)]
        public long PermissionId { get; set; }

        /// <summary>
        /// 禁用
        /// </summary>
        [EntityField(Description = "禁用")]
        public bool Disable { get; set; }
    }
}