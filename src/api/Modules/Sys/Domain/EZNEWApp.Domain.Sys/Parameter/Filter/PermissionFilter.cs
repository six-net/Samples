using System;
using System.Collections.Generic;
using EZNEW.Development.Query;
using EZNEW.Development.Domain;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Module.Sys;
using EZNEW.Paging;

namespace EZNEWApp.Domain.Sys.Parameter.Filter
{
    /// <summary>
    /// 权限筛选信息
    /// </summary>
    public class PermissionFilter : PagingFilter, IDomainParameter
    {
        #region 筛选条件

        /// <summary>
        /// 编号
        /// </summary>
        public List<long> Ids { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public List<string> Codes { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType? Type { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PermissionStatus? Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        /// <summary>
        /// 权限分组
        /// </summary>
        public long? Group { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets the creation time
        /// </summary>
        public DateTimeOffset? CreationTime { get; set; }

        /// <summary>
        /// Gets the update time
        /// </summary>
        public DateTimeOffset? UpdateTime { get; set; }

        /// <summary>
        /// 名称/编码关键字
        /// </summary>
        public string NameCodeMateKey { get; set; }

        /// <summary>
        /// 菜单筛选条件
        /// </summary>
        public MenuFilter MenuFilter { get; set; }

        /// <summary>
        /// 操作功能筛选条件
        /// </summary>
        public OperationFilter OperationFilter { get; set; }

        /// <summary>
        /// 用户筛选信息
        /// </summary>
        public UserFilter UserFilter { get; set; }

        /// <summary>
        /// 角色筛选
        /// </summary>
        public RoleFilter RoleFilter { get; set; }

        #endregion

        #region 创建查询对象

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <returns></returns>
        public override IQuery CreateQuery()
        {
            var permissionQuery = base.CreateQuery() ?? QueryManager.Create<Permission>(this);

            #region 数据筛选

            if (!Ids.IsNullOrEmpty())
            {
                permissionQuery.In<Permission>(c => c.Id, Ids);
            }
            if (!Codes.IsNullOrEmpty())
            {
                permissionQuery.In<Permission>(c => c.Code, Codes);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                permissionQuery.Equal<Permission>(c => c.Name, Name);
            }
            if (!string.IsNullOrWhiteSpace(NameCodeMateKey))
            {
                permissionQuery.And<Permission>(CriterionOperator.Like, NameCodeMateKey, CriterionConnector.Or, a => a.Code, a => a.Name);
            }
            if (Type.HasValue)
            {
                permissionQuery.Equal<Permission>(c => c.Type, Type.Value);
            }
            if (Status.HasValue)
            {
                permissionQuery.Equal<Permission>(c => c.Status, Status.Value);
            }
            if (Sort.HasValue)
            {
                permissionQuery.Equal<Permission>(c => c.Sort, Sort.Value);
            }
            if (Group.HasValue)
            {
                permissionQuery.Equal<Permission>(c => c.Group, Group.Value);
            }
            if (!string.IsNullOrWhiteSpace(Remark))
            {
                permissionQuery.Equal<Permission>(c => c.Remark, Remark);
            }
            if (CreationTime.HasValue)
            {
                permissionQuery.Equal<Permission>(c => c.CreationTime, CreationTime.Value);
            }
            if (UpdateTime.HasValue)
            {
                permissionQuery.Equal<Permission>(c => c.UpdateTime, UpdateTime.Value);
            }

            #endregion

            #region 菜单筛选

            if (MenuFilter != null)
            {
                IQuery menuQuery = MenuFilter.CreateQuery();
                if (menuQuery != null)
                {
                    var menuPermissionQuery = QueryManager.Create<MenuPermission>();
                    menuPermissionQuery.EqualInnerJoin(menuQuery);
                    permissionQuery.EqualInnerJoin(menuPermissionQuery);
                }
            }

            #endregion

            #region 操作功能筛选

            if (OperationFilter != null)
            {
                IQuery operationQuery = OperationFilter.CreateQuery();
                if (operationQuery != null)
                {
                    var operationPermissionQuery = QueryManager.Create<OperationPermission>();
                    operationPermissionQuery.EqualInnerJoin(operationQuery);
                    permissionQuery.EqualInnerJoin(operationPermissionQuery);
                }
            }

            #endregion

            #region 用户授权筛选

            if (UserFilter != null)
            {
                IQuery userQuery = UserFilter.CreateQuery();
                if (userQuery != null)
                {
                    userQuery.AddQueryFields<User>(c => c.Id);

                    //用户或者用户绑定角色的授权权限
                    IQuery userOrRolePermissionQuery = QueryManager.Create();

                    #region 用户授权

                    IQuery userBindingPermissionQuery = QueryManager.Create<UserPermission>(c => c.Disable == false);
                    userBindingPermissionQuery.EqualInnerJoin(userQuery);
                    userBindingPermissionQuery.AddQueryFields<UserPermission>(c => c.PermissionId);
                    IQuery userPermissionQuery = QueryManager.Create<Permission>();
                    userPermissionQuery.In<Permission>(c => c.Id, userBindingPermissionQuery);
                    userOrRolePermissionQuery.And(userPermissionQuery);

                    #endregion

                    #region 角色授权

                    //用户绑定的角色
                    IQuery userRoleQuery = QueryManager.Create<UserRole>();
                    userRoleQuery.EqualInnerJoin(userQuery);
                    var roleQuery = QueryManager.Create<Role>(r => r.Status == RoleStatus.Enable);
                    roleQuery.EqualInnerJoin(userRoleQuery);

                    //角色授权
                    var roleBindingPermissionQuery = QueryManager.Create<RolePermission>();
                    roleBindingPermissionQuery.EqualInnerJoin(roleQuery);
                    roleBindingPermissionQuery.AddQueryFields<RolePermission>(rp => rp.PermissionId);
                    var rolePermissionQuery = QueryManager.Create<Permission>();
                    rolePermissionQuery.In<Permission>(c => c.Id, roleBindingPermissionQuery);
                    userOrRolePermissionQuery.Or(rolePermissionQuery);

                    #endregion

                    permissionQuery.And(userOrRolePermissionQuery);

                    #region 用户禁用授权

                    IQuery userDisablePermissionQuery = QueryManager.Create<UserPermission>(c => c.Disable == true);
                    userDisablePermissionQuery.EqualInnerJoin(userQuery);
                    userDisablePermissionQuery.AddQueryFields<UserPermission>(c => c.PermissionId);
                    permissionQuery.NotIn<Permission>(c => c.Id, userDisablePermissionQuery);

                    #endregion
                }
            }

            #endregion

            #region 角色授权筛选

            if (RoleFilter != null)
            {
                IQuery roleQuery = RoleFilter.CreateQuery();
                if (roleQuery != null)
                {
                    IQuery rolePermissionQuery = QueryManager.Create<RolePermission>();//角色&权限绑定
                    rolePermissionQuery.EqualInnerJoin(roleQuery);//角色&权限绑定->角色筛选
                    permissionQuery.EqualInnerJoin(rolePermissionQuery);//权限筛选->角色&权限绑定
                }
            }

            #endregion

            return permissionQuery;
        }

        #endregion
    }
}
