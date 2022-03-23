using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEW.Model;

namespace EZNEWApp.Domain.Sys.Service
{
    /// <summary>
    /// 用户授权服务
    /// </summary>
    public interface IUserPermissionService
    {
        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermission">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        Result Modify(ModifyUserPermissionParameter modifyUserPermission);

        #endregion

        #region 根据用户清除用户授权

        /// <summary>
        /// 根据用户清除用户授权
        /// </summary>
        /// <param name="userIds">用户编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByUser(IEnumerable<long> userIds);

        #endregion

        #region 根据权限清除用户授权

        /// <summary>
        /// 根据权限清除用户授权
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByPermission(IEnumerable<long> permissionIds);

        #endregion
    }
}
