using System;
using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;

namespace EZNEW.Query.Sys
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [QueryEntity(typeof(RoleAuthorizeEntity))]
    public class RoleAuthorizeQuery : QueryModel<RoleAuthorizeQuery>
    {
        #region	属性

        /// <summary>
        /// 角色
        /// </summary>
        public long RoleSysNo
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

        #endregion
    }
}