using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Development.Query;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEW.DependencyInjection;
using EZNEW.Development.Domain.Repository;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class MenuService : IMenuService
    {
        readonly IRepository<Menu> menuRepository;

        public MenuService(IRepository<Menu> menuRepository)
        {
            this.menuRepository = menuRepository;
        }

        #region 保存菜单

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns></returns>
        public Result<Menu> Save(Menu menu)
        {
            return menu?.Save() ?? Result<Menu>.FailedResult("菜单保存失败");
        }

        #endregion

        #region 获取菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回菜单</returns>
        Menu GetMenu(IQuery query)
        {
            return menuRepository.Get(query);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuId">菜单编号</param>
        /// <returns>返回菜单</returns>
        public Menu Get(long menuId)
        {
            if (menuId < 1)
            {
                return null;
            }
            IQuery query = QueryManager.Create<Menu>(c => c.Id == menuId);
            return GetMenu(query);
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="menuFilter">菜单筛选信息</param>
        /// <returns>返回菜单</returns>
        public Menu Get(MenuFilter menuFilter)
        {
            return GetMenu(menuFilter?.CreateQuery());
        }

        #endregion

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="query">筛选条件</param>
        /// <returns>返回菜单列表</returns>
        List<Menu> GetMenuList(IQuery query)
        {
            var menuList = menuRepository.GetList(query);
            menuList = LoadOtherData(menuList, query);
            return menuList;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="menuIds">菜单编号</param>
        /// <returns>返回菜单列表</returns>
        public List<Menu> GetList(IEnumerable<long> menuIds)
        {
            if (menuIds.IsNullOrEmpty())
            {
                return new List<Menu>(0);
            }
            IQuery query = QueryManager.Create<Menu>(c => menuIds.Contains(c.Id));
            return GetMenuList(query);
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="menuFilter">菜单筛选条件</param>
        /// <returns>返回菜单列表</returns>
        public List<Menu> GetList(MenuFilter menuFilter)
        {
            return GetMenuList(menuFilter?.CreateQuery());
        }

        #endregion

        #region 获取菜单分页

        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="query">筛选信息</param>
        /// <returns>返回菜单分页</returns>
        PagingInfo<Menu> GetPaging(IQuery query)
        {
            var menuPaging = menuRepository.GetPaging(query);
            var menuList = LoadOtherData(menuPaging?.Items, query);
            return Pager.Create(menuPaging.Page, menuPaging.PageSize, menuPaging.TotalCount, menuList);
        }

        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="menuFilter">菜单筛选信息</param>
        /// <returns>返回菜单分页</returns>
        public PagingInfo<Menu> GetPaging(MenuFilter menuFilter)
        {
            return GetPaging(menuFilter?.CreateQuery());
        }

        #endregion

        #region 加载其它数据

        /// <summary>
        /// 加载其它数据
        /// </summary>
        /// <param name="menus">菜单数据</param>
        /// <param name="query">筛选条件</param>
        /// <returns></returns>
        List<Menu> LoadOtherData(IEnumerable<Menu> menus, IQuery query)
        {
            if (menus.IsNullOrEmpty())
            {
                return new List<Menu>(0);
            }
            if (query == null)
            {
                return menus.ToList();
            }

            foreach (var menu in menus)
            {
                if (menu == null)
                {
                    continue;
                }
            }

            return menus.ToList();
        }

        #endregion

        #region 删除菜单

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="removeMenuParameter">删除信息</param>
        /// <returns>执行结果</returns>
        public Result Remove(RemoveMenuParameter removeMenuParameter)
        {
            if (removeMenuParameter?.Ids.IsNullOrEmpty() ?? true)
            {
                throw new Exception("没有指定任何要删除的菜单");
            }
            IQuery removeQuery = QueryManager.Create<Menu>(a => removeMenuParameter.Ids.Contains(a.Id));
            removeQuery.SetRecurve<Menu>(r => r.Id, r => r.Parent);
            menuRepository.Remove(removeQuery);
            return Result.SuccessResult("删除成功");
        }

        #endregion
    }
}