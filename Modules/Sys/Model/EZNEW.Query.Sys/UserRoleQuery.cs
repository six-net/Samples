using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Query.Sys
{
    /// <summary>
    /// 用户角色查询
    /// </summary>
    [QueryEntity(typeof(UserRoleEntity))]
    public class UserRoleQuery : QueryModel<UserRoleQuery>
    {
        #region 属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserSysNo
        {
            get; set;
        }

        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleSysNo
        {
            get; set;
        }

        #endregion
    }
}
