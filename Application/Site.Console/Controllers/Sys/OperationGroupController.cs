using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EZNEW.AppServiceContract.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.ViewModel.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys;
using EZNEW.Web.Mvc;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Response;
using EZNEW.Web.Security.Authorization;

namespace Site.Console.Controllers.Sys
{
    [AuthorizationOperationGroup(Name = "功能操作分组", Parent = "账户/授权")]
    public class OperationGroupController : WebBaseController
    {
        readonly IOperationGroupAppService operationGroupAppService;

        public OperationGroupController(IOperationGroupAppService operationGroupAppService)
        {
            this.operationGroupAppService = operationGroupAppService;
        }

        #region 操作功能组列表

        [AuthorizationOperation(Name = "权限分组列表页面")]
        public ActionResult OperationGroupList()
        {
            return View();
        }

        #endregion

        #region 编辑/添加操作功能组

        [AuthorizationOperation(Name = "添加/编辑操作功能分组")]
        public ActionResult EditOperationGroup(OperationGroupViewModel operationGroup)
        {
            if (IsPost)
            {
                SaveOperationGroupDto saveInfo = new SaveOperationGroupDto()
                {
                    OperationGroup = operationGroup.MapTo<OperationGroupDto>()
                };
                var saveResult = operationGroupAppService.SaveOperationGroup(saveInfo);
                var ajaxResult = AjaxResult.CopyFromResult(saveResult);
                ajaxResult.Data = saveResult.Data?.MapTo<OperationGroupViewModel>();
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else if (operationGroup.Id > 0)
            {
                OperationGroupFilterDto filter = new OperationGroupFilterDto()
                {
                    Ids = new List<long>()
                    {
                        operationGroup.Id
                    }
                };
                operationGroup = operationGroupAppService.GetOperationGroup(filter).MapTo<OperationGroupViewModel>();
            }
            return View(operationGroup);
        }

        #endregion

        #region 删除操作功能组

        [HttpPost]
        [AuthorizationOperation(Name = "删除操作功能分组")]
        public ActionResult RemoveOperationGroup(List<long> ids)
        {
            Result result = operationGroupAppService.RemoveOperationGroup(new RemoveOperationGroupDto()
            {
                Ids = ids
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 修改操作功能组排序

        [HttpPost]
        [AuthorizationOperation(Name = "修改操作功能分组排序")]
        public ActionResult ChangeOperationGroupSort(long id, int sort)
        {
            Result result = operationGroupAppService.ModifyOperationGroupSort(new ModifyOperationGroupSortDto()
            {
                Id = id,
                NewSort = sort
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 查询操作功能分组数据

        [HttpPost]
        [AuthorizationOperation(Name = "查询操作功能分组数据")]
        public IActionResult SearchOperationGroup(OperationGroupFilterDto filter)
        {
            var operationGroups = operationGroupAppService.GetOperationGroupList(filter).Select(c => c.MapTo<OperationGroupViewModel>()).OrderBy(r => r.Sort).ToList();
            return Json(operationGroups);
        }

        #endregion

        #region 检查操作分组名称是否可用

        [HttpPost]
        [AuthorizationOperation(Name = "检查操作分组名称是否可用")]
        public ActionResult CheckOperationGroupName(OperationGroupViewModel group)
        {
            ExistOperationGroupNameDto existInfo = new ExistOperationGroupNameDto()
            {
                Name = group.Name,
                ExcludeId = group.Id
            };
            bool allowUser = !operationGroupAppService.ExistOperationGroupName(existInfo);
            return Content(allowUser.ToString().ToLower());
        }

        #endregion
    }
}
