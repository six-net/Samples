using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Response;

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
        /// <param name="userRoleBindings">用户角色绑定信息</param>
        /// <returns>返回操作结果</returns>
        Result Bind(params UserRole[] userRoleBindings);

        #endregion

        #region 用户角色解绑

        /// <summary>
        /// 用户角色解绑
        /// </summary>
        /// <param name="userRoleUnbindings">用户角色绑定信息</param>
        /// <returns>返回操作结果</returns>
        Result Unbind(params UserRole[] userRoleUnbindings);

        #endregion

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
        /// <param name="modifyUserRole">用户&角色修绑定关系修改信息</param>
        /// <returns>返回操作结果</returns>
        Result Modify(ModifyUserRole modifyUserRole);

        #endregion
    }
}
