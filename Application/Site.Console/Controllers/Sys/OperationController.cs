using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Module.Sys;
using EZNEW.Response;
using EZNEW.ViewModel.Sys;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Site.Console.Controllers.Sys
{
    [AuthorizationGroup(Name = "功能操作", Parent = "账户/授权")]
    public class OperationController : WebBaseController
    {
        readonly IOperationAppService operationAppService;
        readonly IOperationGroupAppService operationGroupAppService;
        readonly IPermissionAppService permissionAppService;

        public OperationController(IOperationAppService operationAppService
            , IOperationGroupAppService operationGroupAppService
            , IPermissionAppService permissionAppService)
        {
            this.operationAppService = operationAppService;
            this.operationGroupAppService = operationGroupAppService;
            this.permissionAppService = permissionAppService;
        }

        #region 权限关联的操作功能列表

        [AuthorizationAction(Name = "查看权限绑定操作功能页面")]
        public ActionResult PermissionOperationList(long id)
        {
            ViewBag.PermissionId = id;
            return View();
        }

        #endregion

        #region 编辑/添加功能操作

        [AuthorizationAction(Name = "添加/编辑功能操作")]
        public ActionResult EditOperation(OperationViewModel operation, long groupId = 0)
        {
            if (IsPost)
            {
                SaveOperationDto saveInfo = new SaveOperationDto()
                {
                    Operation = operation.MapTo<OperationDto>()
                };
                Result<OperationDto> result = operationAppService.SaveOperation(saveInfo);
                var ajaxResult = AjaxResult.CopyFromResult(result);
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else
            {
                if (operation.Id > 0)
                {
                    OperationFilterDto filter = new OperationFilterDto()
                    {
                        Ids = new List<long>()
                    {
                        operation.Id
                    },
                        LoadGroup = true
                    };
                    operation = operationAppService.GetOperation(filter).MapTo<OperationViewModel>();
                    if (operation == null)
                    {
                        return Content("没有指定要操作的数据");
                    }
                }
                else if (groupId > 0)
                {
                    operation.Group = operationGroupAppService.GetOperationGroup(new OperationGroupFilterDto()
                    {
                        Ids = new List<long>() { groupId }
                    })?.MapTo<OperationGroupViewModel>();
                }
            }
            return View(operation);
        }

        #endregion

        #region 删除功能操作

        [AuthorizationAction(Name = "删除功能操作")]
        public ActionResult RemoveOperation(List<long> datas)
        {
            Result result = operationAppService.RemoveOperation(new RemoveOperationDto()
            {
                Ids = datas
            });
            return Json(result);
        }

        #endregion

        #region 查询操作功能数据

        /// <summary>
        /// 查询操作功能
        /// </summary>
        /// <param name="filter">操作功能筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询操作功能数据")]
        public ActionResult SearchOperation(OperationFilterDto filter)
        {
            var operationPaging = operationAppService.GetOperationPaging(filter).ConvertTo<OperationViewModel>();
            var result = new
            {
                operationPaging.TotalCount,
                Datas = operationPaging.ToList()
            };
            return Json(result);
        }

        /// <summary>
        /// 查询权限绑定的操作功能
        /// </summary>
        /// <param name="filter">权限绑定操作功能筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询权限绑定的操作功能数据")]
        public ActionResult SearchPermissionOperation(PermissionOperationFilterDto filter)
        {
            filter.LoadGroup = true;
            return SearchOperation(filter);
        }

        /// <summary>
        /// 查询用户授予的的所有操作功能
        /// </summary>
        /// <param name="filter">用户授权功能筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询授予用户的操作功能数据")]
        public ActionResult SearchUserOperation(UserOperationFilterDto filter)
        {
            List<OperationDto> operationList = null;
            if (filter?.UserFilter?.Ids.IsNullOrEmpty() ?? true)
            {
                operationList = new List<OperationDto>(0);
            }
            else
            {
                operationList = operationAppService.GetOperationList(filter) ?? new List<OperationDto>(0);
            }
            return Json(operationList);
        }

        /// <summary>
        /// 查询当前登录用户授予的所有操作功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询当前登录用户授予的所有操作功能数据")]
        public ActionResult SearchCurrentUserOperation()
        {
            var userId = User.Id;
            UserOperationFilterDto filterDto = new UserOperationFilterDto()
            {
                UserFilter = new UserFilterDto()
                {
                    Ids = new List<long>(1) { userId }
                }
            };
            return SearchUserOperation(filterDto);
        }

        #endregion

        #region 启用/关闭操作

        [HttpPost]
        [AuthorizationAction(Name = "修改操作功能状态")]
        public ActionResult ModifyOperationStatus(IEnumerable<long> ids, OperationStatus status)
        {
            var result = operationAppService.ModifyOperationStatus(new ModifyOperationStatusDto()
            {
                StatusInfos = ids.ToDictionary(c => c, c => status)
            });
            return Json(result);
        }

        #endregion

        #region 检查操作名称是否存在

        [HttpPost]
        [AuthorizationAction(Name = "检查操作功能名称是否存在")]
        public ActionResult CheckOperationName(OperationViewModel operation)
        {
            bool allowUse = true;
            if (string.IsNullOrWhiteSpace(operation.Name))
            {
                allowUse = false;
            }
            else
            {
                allowUse = !operationAppService.ExistOperationName(operation.Name, operation.Id);
            }
            return Content(allowUse ? "true" : "false");
        }

        #endregion

        #region 操作功能&权限

        #region 操作绑定权限

        [HttpPost]
        [AuthorizationAction(Name = "添加操作功能绑定的权限")]
        public ActionResult AddOperationPermission(long operationId, IEnumerable<long> permissionIds)
        {
            ModifyPermissionOperationDto modifyPermissionOperationDto = new ModifyPermissionOperationDto()
            {
                Bindings = permissionIds.Select(pid => new PermissionOperationDto()
                {
                    OperationId = operationId,
                    PermissionId = pid
                })
            };
            return Json(permissionAppService.ModifyPermissionOperation(modifyPermissionOperationDto));
        }

        #endregion

        #region 操作解绑权限

        [HttpPost]
        [AuthorizationAction(Name = "解绑操作功能绑定的权限")]
        public ActionResult RemoveOperationPermission(long operationId, IEnumerable<long> permissionIds)
        {
            ModifyPermissionOperationDto modifyPermissionOperationDto = new ModifyPermissionOperationDto()
            {
                Unbindings = permissionIds.Select(pid => new PermissionOperationDto()
                {
                    OperationId = operationId,
                    PermissionId = pid
                })
            };
            return Json(permissionAppService.ModifyPermissionOperation(modifyPermissionOperationDto));
        }

        #endregion

        #endregion

        #region 操作多选

        [AuthorizationAction(Name = "操作功能多选页面")]
        public ActionResult OperationMultiSelect()
        {
            return View();
        }

        #endregion

        #region 初始化默认权限操作

        [AuthorizationAction(Name = "初始化默认操作功能")]
        [HttpPost]
        public ActionResult ResolveDefaultAuthorizationOperation()
        {
            var operationGroups = AuthorizationManager.ResolveDefaultAuthorizations();
            if (!operationGroups.IsNullOrEmpty())
            {
                List<OperationGroupDto> operationGroupDtos = new List<OperationGroupDto>();
                List<OperationDto> operationDtos = new List<OperationDto>();
                foreach (var group in operationGroups)
                {
                    var nowGroup = new OperationGroupDto() { Name = group.Name };
                    operationGroupDtos.Add(nowGroup);
                    if (!group.ChildGroups.IsNullOrEmpty())
                    {
                        foreach (var childGroup in group.ChildGroups)
                        {
                            var nowChildGroup = new OperationGroupDto() { Name = childGroup.Name, Parent = nowGroup };
                            operationGroupDtos.Add(nowChildGroup);
                            if (!childGroup.Actions.IsNullOrEmpty())
                            {
                                foreach (var operation in childGroup.Actions)
                                {
                                    operationDtos.Add(new OperationDto()
                                    {
                                        Name = operation.Name,
                                        ActionCode = operation.Action,
                                        ControllerCode = operation.Controller,
                                        Group = nowChildGroup,
                                        Status = OperationStatus.Enable,
                                        AccessLevel = operation.Public ? OperationAccessLevel.Public : OperationAccessLevel.Authorized
                                    });
                                }
                            }
                        }
                    }
                    if (!group.Actions.IsNullOrEmpty())
                    {
                        foreach (var operation in group.Actions)
                        {
                            operationDtos.Add(new OperationDto()
                            {
                                Name = operation.Name,
                                ActionCode = operation.Action,
                                ControllerCode = operation.Controller,
                                Group = nowGroup,
                                Status = OperationStatus.Enable,
                                AccessLevel = operation.Public ? OperationAccessLevel.Public : OperationAccessLevel.Authorized
                            });
                        }
                    }
                }
                var initDto = new InitializeOperationDto()
                {
                    OperationGroups = operationGroupDtos,
                    Operations = operationDtos
                };
                operationAppService.Initialize(initDto);
            }
            return Json(Result.SuccessResult("解析成功"));
        }

        #endregion
    }
}
