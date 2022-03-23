using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Development.Query;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Domain.Sys.Service
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public interface IMenuService
    {
        #region 保存菜单

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns></returns>
        Result<Menu> Save(Menu menu);

        #endregion

        #region 获取菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Menu Get(long id);

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuFilter">菜单筛选信息</param>
        /// <returns></returns>
        Menu Get(MenuFilter menuFilter);

        #endregion

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="menuIds">菜单编号</param>
        /// <returns></returns>
        List<Menu> GetList(IEnumerable<long> menuIds);

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="menuFilter">菜单筛选信息</param>
        /// <returns></returns>
        List<Menu> GetList(MenuFilter menuFilter);

        #endregion

        #region 获取菜单分页

        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="menuFilter">菜单筛选信息</param>
        /// <returns></returns>
        PagingInfo<Menu> GetPaging(MenuFilter menuFilter);

        #endregion

        #region 删除菜单

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="removeMenuParameter">删除信息</param>
        /// <returns>执行结果</returns>
        Result Remove(RemoveMenuParameter removeMenuParameter);

        #endregion
    }
}