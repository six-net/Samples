using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Model;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using Microsoft.AspNetCore.Mvc;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 操作授权管理
    /// </summary>
    [Route("operation-permission")]
    public class OperationPermissionController : ApiBaseController
    {
        IOperationPermissionAppService operationPermissionAppService;

        public OperationPermissionController(IOperationPermissionAppService operationPermissionAppService)
        {
            this.operationPermissionAppService = operationPermissionAppService;
        }

        #region 添加操作授权

        /// <summary>
        /// 添加操作授权
        /// </summary>
        /// <param name="operationPermissions">操作授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public Result AddOperationPermission(IEnumerable<OperationPermission> operationPermissions)
        {
            return operationPermissionAppService.Modify(new ModifyOperationPermissionParameter()
            {
                Bindings = operationPermissions
            });
        }

        #endregion

        #region 删除操作授权

        /// <summary>
        /// 删除操作授权
        /// </summary>
        /// <param name="operationPermissions">操作授权信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteOperationPermission(IEnumerable<OperationPermission> operationPermissions)
        {
            return operationPermissionAppService.Modify(new ModifyOperationPermissionParameter()
            {
                Unbindings = operationPermissions
            });
        }

        #endregion

        #region 根据操作清空操作授权

        /// <summary>
        /// 清空操作的授权
        /// </summary>
        /// <param name="operationIds">操作编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("clear-by-operation")]
        public Result ClearByOperation(IEnumerable<long> operationIds)
        {
            return operationPermissionAppService.ClearByOperation(operationIds);
        }

        #endregion

        #region 根据权限清空操作授权

        /// <summary>
        /// 清空权限授权的操作
        /// </summary>
        /// <param name="permissionIds">权限编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("clear-by-permission")]
        public Result ClearByPermission(IEnumerable<long> permissionIds)
        {
            return operationPermissionAppService.ClearByPermission(permissionIds);
        }

        #endregion
    }
}
