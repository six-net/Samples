using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.Model;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEW.Paging;
using EZNEWApp.Module.Sys;
using Api.Console.Model.Response;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : ApiBaseController
    {
        readonly IRoleAppService roleAppService;

        public RoleController(IRoleAppService roleAppService)
        {
            this.roleAppService = roleAppService;
        }

        #region 获取角色配置

        /// <summary>
        /// 获取角色配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("config")]
        public GetRoleConfigurationResponse GetRoleConfiguration()
        {
            return new GetRoleConfigurationResponse()
            {
                StatusCollection = RoleStatus.Enable.GetEnumKeyValueCollection()
            };
        }

        #endregion

        #region 查询角色

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="roleFilter">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        public PagingInfo<Role> Query(RoleFilter roleFilter)
        {
            return roleAppService.GetRolePaging(roleFilter);
        }

        #endregion

        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Result<Role> SaveRole(Role role)
        {
            SaveRoleParameter saveRoleParameter = new SaveRoleParameter()
            {
                Role = role
            };
            return roleAppService.SaveRole(saveRoleParameter);
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="removeRoleParameter">删除角色参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result DeleteRole(RemoveRoleParameter removeRoleParameter)
        {
            return roleAppService.RemoveRole(removeRoleParameter);
        }

        #endregion

        #region 检查角色名称

        /// <summary>
        /// 检查角色名称
        /// </summary>
        /// <param name="existRoleNameParameter">参数信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("check-name")]
        public bool CheckRoleName(ExistRoleNameParameter existRoleNameParameter)
        {
            return !roleAppService.ExistRoleName(existRoleNameParameter);
        }

        #endregion
    }
}
