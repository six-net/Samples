using System.Collections.Generic;
using System.Transactions;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 权限&授权操作绑定操作服务
    /// </summary>
    public interface IPermissionOperationService
    {
        #region 修改权限&操作授权

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="modifyPermissionOperation">权限操作修改信息</param>
        /// <returns>返回执行结果</returns>
        Result Modify(ModifyPermissionOperationParameter modifyPermissionOperation);

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
