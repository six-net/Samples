using EZNEW.Model;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.BusinessContract.Sys;
using EZNEWApp.Domain.Sys.Parameter;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.AppService.Sys
{
    public class OperationPermissionAppService : IOperationPermissionAppService
    {
        IOperationPermissionBusiness operationPermissionBusiness;

        public OperationPermissionAppService(IOperationPermissionBusiness operationPermissionBusiness)
        {
            this.operationPermissionBusiness = operationPermissionBusiness;
        }

        #region 修改权限&操作授权

        /// <summary>
        /// 修改权限&操作绑定
        /// </summary>
        /// <param name="modifyPermissionOperation">权限操作修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result Modify(ModifyOperationPermissionParameter modifyOperationPermissionParameter)
        {
            return operationPermissionBusiness.Modify(modifyOperationPermissionParameter);
        }

        #endregion

        #region 清除权限绑定的所有操作

        /// <summary>
        /// 清除权限绑定的所有操作
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return operationPermissionBusiness.ClearByPermission(permissionIds);
        }

        #endregion

        #region 清除操作功能授权的所有权限

        /// <summary>
        /// 清除操作功能授权的所有权限
        /// </summary>
        /// <param name="operationIds">操作编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearByOperation(IEnumerable<long> operationIds)
        {
            return operationPermissionBusiness.ClearByOperation(operationIds);
        }

        #endregion
    }
}
