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

namespace EZNEWApp.AppServiceContract.Sys
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuAppService
    {
        #region 保存菜单

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="saveMenuParameter">保存信息</param>
        /// <returns>执行结果</returns>
        Result<Menu> SaveMenu(SaveMenuParameter saveMenuParameter);

        #endregion

        #region 获取菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        Menu GetMenu(MenuFilter filter);

        #endregion

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        List<Menu> GetMenuList(MenuFilter filter);

        #endregion

        #region 获取菜单分页

        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        PagingInfo<Menu> GetMenuPaging(MenuFilter filter);

        #endregion

        #region 删除菜单

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="removeMenuParameter">删除信息</param>
        /// <returns>执行结果</returns>
        Result RemoveMenu(RemoveMenuParameter removeMenuParameter);

        #endregion

        #region 修改菜单&权限

        /// <summary>
        /// 修改菜单&权限
        /// </summary>
        /// <param name="modifyMenuPermissionParameter">修改参数</param>
        /// <returns>返回执行结果</returns>
        Result ModifyMenuPermission(ModifyMenuPermissionParameter modifyMenuPermissionParameter);

        #endregion

        #region 清除菜单权限

        /// <summary>
        /// 清除菜单权限
        /// </summary>
        /// <param name="menuIds">菜单编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearMenuPermission(IEnumerable<long> menuIds);

        #endregion
    }
}