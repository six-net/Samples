using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 用户权限筛选
    /// </summary>
    public class UserPermissionFilter : PermissionFilter
    {
        /// <summary>
        /// 用户筛选信息
        /// </summary>
        public UserFilter UserFilter { get; set; }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns>返回查询对象</returns>
        public override IQuery CreateQuery()
        {
            IQuery query = base.CreateQuery() ?? QueryManager.Create<PermissionEntity>(this);

            #region 用户授权筛选

            if (UserFilter != null)
            {
                IQuery userQuery = UserFilter.CreateQuery();
                if (userQuery != null)
                {
                    userQuery.AddQueryFields<UserEntity>(c => c.Id);

                    //用户或者用户绑定角色的授权权限
                    IQuery userOrRolePermissionQuery = QueryManager.Create();

                    #region 用户授权

                    IQuery userBindingPermissionQuery = QueryManager.Create<UserPermissionEntity>(c => c.Disable == false);
                    userBindingPermissionQuery.EqualInnerJoin(userQuery);
                    userBindingPermissionQuery.AddQueryFields<UserPermissionEntity>(c => c.PermissionId);
                    IQuery userPermissionQuery = QueryManager.Create<PermissionEntity>();
                    userPermissionQuery.In<PermissionEntity>(c => c.Id, userBindingPermissionQuery);
                    userOrRolePermissionQuery.And(userPermissionQuery);

                    #endregion

                    #region 角色授权

                    //用户绑定的角色
                    IQuery userRoleQuery = QueryManager.Create<UserRoleEntity>();
                    userRoleQuery.EqualInnerJoin(userQuery);
                    var roleQuery = QueryManager.Create<RoleEntity>(r => r.Status == RoleStatus.Enable);
                    roleQuery.EqualInnerJoin(userRoleQuery);
                    //包括所有上级角色
                    roleQuery.SetRecurve<RoleEntity>(r => r.Id, r => r.Parent, RecurveDirection.Up);

                    //角色授权
                    var roleBindingPermissionQuery = QueryManager.Create<RolePermissionEntity>();
                    roleBindingPermissionQuery.EqualInnerJoin(roleQuery);
                    roleBindingPermissionQuery.AddQueryFields<RolePermissionEntity>(rp => rp.PermissionId);
                    var rolePermissionQuery = QueryManager.Create<PermissionEntity>();
                    rolePermissionQuery.In<PermissionEntity>(c => c.Id, roleBindingPermissionQuery);
                    userOrRolePermissionQuery.Or(rolePermissionQuery);

                    #endregion

                    query.And(userOrRolePermissionQuery);

                    #region 用户禁用授权

                    IQuery userDisablePermissionQuery = QueryManager.Create<UserPermissionEntity>(c => c.Disable == true);
                    userDisablePermissionQuery.EqualInnerJoin(userQuery);
                    userDisablePermissionQuery.AddQueryFields<UserPermissionEntity>(c => c.PermissionId);
                    query.NotIn<PermissionEntity>(c => c.Id, userDisablePermissionQuery);

                    #endregion
                }
            }

            #endregion

            return query;
        }
    }
}
