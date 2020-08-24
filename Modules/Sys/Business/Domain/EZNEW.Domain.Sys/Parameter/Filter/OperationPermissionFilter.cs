using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 操作功能绑定权限筛选
    /// </summary>
    public class OperationPermissionFilter : PermissionFilter
    {
        /// <summary>
        /// 操作功能筛选
        /// </summary>
        public OperationFilter OperationFilter { get; set; }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery query = base.CreateQuery() ?? QueryManager.Create<PermissionEntity>(this);

            #region 授权操作筛选

            if (OperationFilter != null)
            {
                IQuery operationQuery = OperationFilter.CreateQuery();
                if (operationQuery != null)
                {
                    //功能绑定权限
                    IQuery permissionOperationQuery = QueryManager.Create<PermissionOperationEntity>();
                    permissionOperationQuery.EqualInnerJoin(operationQuery);
                    query.EqualInnerJoin(permissionOperationQuery);
                }
            }

            #endregion

            return query;
        }
    }
}
