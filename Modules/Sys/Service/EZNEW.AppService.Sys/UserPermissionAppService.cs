using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.AppServiceContract.Sys;
using EZNEW.BusinessContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Response;

namespace EZNEW.AppService.Sys
{
    /// <summary>
    /// 用户授权应用服务
    /// </summary>
    public class UserPermissionAppService : IUserPermissionAppService
    {
        /// <summary>
        /// 用户授权业务
        /// </summary>
        readonly IUserPermissionBusiness userPermissionBusiness;

        public UserPermissionAppService(IUserPermissionBusiness userPermissionBusiness)
        {
            this.userPermissionBusiness = userPermissionBusiness;
        }

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermissionDto">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyUserPermission(ModifyUserPermissionDto modifyUserPermissionDto)
        {
            return userPermissionBusiness.ModifyUserPermission(modifyUserPermissionDto);
        }

        #endregion

        #region 清除用户授权

        /// <summary>
        /// 清除用户授权
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearUserPermission(IEnumerable<long> userIds)
        {
            return userPermissionBusiness.ClearUserPermission(userIds);
        }

        #endregion
    }
}
