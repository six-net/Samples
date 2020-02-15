using System;
using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;

namespace EZNEW.Query.Sys
{
    /// <summary>
    /// 用户授权
    /// </summary>
    [QueryEntity(typeof(UserAuthorizeEntity))]
    public class UserAuthorizeQuery : QueryModel<UserAuthorizeQuery>
    {
        #region	属性

        /// <summary>
        /// 用户
        /// </summary>
        public long UserSysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 权限
        /// </summary>
        public long AuthoritySysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disable
        {
            get;
            set;
        }

        #endregion
    }
}