using System;
using System.Collections.Generic;
using EZNEW.Development.Entity;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_UserRole", Group = "Sys", Description = "用户角色")]
    public class UserRole : ModelEntity<UserRole>
    {
        /// <summary>
        /// 用户
        /// </summary>
        [EntityField(Description = "用户", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(User), nameof(User.Id), RelationBehavior.CascadingRemove)]
        public long UserId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [EntityField(Description = "角色", Role = FieldRole.PrimaryKey)]
        [EntityRelationField(typeof(Role), nameof(Role.Id), RelationBehavior.CascadingRemove)]
        public long RoleId { get; set; }
    }
}