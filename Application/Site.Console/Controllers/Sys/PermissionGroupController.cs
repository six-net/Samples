using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Response;
using EZNEW.ViewModel.Sys;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.Console.Controllers.Sys
{
    [AuthorizationOperationGroup(Name = "权限分组", Parent = "账户/授权")]
    public class PermissionGroupController : WebBaseController
    {
        readonly IPermissionGroupAppService permissionGroupAppService;

        public PermissionGroupController(IPermissionGroupAppService permissionGroupAppService)
        {
            this.permissionGroupAppService = permissionGroupAppService;
        }

        #region 权限分组列表

        [AuthorizationOperation(Name = "权限分组列表页面")]
        public ActionResult PermissionGroupList()
        {
            return View();
        }

        #endregion

        #region 编辑/添加权限分组

        [AuthorizationOperation(Name = "添加/编辑权限分组")]
        public ActionResult EditPermissionGroup(PermissionGroupViewModel permissionGroup)
        {
            if (IsPost)
            {
                Result<PermissionGroupDto> saveResult = permissionGroupAppService.SavePermissionGroup(new SavePermissionGroupDto()
                {
                    PermissionGroup = permissionGroup.MapTo<PermissionGroupDto>()
                });
                var ajaxResult = AjaxResult.CopyFromResult(saveResult);
                ajaxResult.Data = saveResult.Data?.MapTo<PermissionGroupViewModel>();
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else if (permissionGroup.Id > 0)
            {
                PermissionGroupFilterDto filter = new PermissionGroupFilterDto()
                {
                    Ids = new List<long>()
                    {
                        permissionGroup.Id
                    }
                };
                permissionGroup = permissionGroupAppService.GetPermissionGroup(filter).MapTo<PermissionGroupViewModel>();
            }
            return View(permissionGroup);
        }

        #endregion

        #region 删除权限分组

        [HttpPost]
        [AuthorizationOperation(Name = "删除权限分组")]
        public ActionResult RemovePermissionGroup(List<long> ids)
        {
            Result result = permissionGroupAppService.RemovePermissionGroup(new RemovePermissionGroupDto()
            {
                Ids = ids
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 修改权限分组排序

        [HttpPost]
        [AuthorizationOperation(Name = "修改权限分组排序")]
        public ActionResult ChangePermissionGroupSort(long id, int sort)
        {
            Result result = null;
            result = permissionGroupAppService.ModifyPermissionGroupSort(new ModifyPermissionGroupSortDto()
            {
                Id = id,
                NewSort = sort
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 查询权限分组数据

        [HttpPost]
        [AuthorizationOperation(Name = "查看权限分组数据")]
        public IActionResult SearchPermissionGroup(PermissionGroupFilterDto filter)
        {
            var permissionGroups = permissionGroupAppService.GetPermissionGroupList(filter).Select(c => c.MapTo<PermissionGroupViewModel>()).OrderBy(r => r.Sort).ToList();
            return Json(permissionGroups);
        }

        #endregion

        #region 验证权限分组名称是否存在

        [HttpPost]
        [AuthorizationOperation(Name = "验证权限分组名称是否存在")]
        public ActionResult CheckPermissionGroupName(PermissionGroupViewModel group)
        {
            ExistPermissionGroupNameDto existInfo = new ExistPermissionGroupNameDto()
            {
                Name = group.Name,
                ExcludeId = group.Id
            };
            bool allowUse = !permissionGroupAppService.ExistPermissionGroupName(existInfo);
            return Content(allowUse.ToString().ToLower());
        }

        #endregion
    }
}
