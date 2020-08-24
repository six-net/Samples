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
    /// 授权操作存储
    /// </summary>
    public class OperationRepository : DefaultAggregationRepository<Operation, OperationEntity, IOperationDataAccess>, IOperationRepository
    {
        #region 根据操作分组删除分组下的授权操作

        /// <summary>
        /// 根据操作分组删除分组下的授权操作
        /// </summary>
        /// <param name="groups">要移除操作的分组</param>
        public void RemoveOperationByGroup(IEnumerable<OperationGroup> groups, ActivationOption activationOption = null)
        {
            if (groups.IsNullOrEmpty())
            {
                return;
            }
            IEnumerable<long> groupIds = groups.Select(c => c.Id);
            IQuery removeOperationQuery = QueryManager.Create<OperationEntity>(c => groupIds.Contains(c.Group));
            Remove(removeOperationQuery, activationOption);
        }

        /// <summary>
        /// 根据操作分组删除分组下的授权操作
        /// </summary>
        /// <param name="query">query</param>
        public void RemoveOperationByGroup(IQuery query, ActivationOption activationOption = null)
        {
            if (query == null)
            {
                return;
            }
            var removeGroupQuery = query.LightClone();
            removeGroupQuery.ClearQueryFields();
            removeGroupQuery.AddQueryFields<OperationGroupEntity>(c => c.Id);
            //remove operation
            var removeOperationQuery = QueryManager.Create<OperationEntity>();
            removeOperationQuery.And<OperationEntity>(c => c.Group, CriteriaOperator.In, removeGroupQuery);
            Remove(removeOperationQuery, activationOption);
        }

        #endregion
    }
}
