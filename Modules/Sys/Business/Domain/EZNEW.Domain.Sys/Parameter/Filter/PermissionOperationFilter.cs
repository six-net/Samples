using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using System.Collections.Generic;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 权限绑定的操作筛选信息
    /// </summary>
    public class PermissionOperationFilter : OperationFilter
    {
        /// <summary>
        /// 权限筛选信息
        /// </summary>
        public PermissionFilter PermissionFilter { get; set; }

        /// <summary>
        /// 根据筛选条件创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery query = base.CreateQuery() ?? QueryManager.Create<OperationEntity>(this);

            #region 权限筛选

            if (PermissionFilter != null)
            {
                IQuery permissionQuery = PermissionFilter.CreateQuery();
                if (permissionQuery != null && !permissionQuery.Criterias.IsNullOrEmpty())
                {
                    //权限和操作绑定
                    IQuery permissionOperationQuery = QueryManager.Create<PermissionOperationEntity>();
                    permissionOperationQuery.EqualInnerJoin(permissionQuery);
                    query.EqualInnerJoin(permissionOperationQuery);
                }
            }

            #endregion

            return query;
        }
    }
}
