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
    /// 权限存储
    /// </summary>
    public interface IAuthorityRepository : IAggregationRepository<Authority>
    {
        #region 根据分组删除权限

        /// <summary>
        /// 根据分组删除权限
        /// </summary>
        /// <param name="groups">分组信息</param>
        /// <param name="activationOption">操作配置项</param>
        void RemoveAuthorityByGroup(IEnumerable<AuthorityGroup> groups, ActivationOption activationOption);

        /// <summary>
        /// 根据分组删除权限
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <param name="activationOption">操作配置项</param>
        void RemoveAuthorityByGroup(IQuery query, ActivationOption activationOption);

        #endregion
    }
}
