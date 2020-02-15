using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Entity.Sys;
using EZNEW.Framework.Extension;
using EZNEW.DataAccessContract.Sys;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Query.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 授权操作存储
    /// </summary>
    public class AuthorityOperationRepository : DefaultAggregationRepository<AuthorityOperation, AuthorityOperationEntity, IAuthorityOperationDataAccess>, IAuthorityOperationRepository
    {
        #region 根据操作分组删除分组下的授权操作

        /// <summary>
        /// 根据操作分组删除分组下的授权操作
        /// </summary>
        /// <param name="groups">要移除操作的分组</param>
        public void RemoveOperationByGroup(IEnumerable<AuthorityOperationGroup> groups, ActivationOption activationOption = null)
        {
            if (groups.IsNullOrEmpty())
            {
                return;
            }
            IEnumerable<long> groupIds = groups.Select(c => c.SysNo).Distinct().ToList();
            IQuery query = QueryFactory.Create<AuthorityOperationQuery>(c => groupIds.Contains(c.Group));
            Remove(query, activationOption);
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
            var removeGroupQuery = query.Clone();
            removeGroupQuery.QueryFields.Clear();
            removeGroupQuery.AddQueryFields<AuthorityOperationGroupQuery>(c => c.SysNo);
            //remove operation
            var removeOperationQuery = QueryFactory.Create<AuthorityOperationQuery>();
            removeOperationQuery.And<AuthorityOperationQuery>(c => c.Group, CriteriaOperator.In, removeGroupQuery);
            Remove(removeOperationQuery, activationOption);
        }

        #endregion
    }
}
