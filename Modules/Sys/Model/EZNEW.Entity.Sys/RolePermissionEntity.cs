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
        [EntityField(Description = "角色", PrimaryKey = true)]
        [EntityRelation(typeof(RoleEntity), "Id")]
        [EntityRelation(typeof(UserRoleEntity), "RoleId")]
        public long RoleId
        {
            get { return valueDict.GetValue<long>("RoleId"); }
            set { valueDict.SetValue("RoleId", value); }
        }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", PrimaryKey = true)]
        [EntityRelation(typeof(PermissionEntity), "Id")]
        public long PermissionId
        {
            get { return valueDict.GetValue<long>("PermissionId"); }
            set { valueDict.SetValue("PermissionId", value); }
        }
    }
}