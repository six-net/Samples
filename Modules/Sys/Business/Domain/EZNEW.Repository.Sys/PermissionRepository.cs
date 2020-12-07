using System.Collections.Generic;
using System.Linq;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 权限存储
    /// </summary>
    public class PermissionRepository : DefaultAggregationRepository<Permission, PermissionEntity, IPermissionDataAccess>, IPermissionRepository
    {
        #region 根据分组删除权限

        /// <summary>
        /// 根据分组删除权限
        /// </summary>
        /// <param name="groups">分组信息</param>
        public void RemovePermissionByGroup(IEnumerable<PermissionGroup> groups, ActivationOptions activationOptions)
        {
            if (groups.IsNullOrEmpty())
            {
                return;
            }
            IEnumerable<long> groupIds = groups.Select(c => c.Id);
            IQuery removePermissionQuery = QueryManager.Create<PermissionEntity>(c => groupIds.Contains(c.Group));
            Remove(removePermissionQuery, activationOptions);
        }

        /// <summary>
        /// 根据分组删除权限
        /// </summary>
        /// <param name="query">query</param>
        public void RemovePermissionByGroup(IQuery query, ActivationOptions activationOptions)
        {
            if (query == null)
            {
                return;
            }
            var removeGroupQuery = query.LightClone();
            removeGroupQuery.ClearQueryFields();
            removeGroupQuery.AddQueryFields<PermissionGroupEntity>(c => c.Id);
            //remove permission
            var removePermissionQuery = QueryManager.Create<PermissionEntity>();
            removePermissionQuery.And<PermissionEntity>(c => c.Group, CriteriaOperator.In, removeGroupQuery);
            Remove(removePermissionQuery, activationOptions);
        }
        #endregion
    }
}
