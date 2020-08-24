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
        [EntityField(Description = "用户", PrimaryKey = true)]
        public long UserId
        {
            get { return valueDict.GetValue<long>("UserId"); }
            set { valueDict.SetValue("UserId", value); }
        }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", PrimaryKey = true)]
        public long PermissionId
        {
            get { return valueDict.GetValue<long>("PermissionId"); }
            set { valueDict.SetValue("PermissionId", value); }
        }

        /// <summary>
        /// 禁用
        /// </summary>
        [EntityField(Description = "禁用")]
        public bool Disable
        {
            get { return valueDict.GetValue<bool>("Disable"); }
            set { valueDict.SetValue("Disable", value); }
        }
    }
}