using EZNEW.Model;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 用户授权管理
    /// </summary>
    [Route("user-permission")]
    public class UserPermissionController : ApiBaseController
    {
        IUserPermissionAppService userPermissionAppService;

        public UserPermissionController(IUserPermissionAppService userPermissionAppService)
        {
            this.userPermissionAppService = userPermissionAppService;
        }

        #region 添加用户授权

        /// <summary>
        /// 添加用户授权
        /// </summary>
        /// <param name="userPermissions">用户授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public Result AddUserPermission(IEnumerable<UserPermission> userPermissions)
        {
            if (!userPermissions.IsNullOrEmpty())
            {
                foreach (var up in userPermissions)
                {
                    up.Disable = false;
                }
            }
            return userPermissionAppService.Modify(new ModifyUserPermissionParameter()
            {
                UserPermissions = userPermissions
            });
        }

        #endregion

        #region 删除用户授权

        /// <summary>
        /// 删除用户授权
        /// </summary>
        /// <param name="userPermissions">用户授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteUserPermission(IEnumerable<UserPermission> userPermissions)
        {
            if (!userPermissions.IsNullOrEmpty())
            {
                foreach (var up in userPermissions)
                {
                    up.Disable = true;
                }
            }
            return userPermissionAppService.Modify(new ModifyUserPermissionParameter()
            {
                UserPermissions = userPermissions
            });
        }

        #endregion

        #region 根据用户清除用户授权信息

        /// <summary>
        /// 清空用户所有授权
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [Route("clear-by-user")]
        public Result ClearByUser(IEnumerable<long> userIds)
        {
            return userPermissionAppService.ClearByUser(userIds);
        }

        #endregion

        #region 根据权限清除用户授权信息

        /// <summary>
        /// 清空权限授权的用户
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [Route("clear-by-permission")]
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return userPermissionAppService.ClearByPermission(permissionIds);
        }

        #endregion
    }
}
