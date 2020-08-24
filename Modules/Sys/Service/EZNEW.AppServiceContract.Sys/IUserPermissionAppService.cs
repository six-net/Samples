using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Response;

namespace EZNEW.AppServiceContract.Sys
{
    /// <summary>
    /// 用户授权应用服务
    /// </summary>
    public interface IUserPermissionAppService
    {
        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="modifyUserPermissionDto">用户授权修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyUserPermission(ModifyUserPermissionDto modifyUserPermissionDto);

        #endregion

        #region 清除用户授权

        /// <summary>
        /// 清除用户授权
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearUserPermission(IEnumerable<long> userIds);

        #endregion
    }
}
