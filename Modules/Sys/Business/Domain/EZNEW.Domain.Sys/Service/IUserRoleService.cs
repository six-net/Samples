using EZNEW.Domain.Sys.Model;
using EZNEW.Response;
using System;
using System.Collections.Generic;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 用户角色服务
    /// </summary>
    public interface IUserRoleService
    {
        #region 用户和角色的绑定

        /// <summary>
        /// 绑定用户角色
        /// </summary>
        /// <param name="userRoleBinds">用户角色绑定信息</param>
        /// <returns></returns>
        Result BindUserAndRole(params Tuple<User, Role>[] userRoleBinds);

        #endregion

        #region 用户角色解绑

        /// <summary>
        /// 用户角色解绑
        /// </summary>
        /// <param name="userRoleBinds">用户角色绑定信息</param>
        /// <returns></returns>
        Result UnBindUserAndRole(params Tuple<User, Role>[] userRoleBinds);

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleSysNos">角色系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearRoleUser(IEnumerable<long> roleSysNos);

        #endregion

        #region 清除用户绑定的所有角色

        /// <summary>
        /// 清除用户绑定的所有角色
        /// </summary>
        /// <param name="userSysNos">用户系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearUserRole(IEnumerable<long> userSysNos);

        #endregion
    }
}
