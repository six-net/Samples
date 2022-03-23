using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.BusinessContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 角色业务
    /// </summary>
    public class RoleBusiness : IRoleBusiness
    {
        static readonly IRoleService roleService = ContainerManager.Resolve<IRoleService>();
        static readonly IUserRoleService userRoleService = ContainerManager.Resolve<IUserRoleService>();

        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveRoleParameter">角色保存信息</param>
        /// <returns>返回角色保存执行结果</returns>
        public Result<Role> SaveRole(SaveRoleParameter saveRoleParameter)
        {
            if (saveRoleParameter is null)
            {
                throw new ArgumentNullException(nameof(saveRoleParameter));
            }

            using (var work = WorkManager.Create())
            {
                var saveResult = roleService.Save(saveRoleParameter.Role);
                if (!saveResult.Success)
                {
                    return saveResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? saveResult : Result<Role>.FailedResult("保存失败");
            }
        }

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns></returns>
        public Role GetRole(RoleFilter roleFilter)
        {
            return roleService.Get(roleFilter);
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="deleteRoleParameter">删除角色信息</param>
        /// <returns>返回删除角色执行结果</returns>
        public Result RemoveRole(RemoveRoleParameter deleteRoleParameter)
        {
            if (deleteRoleParameter is null)
            {
                throw new ArgumentNullException(nameof(deleteRoleParameter));
            }

            using (var work = WorkManager.Create())
            {
                var removeRoleResult = roleService.Remove(deleteRoleParameter.Ids);
                if (!removeRoleResult.Success)
                {
                    return removeRoleResult;
                }
                var commitResult = work.Commit();
                return commitResult.Success ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleFilterParameter">角色筛选信息</param>
        /// <returns>返回角色列表</returns>
        public List<Role> GetRoleList(RoleFilter roleFilterParameter)
        {
            return roleService.GetList(roleFilterParameter);
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="roleFilterParameter">角色筛选信息</param>
        /// <returns>返回角色分页</returns>
        public PagingInfo<Role> GetRolePaging(RoleFilter roleFilterParameter)
        {
            return roleService.GetPaging(roleFilterParameter);
        }

        #endregion

        #region 验证角色名称是否存在

        /// <summary>
        /// 验证角色名称是否存在
        /// </summary>
        /// <param name="existRoleNameParameter">角色名称验证信息</param>
        /// <returns>返回角色名称是否存在</returns>
        public bool ExistRoleName(ExistRoleNameParameter existRoleNameParameter)
        {
            if (existRoleNameParameter == null)
            {
                return false;
            }
            return roleService.ExistName(existRoleNameParameter);
        }

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回清除用户执行结果</returns>
        public Result ClearUser(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有任何要操作的角色");
            }
            using (var work = WorkManager.Create())
            {
                var clearResult = userRoleService.ClearByRole(roleIds);
                if (!clearResult.Success)
                {
                    return clearResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? clearResult : Result.FailedResult("修改失败");
            }
        }

        #endregion
    }
}
