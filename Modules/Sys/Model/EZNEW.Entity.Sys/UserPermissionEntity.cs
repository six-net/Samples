using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 用户授权
    /// </summary>
    [Serializable]
    [Entity("Sys_UserPermission", "Sys", "用户授权")]
    public class UserPermissionEntity : BaseEntity<UserPermissionEntity>
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
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", Role = FieldRole.PrimaryKey)]
        [EntityRelation(typeof(PermissionEntity), nameof(PermissionEntity.Id))]
        public long PermissionId
        {
            get => GetPropertyValue<long>(nameof(PermissionId));
            set => SetPropertyValue(nameof(PermissionId), value);
        }

        /// <summary>
        /// 禁用
        /// </summary>
        [EntityField(Description = "禁用")]
        public bool Disable
        {
            get => GetPropertyValue<bool>(nameof(Disable));
            set => SetPropertyValue(nameof(Disable), value);
        }
    }
}