using EZNEW.Develop.CQuery;
using EZNEW.Framework.Paging;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Repository
{
    /// <summary>
    /// 角色存储
    /// </summary>
    public interface IRoleRepository : IAggregationRepository<Role>
    {
        #region 获取用户绑定的角色

        /// <summary>
        /// 获取用户绑定的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        List<Role> GetUserBindRole(long userId);

        #endregion
    }
}
