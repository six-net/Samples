using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 角色授予权限筛选
    /// </summary>
    public class RolePermissionFilter : PermissionFilter
    {
        /// <summary>
        /// 角色筛选
        /// </summary>
        public RoleFilter RoleFilter { get; set; }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            //权限筛选
            IQuery permissionQuery = base.CreateQuery() ?? QueryManager.Create<PermissionEntity>(this);

            #region 角色筛选

            if (RoleFilter != null)
            {
                IQuery roleQuery = RoleFilter.CreateQuery();
                if (roleQuery != null)
                {
                    IQuery roleBindAuthQuery = QueryManager.Create<RolePermissionEntity>();//角色&权限绑定
                    roleBindAuthQuery.EqualInnerJoin(roleQuery);//角色&权限绑定->角色筛选
                    permissionQuery.EqualInnerJoin(roleBindAuthQuery);//权限筛选->角色&权限绑定
                }
            }

            #endregion

            return permissionQuery;
        }
    }
}
