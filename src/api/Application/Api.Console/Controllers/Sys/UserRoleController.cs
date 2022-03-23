using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EZNEW.Model;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.AppServiceContract.Sys;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 用户角色管理
    /// </summary>
    [Route("user-role")]
    public class UserRoleController : ApiBaseController
    {
        readonly IUserRoleAppService userRoleAppService;

        public UserRoleController(IUserRoleAppService userRoleAppService)
        {
            this.userRoleAppService = userRoleAppService;
        }

        #region 删除用户角色

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="userRoles">用户角色</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public Result DeleteUserRole(List<UserRole> userRoles)
        {
            if (userRoles.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定用户角色数据");
            }
            return userRoleAppService.Modify(new ModifyUserRoleParameter()
            {
                Unbindings = userRoles
            });
        }

        #endregion

        #region 添加用户角色

        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="userRoles">用户角色</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public Result AddUserRole(List<UserRole> userRoles)
        {
            if (userRoles.IsNullOrEmpty())
            {
                return Result.FailedResult("未指定用户角色数据");
            }
            return userRoleAppService.Modify(new ModifyUserRoleParameter()
            {
                Bindings = userRoles
            });
        }

        #endregion

        #region 清除用户绑定的所有角色

        /// <summary>
        /// 清除用户绑定的所有角色
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns></returns>
        [Route("clear-by-user")]
        [HttpPost]
        public Result ClearByUser(List<long> userIds)
        {
            return userRoleAppService.ClearByUser(userIds);
        }

        #endregion

        #region 清除角色绑定的所有用户

        /// <summary>
        /// 清除角色绑定的所有用户
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns></returns>
        [Route("clear-by-role")]
        [HttpPost]
        public Result ClearByRole(List<long> roleIds)
        {
            return userRoleAppService.ClearByRole(roleIds);
        }

        #endregion
    }
}
