using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 操作功能存储
    /// </summary>
    public interface IOperationRepository : IAggregationRepository<Operation>
    {
        #region 根据操作分组删除分组下的授权操作

        /// <summary>
        /// 根据操作分组删除分组下的授权操作
        /// </summary>
        /// <param name="groups">要移除操作的分组</param>
        void RemoveOperationByGroup(IEnumerable<OperationGroup> groups, ActivationOptions activationOptions = null);

        /// <summary>
        /// 根据操作分组删除分组下的授权操作
        /// </summary>
        /// <param name="query">query</param>
        void RemoveOperationByGroup(IQuery query, ActivationOptions activationOptions = null);

        #endregion
    }
}
