using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter;

namespace EZNEWApp.BusinessContract.Sys
{
    /// <summary>
    /// 功能操作&权限接口
    /// </summary>
    public interface IOperationPermissionBusiness
    {
        #region 修改权限&操作授权

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="modifyPermissionOperation">权限操作修改信息</param>
        /// <returns>返回执行结果</returns>
        Result Modify(ModifyOperationPermissionParameter modifyOperationPermissionParameter);

        #endregion

        #region 清除权限绑定的所有操作

        /// <summary>
        /// 清除权限绑定的所有操作
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByPermission(IEnumerable<long> permissionIds);

        #endregion

        #region 清除操作功能授权的所有权限

        /// <summary>
        /// 清除操作功能授权的所有权限
        /// </summary>
        /// <param name="operationIds">操作编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearByOperation(IEnumerable<long> operationIds);

        #endregion
    }
}
