using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EZNEW.Model;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;

namespace Api.Console.Controllers.Sys
{
    /// <summary>
    /// 权限分组管理
    /// </summary>
    public class PermissionGroupController : ApiBaseController
    {
        readonly IPermissionGroupAppService permissionGroupAppService;

        public PermissionGroupController(IPermissionGroupAppService permissionGroupAppService)
        {
            this.permissionGroupAppService = permissionGroupAppService;
        }

        #region 查询权限分组数据

        /// <summary>
        /// 查询权限分组
        /// </summary>
        /// <param name="filter">查询参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        public List<PermissionGroup> QueryPermissionGroup(PermissionGroupFilter filter)
        {
            return permissionGroupAppService.GetPermissionGroupList(filter).Select(c => c).OrderBy(r => r.Sort).ToList();
        }

        #endregion

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="permissionGroup">权限分组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        public Result<PermissionGroup> SavePermissionGroup(PermissionGroup permissionGroup)
        {
            return permissionGroupAppService.SavePermissionGroup(new SavePermissionGroupParameter()
            {
                PermissionGroup = permissionGroup
            });
        }

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="removePermissionGroupParameter">删除权限分组参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public Result RemovePermissionGroup(RemovePermissionGroupParameter removePermissionGroupParameter)
        {
            return permissionGroupAppService.RemovePermissionGroup(removePermissionGroupParameter);
        }

        #endregion

        #region 修改权限分组排序

        /// <summary>
        /// 修改权限分组排序
        /// </summary>
        /// <param name="id">权限编号</param>
        /// <param name="sort">权限排序</param>
        /// <returns></returns>
        [HttpPost]
        public Result ChangePermissionGroupSort(long id, int sort)
        {
            return permissionGroupAppService.ModifyPermissionGroupSort(new ModifyPermissionGroupSortParameter()
            {
                Id = id,
                NewSort = sort
            });
        }

        #endregion

        #region 检查权限分组名称

        /// <summary>
        /// 检查权限分组名称
        /// </summary>
        /// <param name="existPermissionGroupNameParameter">参数信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("check-name")]
        public bool CheckPermissionGroupName(ExistPermissionGroupNameParameter existPermissionGroupNameParameter)
        {
            return !permissionGroupAppService.ExistPermissionGroupName(existPermissionGroupNameParameter);
        }

        #endregion
    }
}
