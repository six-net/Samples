using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 用户授权
    /// </summary>
    [Serializable]
    [Entity("Sys_UserAuthorize", "Sys", "用户授权")]
    public class UserAuthorizeEntity : BaseEntity<UserAuthorizeEntity>
    {
        #region	字段

        /// <summary>
        /// 用户
        /// </summary>
        [EntityField(Description = "用户", PrimaryKey = true)]
        public long UserSysNo
        {
            get { return valueDict.GetValue<long>("UserSysNo"); }
            set { valueDict.SetValue("UserSysNo", value); }
        }

        /// <summary>
        /// 权限
        /// </summary>
        [EntityField(Description = "权限", PrimaryKey = true)]
        public long AuthoritySysNo
        {
            get { return valueDict.GetValue<long>("AuthoritySysNo"); }
            set { valueDict.SetValue("AuthoritySysNo", value); }
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

        #endregion
    }
}