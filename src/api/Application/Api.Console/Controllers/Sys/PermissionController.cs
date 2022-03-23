using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EZNEW.Paging;
using EZNEW.Web.Mvc;
using EZNEW.Code;
using EZNEW.Model;
using EZNEWApp.Module.Sys;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using Api.Console.Model.Response;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class PermissionController : ApiBaseController
    {
        readonly IPermissionAppService permissionAppService;

        public PermissionController(IPermissionAppService permissionAppService)
        {
            this.permissionAppService = permissionAppService;
        }

        #region 查询权限数据

        /// <summary>
        /// 查询权限数据
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        public PagingInfo<Permission> QueryPermission(PermissionFilter filter)
        {
            return permissionAppService.GetPermissionPaging(filter);
        }

        #endregion

        #region 获取权限配置

        /// <summary>
        /// 获取权限配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("config")]
        public GetPermissionConfigurationResponse GetPermissionConfiguration()
        {
            return new GetPermissionConfigurationResponse()
            {
                StatusCollection = PermissionStatus.Enable.GetEnumKeyValueCollection()
            };
        }

        #endregion

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="perission">权限信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Result<Permission> SavePermission(Permission perission)
        {
            SavePermissionParameter savePermissionParameter = new SavePermissionParameter()
            {
                Permission = perission
            };
            return permissionAppService.SavePermission(savePermissionParameter);
        }

        #endregion

        #region 删除权限

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="removePermissionParameter">删除权限参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result RemovePermission(RemovePermissionParameter removePermissionParameter)
        {
            return permissionAppService.RemovePermission(removePermissionParameter);
        }

        #endregion
    }
}
