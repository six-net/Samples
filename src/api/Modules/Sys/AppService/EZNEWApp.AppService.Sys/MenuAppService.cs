using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class MenuAppService : IMenuAppService
    {
        IMenuBusiness menuBusiness = null;

        public MenuAppService(IMenuBusiness menuBusiness)
        {
            this.menuBusiness = menuBusiness;
        }

        #region 保存菜单

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="saveMenuParameter">保存信息</param>
        /// <returns>执行结果</returns>
        public Result<Menu> SaveMenu(SaveMenuParameter saveMenuParameter)
        {
            return menuBusiness.SaveMenu(saveMenuParameter);
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
            return menuBusiness.GetMenu(filter);
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
            return menuBusiness.GetMenuList(filter);
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
            return menuBusiness.GetMenuPaging(filter);
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
            return menuBusiness.RemoveMenu(removeMenuParameter);
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
            return menuBusiness.ModifyMenuPermission(modifyMenuPermissionParameter);
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
            return menuBusiness.ClearMenuPermission(menuIds);
        }

        #endregion
    }
}