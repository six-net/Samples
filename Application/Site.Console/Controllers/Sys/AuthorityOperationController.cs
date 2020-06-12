using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Module.Sys;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Response;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.ViewModel.Sys.Response;
using EZNEW.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Site.Console.Controllers.Sys
{
    public class AuthorityOperationController : WebBaseController
    {
        IAuthAppService authService = null;

        public AuthorityOperationController(IAuthAppService authAppService)
        {
            authService = authAppService;
        }

        #region 操作分组

        #region 授权操作组列表

        public ActionResult AuthorityOperationGroupList()
        {
            return View();
        }

        #endregion

        #region 编辑/添加授权操作组

        public ActionResult EditAuthorityOperationGroup(EditAuthorityOperationGroupViewModel authorityOperationGroup)
        {
            if (IsPost)
            {
                SaveAuthorityOperationGroupCmdDto saveInfo = new SaveAuthorityOperationGroupCmdDto()
                {
                    AuthorityOperationGroup = authorityOperationGroup.MapTo<AuthorityOperationGroupCmdDto>()
                };
                var saveResult = authService.SaveAuthorityOperationGroup(saveInfo);
                var ajaxResult = AjaxResult.CopyFromResult(saveResult);
                ajaxResult.Data = saveResult.Data?.MapTo<AuthorityOperationGroupViewModel>();
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else if (!(authorityOperationGroup.SysNo <= 0))
            {
                AuthorityOperationGroupFilterDto filter = new AuthorityOperationGroupFilterDto()
                {
                    SysNos = new List<long>()
                    {
                        authorityOperationGroup.SysNo
                    }
                };
                authorityOperationGroup = authService.GetAuthorityOperationGroup(filter).MapTo<EditAuthorityOperationGroupViewModel>();
            }
            return View(authorityOperationGroup);
        }

        #endregion

        #region 删除授权操作组

        [HttpPost]
        public ActionResult DeleteAuthorityOperationGroup(List<long> ids)
        {
            Result result = authService.DeleteAuthorityOperationGroup(new DeleteAuthorityOperationGroupCmdDto()
            {
                AuthorityOperationGroupIds = ids
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 修改授权操作组排序

        [HttpPost]
        public ActionResult ChangeAuthorityOperationGroupSort(long sysNo, int sort)
        {
            Result result = null;
            result = authService.ModifyAuthorityOperationGroupSort(new ModifyAuthorityOperationGroupSortCmdDto()
            {
                AuthorityOperationGroupSysNo = sysNo,
                NewSort = sort
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 查询授权操作分组数据

        [HttpPost]
        public IActionResult SearchAuthorityOperationGroup(AuthorityOperationGroupFilterDto filter)
        {
            var authorityOperationGroups = authService.GetAuthorityOperationGroupList(filter).Select(c => c.MapTo<AuthorityOperationGroupViewModel>()).OrderBy(r => r.Sort).ToList();
            return Json(authorityOperationGroups);
        }

        #endregion

        #region 检查操作分组名称是否可用

        [HttpPost]
        public ActionResult CheckAuthorityOperationGroupName(AuthorityOperationGroupViewModel group)
        {
            ExistAuthorityOperationGroupNameCmdDto existInfo = new ExistAuthorityOperationGroupNameCmdDto()
            {
                GroupName = group.Name,
                ExcludeGroupId = group.SysNo
            };
            bool allowUser = !authService.ExistAuthorityOperationGroupName(existInfo);
            return Content(allowUser.ToString().ToLower());
        }

        #endregion

        #endregion

        #region 授权操作

        #region 编辑/添加授权操作

        public ActionResult EditAuthorityOperation(EditAuthorityOperationViewModel authorityOperation, long groupSysNo = 0)
        {
            if (IsPost)
            {
                SaveAuthorityOperationCmdDto saveInfo = new SaveAuthorityOperationCmdDto()
                {
                    AuthorityOperation = authorityOperation.MapTo<AuthorityOperationCmdDto>()
                };
                Result<AuthorityOperationDto> result = authService.SaveAuthorityOperation(saveInfo);
                var ajaxResult = AjaxResult.CopyFromResult(result);
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else
            {
                if (authorityOperation.SysNo > 0)
                {
                    AuthorityOperationFilterDto filter = new AuthorityOperationFilterDto()
                    {
                        SysNos = new List<long>()
                    {
                        authorityOperation.SysNo
                    },
                        LoadGroup = true
                    };
                    authorityOperation = authService.GetAuthorityOperation(filter).MapTo<EditAuthorityOperationViewModel>();
                    if (authorityOperation == null)
                    {
                        return Content("没有指定要操作的数据");
                    }
                }
                else if (groupSysNo > 0)
                {
                    authorityOperation.Group = authService.GetAuthorityOperationGroup(new AuthorityOperationGroupFilterDto()
                    {
                        SysNos = new List<long>() { groupSysNo }
                    })?.MapTo<EditAuthorityOperationGroupViewModel>();
                }
            }
            return View(authorityOperation);
        }

        #endregion

        #region 删除授权操作

        public ActionResult DeleteAuthorityOperation(List<long> datas)
        {
            Result result = authService.DeleteAuthorityOperation(new DeleteAuthorityOperationCmdDto()
            {
                AuthorityOperationIds = datas
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
        public ActionResult SearchAuthorityOperation(AuthorityOperationFilterDto filter)
        {
            var authorityOperationPaging = authService.GetAuthorityOperationPaging(filter).ConvertTo<AuthorityOperationViewModel>();
            var result = new
            {
                authorityOperationPaging.TotalCount,
                Datas = authorityOperationPaging.ToList()
            };
            return Json(result);
        }

        /// <summary>
        /// 查询权限绑定的操作功能
        /// </summary>
        /// <param name="filter">权限绑定操作功能筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchAuthorityBindOperation(AuthorityBindOperationFilterDto filter)
        {
            filter.LoadGroup = true;
            return SearchAuthorityOperation(filter);
        }

        /// <summary>
        /// 查询用户授予的的所有操作功能
        /// </summary>
        /// <param name="filter">用户授权功能筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchUserOperation(UserOperationFilterDto filter)
        {
            List<AuthorityOperationDto> operationList = null;
            if (filter?.UserFilter?.SysNos.IsNullOrEmpty() ?? true)
            {
                operationList = new List<AuthorityOperationDto>(0);
            }
            else
            {
                operationList = authService.GetAuthorityOperationList(filter) ?? new List<AuthorityOperationDto>(0);
            }
            return Json(operationList);
        }

        /// <summary>
        /// 查询当前登录用户授予的所有操作功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchCurrentUserOperation()
        {
            var userId = User.Id;
            UserOperationFilterDto filterDto = new UserOperationFilterDto()
            {
                UserFilter = new UserFilterDto()
                {
                    SysNos = new List<long>(1) { userId }
                }
            };
            return SearchUserOperation(filterDto);
        }

        #endregion

        #region 启用/关闭操作

        [HttpPost]
        public ActionResult ModifyAuthorityOperationStatus(string sysNos, AuthorityOperationStatus status)
        {
            IEnumerable<long> sysNoArray = sysNos.LSplit(",").Select(s => long.Parse(s));
            Dictionary<long, AuthorityOperationStatus> statusInfo = new Dictionary<long, AuthorityOperationStatus>();
            foreach (var sysNo in sysNoArray)
            {
                statusInfo.Add(sysNo, status);
            }
            var result = authService.ModifyAuthorityOperationStatus(new ModifyAuthorityOperationStatusCmdDto()
            {
                StatusInfo = statusInfo
            });
            return Json(result);
        }

        #endregion

        #region 检查操作名称是否存在

        [HttpPost]
        public ActionResult CheckAuthorityOperationName(AuthorityOperationViewModel operation)
        {
            bool allowUse = true;
            if (operation.Name.IsNullOrEmpty())
            {
                allowUse = false;
            }
            else
            {
                allowUse = !authService.ExistAuthorityOperationName(operation.Name, operation.SysNo);
            }
            return Content(allowUse ? "true" : "false");
        }

        #endregion

        #region 操作功能&权限

        #region 查看授权操作对应的权限

        public ActionResult AuthorityOperationBindAuthorityList(long id)
        {
            ViewBag.OperationId = id;
            return View();
        }

        #endregion

        #region 操作绑定权限

        [HttpPost]
        public ActionResult OperationBindAuthority(long operationId, IEnumerable<long> authIds)
        {
            ModifyAuthorityBindAuthorityOperationCmdDto bindInfo = new ModifyAuthorityBindAuthorityOperationCmdDto()
            {
                Binds = authIds.Select(c => new Tuple<AuthorityCmdDto, AuthorityOperationCmdDto>(new AuthorityCmdDto()
                {
                    SysNo = c
                },
                new AuthorityOperationCmdDto()
                {
                    SysNo = operationId
                }))
            };
            return Json(authService.ModifyAuthorityOperationBindAuthority(bindInfo));
        }

        #endregion

        #region 操作解绑权限

        [HttpPost]
        public ActionResult OperationUnBindAuthority(long operationId, IEnumerable<long> authIds)
        {
            ModifyAuthorityBindAuthorityOperationCmdDto bindInfo = new ModifyAuthorityBindAuthorityOperationCmdDto()
            {
                UnBinds = authIds.Select(c => new Tuple<AuthorityCmdDto, AuthorityOperationCmdDto>(new AuthorityCmdDto()
                {
                    SysNo = c
                },
                new AuthorityOperationCmdDto()
                {
                    SysNo = operationId
                }))
            };
            return Json(authService.ModifyAuthorityOperationBindAuthority(bindInfo));
        }

        #endregion 

        #endregion

        #region 操作多选

        public ActionResult AuthorityOperationMultiSelect()
        {
            //var result = InitAuthorityOperationGroupSelectTreeData();
            //ViewBag.AllNodes = result.Item1;
            return View();
        }

        #endregion

        #endregion
    }
}