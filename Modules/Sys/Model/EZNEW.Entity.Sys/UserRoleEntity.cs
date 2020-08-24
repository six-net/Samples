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
        [EntityField(Description = "用户", PrimaryKey = true)]
        [EntityRelation(typeof(UserEntity), "Id")]
        public long UserId
        {
            get { return valueDict.GetValue<long>("UserId"); }
            set { valueDict.SetValue("UserId", value); }
        }

        /// <summary>
        /// 角色
        /// </summary>
        [EntityField(Description = "角色", PrimaryKey = true)]
        [EntityRelation(typeof(RoleEntity), "Id")]
        public long RoleId
        {
            get { return valueDict.GetValue<long>("RoleId"); }
            set { valueDict.SetValue("RoleId", value); }
        }
    }
}