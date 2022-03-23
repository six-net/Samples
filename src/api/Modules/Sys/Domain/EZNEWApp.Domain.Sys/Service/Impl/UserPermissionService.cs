using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEW.DependencyInjection;
using EZNEW.Development.Query;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEW.Model;
using EZNEW.Development.Domain.Repository;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 用户授权服务
    /// </summary>
    public class UserPermissionService : IUserPermissionService
    {
        readonly IRepository<RolePermission> rolePermissionRepository;
        readonly IRepository<UserPermission> userPermissionRepository;

        public UserPermissionService(IRepository<RolePermission> rolePermissionRepository,
            IRepository<UserPermission> userPermissionRepository)
        {
            this.rolePermissionRepository = rolePermissionRepository;
            this.userPermissionRepository = userPermissionRepository;
        }

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermission">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyUserPermissionParameter modifyUserPermission)
        {
            if (modifyUserPermission?.UserPermissions.IsNullOrEmpty() ?? true)
            {
                return Result.FailedResult("没有指定任何要修改的用户授权信息");
            }

            var userPermissions = modifyUserPermission.UserPermissions;

            #region 角色授权

            //用户绑定角色
            IEnumerable<long> userIds = userPermissions.Select(c => c.UserId).Distinct();
            IQuery userRoleQuery = QueryManager.Create<UserRole>(c => userIds.Contains(c.UserId));

            //角色授权
            IQuery rolePermissionQuery = QueryManager.Create<RolePermission>();
            rolePermissionQuery.EqualInnerJoin(userRoleQuery);

            IEnumerable<long> rolePermissionIds = rolePermissionRepository.GetList(rolePermissionQuery).Select(c => c.PermissionId);

            #endregion

            //移除当前存在的授权数据
            userPermissionRepository.Remove(userPermissions);
            var saveUserPermissions = new List<UserPermission>();
            //角色拥有但是用户显示禁用掉的授权
            var disablePermissions = userPermissions.Where(c => c.Disable && rolePermissionIds.Contains(c.PermissionId)).ToList();
            if (!disablePermissions.IsNullOrEmpty())
            {
                saveUserPermissions.AddRange(disablePermissions);
            }
            //用户单独授权的权限
            var enablePermissions = userPermissions.Where(c => !c.Disable && !rolePermissionIds.Contains(c.PermissionId)).ToList();
            if (!enablePermissions.IsNullOrEmpty())
            {
                saveUserPermissions.AddRange(enablePermissions);
            }
            if (!saveUserPermissions.IsNullOrEmpty())
            {
                userPermissionRepository.Save(saveUserPermissions);
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 根据用户清除用户授权

        /// <summary>
        /// 根据用户清除用户授权
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByUser(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何用户信息");
            }
            userPermissionRepository.RemoveByRelationData(userIds.Select(c => new User() { Id = c }));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 根据权限清除用户授权

        /// <summary>
        /// 根据权限清除用户授权
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            if (permissionIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何用户信息");
            }
            userPermissionRepository.RemoveByRelationData(permissionIds.Select(c => new Permission() { Id = c }));
            return Result.SuccessResult("修改成功");
        }

        #endregion
    }
}
