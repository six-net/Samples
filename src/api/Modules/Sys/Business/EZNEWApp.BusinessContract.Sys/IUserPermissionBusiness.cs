using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;

namespace EZNEWApp.BusinessContract.Sys
{
    /// <summary>
    /// 用户授权业务逻辑
    /// </summary>
    public interface IUserPermissionBusiness
    {
        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermissionParameter">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        Result Modify(ModifyUserPermissionParameter modifyUserPermissionParameter);

        #endregion

        #region 根据用户清除用户授权信息

        /// <summary>
        /// 根据用户清除用户授权信息
        /// </summary>
        /// <param name="permissionIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByUser(IEnumerable<long> permissionIds);

        #endregion

        #region 根据权限清除用户授权信息

        /// <summary>
        /// 根据权限清除用户授权信息
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByPermission(IEnumerable<long> permissionIds);

        #endregion
    }
}
