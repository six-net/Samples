using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Serializable]
    [Entity("Sys_UserRole", "Sys", "用户角色")]
    public class UserRoleEntity : BaseEntity<UserRoleEntity>
    {
        /// <summary>
        /// 用户
        /// </summary>
        [EntityField(Description = "用户", Role = FieldRole.PrimaryKey)]
        [EntityRelation(typeof(UserEntity), nameof(UserEntity.Id))]
        public long UserId
        {
            get => GetPropertyValue<long>(nameof(UserId));
            set => SetPropertyValue(nameof(UserId), value);
        }

        /// <summary>
        /// 角色
        /// </summary>
        [EntityField(Description = "角色", Role = FieldRole.PrimaryKey)]
        [EntityRelation(typeof(RoleEntity), nameof(RoleEntity.Id))]
        public long RoleId
        {
            get => GetPropertyValue<long>(nameof(RoleId));
            set => SetPropertyValue(nameof(RoleId), value);
        }
    }
}