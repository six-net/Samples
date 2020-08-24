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
    /// 角色授权应用服务
    /// </summary>
    public class RolePermissionAppService : IRolePermissionAppService
    {
        /// <summary>
        /// 角色授权业务
        /// </summary>
        readonly IRolePermissionBusiness rolePermissionBusiness;

        public RolePermissionAppService(IRolePermissionBusiness rolePermissionBusiness)
        {
            this.rolePermissionBusiness = rolePermissionBusiness;
        }

        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="modifyRolePermissionDto">角色授权修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyRolePermission(ModifyRolePermissionDto modifyRolePermissionDto)
        {
            return rolePermissionBusiness.ModifyRolePermission(modifyRolePermissionDto);
        }

        #endregion

        #region 清除角色授权

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearRolePermission(IEnumerable<long> roleIds)
        {
            return rolePermissionBusiness.ClearRolePermission(roleIds);
        }

        #endregion
    }
}
