using System;
using EZNEW.Develop.Entity;
using EZNEW.Framework.Extension;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Serializable]
    [Entity("Sys_UserRole", "Sys", "用户角色")]
    public class UserRoleEntity : BaseEntity<UserRoleEntity>
    {
        #region	字段

        /// <summary>
        /// 用户
        /// </summary>
        [EntityField(Description = "用户", PrimaryKey = true)]
        [EntityRelation(typeof(UserEntity),"SysNo")]
        public long UserSysNo
        {
            get { return valueDict.GetValue<long>("UserSysNo"); }
            set { valueDict.SetValue("UserSysNo", value); }
        }

        /// <summary>
        /// 角色
        /// </summary>
        [EntityField(Description = "角色", PrimaryKey = true)]
        [EntityRelation(typeof(RoleEntity), "SysNo")]
        public long RoleSysNo
        {
            get { return valueDict.GetValue<long>("RoleSysNo"); }
            set { valueDict.SetValue("RoleSysNo", value); }
        }

        #endregion
    }
}