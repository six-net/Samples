using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 权限存储
    /// </summary>
    public interface IPermissionRepository : IAggregationRepository<Permission>
    {
        #region 根据分组删除权限

        /// <summary>
        /// 根据分组删除权限
        /// </summary>
        /// <param name="groups">分组信息</param>
        /// <param name="activationOptions">操作配置项</param>
        void RemovePermissionByGroup(IEnumerable<PermissionGroup> groups, ActivationOptions activationOptions);

        /// <summary>
        /// 根据分组删除权限
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <param name="activationOptions">操作配置项</param>
        void RemovePermissionByGroup(IQuery query, ActivationOptions activationOptions);

        #endregion
    }
}
