using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 用户角色筛选
    /// </summary>
    public class UserRoleFilter : RoleFilter
    {
        /// <summary>
        /// 用户筛选
        /// </summary>
        public UserFilter UserFilter { get; set; }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery roleQuery = base.CreateQuery() ?? QueryManager.Create<RoleEntity>(this);

            #region 用户筛选

            if (UserFilter != null)
            {
                IQuery userQuery = UserFilter.CreateQuery();
                if (userQuery != null)
                {
                    IQuery userRoleQuery = QueryManager.Create<UserRoleEntity>();
                    userRoleQuery.EqualInnerJoin(userQuery);
                    roleQuery.EqualInnerJoin(userRoleQuery);
                }
            }

            #endregion

            return roleQuery;
        }
    }
}
