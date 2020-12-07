using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.ViewModel.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys;
using EZNEW.Web.Mvc;
using EZNEW.Code;
using EZNEW.Response;
using EZNEW.Module.Sys;
using EZNEW.Web.Security.Authorization;

namespace Site.Console.Controllers.Sys
{
    [AuthorizationGroup(Name = "权限", Parent = "账户/授权")]
    public class PermissionController : WebBaseController
    {
        readonly IPermissionAppService permissionAppService;
        readonly IPermissionGroupAppService permissionGroupAppService;

        public PermissionController(IPermissionAppService permissionAppService
            , IPermissionGroupAppService permissionGroupAppService)
        {
            this.permissionAppService = permissionAppService;
            this.permissionGroupAppService = permissionGroupAppService;
        }

        #region 操作功能绑定权限列表

        [AuthorizationAction(Name = "操作功能绑定权限列表页面")]
        public ActionResult OperationPermissionList(long id)
        {
            ViewBag.OperationId = id;
            return View();
        }

        #endregion

        #region 查询权限数据

        /// <summary>
        /// 查询权限数据
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询权限数据")]
        public ActionResult SearchPermission(PermissionFilterDto filter)
        {
            IPaging<PermissionViewModel> permissionPager = permissionAppService.GetPermissionPaging(filter).ConvertTo<PermissionViewModel>();
            object objResult = new
            {
                permissionPager.TotalCount,
                Datas = permissionPager.ToList()
            };
            return Json(objResult);
        }

        /// <summary>
        /// 查询角色权限数据
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询角色权限数据")]
        public ActionResult SearchRolePermission(RolePermissionFilterDto filter)
        {
            return SearchPermission(filter);
        }

        /// <summary>
        /// 查询用户权限数据
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询用户权限数据")]
        public ActionResult SearchUserPermission(UserPermissionFilterDto filter)
        {
            return SearchPermission(filter);
        }

        /// <summary>
        /// 查询操作功能绑定的权限
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationAction(Name = "查询操作功能绑定的权限")]
        public ActionResult SearchOperationPermission(OperationPermissionFilterDto filter)
        {
            filter.LoadGroup = true;
            return SearchPermission(filter);
        }

        #endregion

        #region 编辑/添加权限

        [AuthorizationAction(Name = "添加/编辑权限")]
        public ActionResult EditPermission(PermissionViewModel perission, long groupId = 0)
        {
            if (IsPost)
            {
                SavePermissionDto savePermissionDto = new SavePermissionDto()
                {
                    Permission = perission.MapTo<PermissionDto>()
                };
                var result = permissionAppService.SavePermission(savePermissionDto);
                var ajaxResult = AjaxResult.CopyFromResult(result);
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else
            {
                if (perission.Id < 1)
                {
                    if (groupId > 0)
                    {
                        perission.Group = permissionGroupAppService.GetPermissionGroup(new PermissionGroupFilterDto()
                        {
                            Ids = new List<long>() { groupId }
                        })?.MapTo<PermissionGroupViewModel>();
                        perission.Code = GuidCodeHelper.GetGuidUniqueCode().ToUpper();
                    }
                }
                else
                {
                    PermissionFilterDto filter = new PermissionFilterDto()
                    {
                        Ids = new List<long>()
                        {
                            perission.Id
                        },
                        LoadGroup = true
                    };
                    perission = permissionAppService.GetPermission(filter).MapTo<PermissionViewModel>();
                }
                return View(perission);
            }
        }

        #endregion

        #region 删除权限

        [HttpPost]
        [AuthorizationAction(Name = "删除权限")]
        public ActionResult RemovePermission(List<long> datas)
        {
            Result result = permissionAppService.RemovePermission(new RemovePermissionDto()
            {
                Ids = datas
            });
            return Json(result);
        }

        #endregion

        #region 启用/关闭权限

        [HttpPost]
        [AuthorizationAction(Name = "修改权限状态")]
        public ActionResult ModifyPermissionStatus(IEnumerable<long> ids, PermissionStatus status)
        {
            if (ids.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定权限"));
            }
            var result = permissionAppService.ModifyPermissionStatus(new ModifyPermissionStatusDto()
            {
                StatusInfos = ids.ToDictionary(c => c, c => status)
            });
            return Json(result);
        }

        #endregion

        #region 权限多选

        [AuthorizationAction(Name = "权限多选页面")]
        public ActionResult PermissionMultiSelect()
        {
            return View();
        }

        #endregion

        #region 权限&操作功能

        #region 权限绑定操作功能

        [HttpPost]
        [AuthorizationAction(Name = "添加权限绑定的操作功能")]
        public ActionResult AddPermissionOperation(long permissionId, IEnumerable<long> operationIds)
        {
            if (permissionId < 1)
            {
                return Json(Result.FailedResult("没有指定权限"));
            }
            if (operationIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定操作功能"));
            }
            ModifyPermissionOperationDto modifyPermissionOperationDto = new ModifyPermissionOperationDto()
            {
                Bindings = operationIds.Select(oid => new PermissionOperationDto()
                {
                    OperationId = oid,
                    PermissionId = permissionId
                })
            };
            return Json(permissionAppService.ModifyPermissionOperation(modifyPermissionOperationDto));
        }

        #endregion

        #region 权限解绑操作功能

        [HttpPost]
        [AuthorizationAction(Name = "解绑权限绑定的操作功能")]
        public ActionResult RemovePermissionOperation(long permissionId, IEnumerable<long> operationIds)
        {
            if (permissionId < 1)
            {
                return Json(Result.FailedResult("没有指定权限"));
            }
            if (operationIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定操作功能"));
            }
            ModifyPermissionOperationDto modifyPermissionOperationDto = new ModifyPermissionOperationDto()
            {
                Unbindings = operationIds.Select(oid => new PermissionOperationDto()
                {
                    OperationId = oid,
                    PermissionId = permissionId
                })
            };
            return Json(permissionAppService.ModifyPermissionOperation(modifyPermissionOperationDto));
        }

        #endregion 

        #endregion

        #region 检查权限码是否可用

        [HttpPost]
        [AuthorizationAction(Name = "检查权限编码是否可用")]
        public ActionResult CheckPermissionCode(PermissionViewModel authority)
        {
            bool allowUse = !permissionAppService.ExistPermissionCode(new ExistPermissionCodeDto()
            {
                ExcludeId = authority.Id,
                Code = authority.Code
            });
            return Content(allowUse.ToString().ToLower());
        }

        #endregion

        #region 检查权限名称是否可用

        [HttpPost]
        [AuthorizationAction(Name = "检查权限名称是否可用")]
        public ActionResult CheckPermissionName(PermissionViewModel authority)
        {
            bool allowUse = !permissionAppService.ExistPermissionName(new ExistPermissionNameDto()
            {
                ExcludeId = authority.Id,
                Name = authority.Name
            });
            return Content(allowUse.ToString().ToLower());
        }

        #endregion
    }
}
