using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.AppServiceContract.Sys
{
    /// <summary>
    /// 用户&角色应用程序服务
    /// </summary>
    public interface IUserRoleAppService
    {
        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearByRole(IEnumerable<long> roleIds);

        #endregion

        #region 清除用户绑定的所有角色

        /// <summary>
        /// 清除用户绑定的所有角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearByUser(IEnumerable<long> userIds);

        #endregion

        #region 修改用户&角色绑定关系

        /// <summary>
        /// 修改用户&角色绑定关系
        /// </summary>
        /// <param name="modifyUserRoleParameter">用户&角色修绑定关系修改信息</param>
        /// <returns>返回操作结果</returns>
        Result Modify(ModifyUserRoleParameter modifyUserRoleParameter);

        #endregion
    }
}
