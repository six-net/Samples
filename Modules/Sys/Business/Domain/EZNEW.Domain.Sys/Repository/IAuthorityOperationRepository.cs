using EZNEW.Develop.CQuery;
using EZNEW.Framework.Paging;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 授权操作存储
    /// </summary>
    public interface IAuthorityOperationRepository : IAggregationRepository<AuthorityOperation>
    {
        #region 根据操作分组删除分组下的授权操作

        /// <summary>
        /// 根据操作分组删除分组下的授权操作
        /// </summary>
        /// <param name="groups">要移除操作的分组</param>
        void RemoveOperationByGroup(IEnumerable<AuthorityOperationGroup> groups, ActivationOption activationOption = null);

        /// <summary>
        /// 根据操作分组删除分组下的授权操作
        /// </summary>
        /// <param name="query">query</param>
        void RemoveOperationByGroup(IQuery query, ActivationOption activationOption = null);

        #endregion
    }
}
