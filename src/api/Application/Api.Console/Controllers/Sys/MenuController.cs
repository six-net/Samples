using Api.Console.Model.Response;
using EZNEW.Model;
using EZNEW.Paging;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.Module.Sys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : ApiBaseController
    {
        IMenuAppService menuAppService;

        public MenuController(IMenuAppService menuAppService)
        {
            this.menuAppService = menuAppService;
        }

        #region 查询菜单

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="filter">查询参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        public List<Menu> QueryMenu(MenuFilter filter)
        {
            return menuAppService.GetMenuList(filter);
        }

        #endregion

        #region 获取菜单配置

        /// <summary>
        /// 获取菜单配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("config")]
        public GetMenuConfigurationResponse GetMenuConfiguration()
        {
            return new GetMenuConfigurationResponse()
            {
                StatusCollection = MenuStatus.Enabled.GetEnumKeyValueCollection(),
                UsageCollection = MenuUsage.Menu.GetEnumKeyValueCollection()
            };
        }

        #endregion

        #region 保存菜单

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Result<Menu> SaveMenu(Menu menu)
        {
            return menuAppService.SaveMenu(new SaveMenuParameter()
            {
                Menu = menu
            });
        }

        #endregion

        #region 删除菜单

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="removeMenuParameter">菜单删除参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteMenu(RemoveMenuParameter removeMenuParameter)
        {
            return menuAppService.RemoveMenu(removeMenuParameter);
        }

        #endregion
    }
}
