using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using System.Collections.Generic;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 管理用户筛选
    /// </summary>
    public class RoleUserFilter : UserFilter
    {
        /// <summary>
        /// 角色筛选
        /// </summary>
        public RoleFilter RoleFilter { get; set; }

        /// <summary>
        /// 根据筛选条件创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            var query = base.CreateQuery() ?? QueryManager.Create<UserEntity>(this);

            #region 角色筛选

            if (RoleFilter != null)
            {
                IQuery roleQuery = RoleFilter.CreateQuery();
                if (roleQuery != null && !roleQuery.Criterias.IsNullOrEmpty())
                {
                    IQuery userRoleQuery = QueryManager.Create<UserRoleEntity>();
                    userRoleQuery.EqualInnerJoin(roleQuery);
                    query.EqualInnerJoin(userRoleQuery);
                }
            }

            #endregion

            return query;
        }
    }
}
