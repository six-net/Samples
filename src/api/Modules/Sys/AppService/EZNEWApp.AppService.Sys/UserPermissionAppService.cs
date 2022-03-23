using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Model;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 用户授权业务逻辑
    /// </summary>
    public class UserPermissionAppService : IUserPermissionAppService
    {
        IUserPermissionBusiness userPermissionBusiness;

        public UserPermissionAppService(IUserPermissionBusiness userPermissionBusiness)
        {
            this.userPermissionBusiness = userPermissionBusiness;
        }

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermissionParameter">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyUserPermissionParameter modifyUserPermissionParameter)
        {
            return userPermissionBusiness.Modify(modifyUserPermissionParameter);
        }

        #endregion

        #region 根据用户清除用户授权信息

        /// <summary>
        /// 根据用户清除用户授权信息
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByUser(IEnumerable<long> userIds)
        {
            return userPermissionBusiness.ClearByUser(userIds);
        }

        #endregion

        #region 根据权限清除用户授权信息

        /// <summary>
        /// 根据权限清除用户授权信息
        /// </summary>
        /// <param name="permissionIds">权限系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return userPermissionBusiness.ClearByPermission(permissionIds);
        }

        #endregion
    }
}
