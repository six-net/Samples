using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Development.UnitOfWork;
using EZNEW.Model;
using EZNEW.Development.Query;
using EZNEW.Paging;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Service;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.Business.Sys
{
    /// <summary>
    /// 菜单业务
    /// </summary>
    public class MenuBusiness : IMenuBusiness
    {
        IMenuService menuService;
        IMenuPermissionService menuPermissionService;

        public MenuBusiness(IMenuService menuService, IMenuPermissionService menuPermissionService)
        {
            this.menuService = menuService;
            this.menuPermissionService = menuPermissionService;
        }

        #region 保存菜单

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="saveMenuParameter">保存信息</param>
        /// <returns>执行结果</returns>
        public Result<Menu> SaveMenu(SaveMenuParameter saveMenuParameter)
        {
            if (saveMenuParameter == null)
            {
                throw new ArgumentNullException(nameof(saveMenuParameter));
            }
            using (var work = WorkManager.Create())
            {
                var saveResult = menuService.Save(saveMenuParameter.Menu);
                if (!saveResult.Success)
                {
                    return saveResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? saveResult : Result<Menu>.SuccessResult("保存失败");
            }
        }

        #endregion

        #region 获取菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public Menu GetMenu(MenuFilter filter)
        {
            return menuService.Get(filter);
        }

        #endregion

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public List<Menu> GetMenuList(MenuFilter filter)
        {
            return menuService.GetList(filter);
        }

        #endregion

        #region 获取菜单分页

        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public PagingInfo<Menu> GetMenuPaging(MenuFilter filter)
        {
            return menuService.GetPaging(filter);
        }

        #endregion

        #region 删除菜单

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="removeMenuParameter">删除信息</param>
        /// <returns>执行结果</returns>
        public Result RemoveMenu(RemoveMenuParameter removeMenuParameter)
        {
            if (removeMenuParameter is null)
            {
                throw new ArgumentNullException(nameof(removeMenuParameter));
            }

            using (var work = WorkManager.Create())
            {
                var removeResult = menuService.Remove(removeMenuParameter);
                if (!removeResult.Success)
                {
                    return removeResult;
                }
                var commitResult = work.Commit();
                return commitResult.EmptyOrSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 修改菜单&权限

        /// <summary>
        /// 修改菜单&权限
        /// </summary>
        /// <param name="modifyMenuPermissionParameter">修改参数</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyMenuPermission(ModifyMenuPermissionParameter modifyMenuPermissionParameter)
        {
            using (var businessWork = WorkManager.Create())
            {
                var modifyResult = menuPermissionService.Modify(modifyMenuPermissionParameter);
                if (modifyResult?.Success ?? false)
                {
                    var commitResult = businessWork.Commit();
                    if (!(commitResult?.EmptyOrSuccess ?? false))
                    {
                        modifyResult = null;
                    }
                }
                return modifyResult ?? Result.FailedResult("修改失败");
            }
        }

        #endregion

        #region 清除菜单权限

        /// <summary>
        /// 清除菜单权限
        /// </summary>
        /// <param name="menuIds">菜单编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearMenuPermission(IEnumerable<long> menuIds)
        {
            using (var businessWork = WorkManager.Create())
            {
                var clearResult = menuPermissionService.ClearByMenu(menuIds);
                if (clearResult?.Success ?? false)
                {
                    var commitResult = businessWork.Commit();
                    if (!(commitResult?.EmptyOrSuccess ?? false))
                    {
                        clearResult = null;
                    }
                }
                return clearResult ?? Result.FailedResult("清除失败");
            }
        }

        #endregion
    }
}