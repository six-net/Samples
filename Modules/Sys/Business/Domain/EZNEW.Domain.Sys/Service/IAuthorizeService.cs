using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Framework.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public interface IAuthorizeService
    {
        #region 角色授权

        #region 修改角色授权信息

        /// <summary>
        /// 修改角色授权信息
        /// </summary>
        /// <param name="roleAuthorizes">角色授权信息</param>
        /// <returns></returns>
        Result ModifyRoleAuthorize(ModifyRoleAuthorize roleAuthorizes);

        #endregion

        #region 清除角色授权

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleSysNos">角色系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearRoleAuthorize(IEnumerable<long> roleSysNos);

        #endregion

        #endregion

        #region 用户授权

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="userAuthorizes">用户授权信息</param>
        /// <returns></returns>
        Result ModifyUserAuthorize(IEnumerable<UserAuthorize> userAuthorizes);

        #endregion

        #region 清除用户授权

        /// <summary>
        /// 清除用户授权
        /// </summary>
        /// <param name="userSysNos">用户系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearUserAuthorize(IEnumerable<long> userSysNos);

        #endregion

        #endregion

        #region 授权验证

        /// <summary>
        /// 用户授权验证
        /// </summary>
        /// <param name="auth">授权验证信息</param>
        /// <returns></returns>
        bool Authentication(Authentication auth);

        #endregion
    }
}
