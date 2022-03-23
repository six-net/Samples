using EZNEW.Development.Domain.Repository;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 菜单&权限服务实现
    /// </summary>
    public class MenuPermissionService : IMenuPermissionService
    {
        readonly IRepository<MenuPermission> menuPermissionRepository;

        public MenuPermissionService(IRepository<MenuPermission> menuPermissionRepository)
        {
            this.menuPermissionRepository = menuPermissionRepository;
        }

        #region 清除菜单的所有权限

        /// <summary>
        /// 清除菜单的所有权限
        /// </summary>
        /// <param name="menuIds">菜单系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearByMenu(IEnumerable<long> menuIds)
        {
            if (menuIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何菜单信息");
            }
            menuPermissionRepository.RemoveByRelationData(menuIds.Select(c => new Menu() { Id = c }));
            return Result.SuccessResult("清除成功");
        }

        #endregion

        #region 清除权限的所有菜单

        /// <summary>
        /// 清除权限的所有菜单
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            if (permissionIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何权限信息");
            }
            menuPermissionRepository.RemoveByRelationData(permissionIds.Select(c => new Permission() { Id = c }));
            return Result.SuccessResult("清除成功");
        }

        #endregion

        #region 修改菜单&权限

        /// <summary>
        /// 修改菜单&权限
        /// </summary>
        /// <param name="modifyMenuPermissionParameter">菜单&权限修改参数</param>
        /// <returns>返回操作结果</returns>
        public Result Modify(ModifyMenuPermissionParameter modifyMenuPermissionParameter)
        {
            if (modifyMenuPermissionParameter == null || (modifyMenuPermissionParameter.Bindings.IsNullOrEmpty() && modifyMenuPermissionParameter.Unbindings.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何需要修改的菜单&权限信息");
            }
            //绑定
            if (!modifyMenuPermissionParameter.Bindings.IsNullOrEmpty())
            {
                menuPermissionRepository.Save(modifyMenuPermissionParameter.Bindings);
            }
            //解绑
            if (!modifyMenuPermissionParameter.Unbindings.IsNullOrEmpty())
            {
                menuPermissionRepository.Remove(modifyMenuPermissionParameter.Unbindings);
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion
    }
}
