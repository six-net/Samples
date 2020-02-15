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
using EZNEW.Query.Sys;
using EZNEW.Develop.Domain.Repository;
using EZNEW.Develop.CQuery;

namespace EZNEW.Repository.Sys
{
    /// <summary>
    /// 角色资源存储
    /// </summary>
    public class RoleRepository : DefaultAggregationRepository<Role, RoleEntity, IRoleDataAccess>, IRoleRepository
    {
        #region 获取用户绑定的角色

        /// <summary>
        /// 获取用户绑定的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public List<Role> GetUserBindRole(long userId)
        {
            if (userId <= 0)
            {
                return new List<Role>(0);
            }
            var userRoleDal = this.Instance<IUserRoleDataAccess>();
            List<UserRoleEntity> userRoleBindList = userRoleDal.GetList(QueryFactory.Create<UserRoleQuery>(u => u.UserSysNo == userId));
            if (userRoleBindList.IsNullOrEmpty())
            {
                return new List<Role>(0);
            }
            IEnumerable<long> roleIds = userRoleBindList.Select(c => c.RoleSysNo).Distinct().ToList();
            IQuery roleQuery = QueryFactory.Create<RoleQuery>(r => roleIds.Contains(r.SysNo));
            return GetList(roleQuery);
        }

        #endregion
    }
}
