using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Application.Identity.Auth;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Framework.Code;
using EZNEW.Framework.Extension;
using EZNEW.Framework.Paging;
using EZNEW.Framework.Response;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.ViewModel.Sys.Response;
using EZNEW.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Site.Console.Controllers.Sys
{
    public class AuthorityController : WebBaseController
    {
        IAuthAppService authService = null;

        public AuthorityController(IAuthAppService authAppService)
        {
            authService = authAppService;
        }

        #region 权限分组管理

        #region 权限分组列表

        public ActionResult AuthorityGroupList()
        {
            return View();
        }

        #endregion

        #region 编辑/添加权限分组

        public ActionResult EditAuthorityGroup(EditAuthorityGroupViewModel authorityGroup)
        {
            if (IsPost)
            {
                Result<AuthorityGroupDto> saveResult = authService.SaveAuthorityGroup(authorityGroup.MapTo<SaveAuthorityGroupCmdDto>());
                var ajaxResult = AjaxResult.CopyFromResult(saveResult);
                ajaxResult.Data = saveResult.Data?.MapTo<AuthorityGroupViewModel>();
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else if (authorityGroup.SysNo > 0)
            {
                AuthorityGroupFilterDto filter = new AuthorityGroupFilterDto()
                {
                    SysNos = new List<long>()
                    {
                        authorityGroup.SysNo
                    }
                };
                authorityGroup = authService.GetAuthorityGroup(filter).MapTo<EditAuthorityGroupViewModel>();
            }
            return View(authorityGroup);
        }

        #endregion

        #region 删除权限分组

        [HttpPost]
        public ActionResult DeleteAuthorityGroup(List<long> ids)
        {
            Result result = authService.DeleteAuthorityGroup(new DeleteAuthorityGroupCmdDto()
            {
                AuthorityGroupIds = ids
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 修改权限分组排序

        [HttpPost]
        public ActionResult ChangeAuthorityGroupSort(long sysNo, int sort)
        {
            Result result = null;
            result = authService.ModifyAuthorityGroupSort(new ModifyAuthorityGroupSortCmdDto()
            {
                AuthorityGroupSysNo = sysNo,
                NewSort = sort
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 查询权限分组数据

        [HttpPost]
        public IActionResult SearchAuthorityGroup(AuthorityGroupFilterDto filter)
        {
            var authorityGroups = authService.GetAuthorityGroupList(filter).Select(c => c.MapTo<AuthorityGroupViewModel>()).OrderBy(r => r.Sort).ToList();
            return Json(authorityGroups);
        }

        #endregion

        #region 验证权限分组名称是否存在

        [HttpPost]
        public ActionResult CheckAuthorityGroupName(AuthorityGroupViewModel group)
        {
            ExistAuthorityGroupNameCmdDto existInfo = new ExistAuthorityGroupNameCmdDto()
            {
                GroupName = group.Name,
                ExcludeGroupId = group.SysNo
            };
            bool allowUse = !authService.ExistAuthorityGroupName(existInfo);
            return Content(allowUse.ToString().ToLower());
        }

        #endregion

        #endregion

        #region 权限管理

        #region 查询权限数据

        /// <summary>
        /// 查询权限数据
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchAuthority(AuthorityFilterDto filter)
        {
            IPaging<AuthorityViewModel> authorityPager = authService.GetAuthorityPaging(filter).ConvertTo<AuthorityViewModel>();
            object objResult = new
            {
                authorityPager.TotalCount,
                Datas = authorityPager.ToList()
            };
            return Json(objResult);
        }

        /// <summary>
        /// 查询角色权限数据
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchRoleAuthority(RoleAuthorityFilterDto filter)
        {
            return SearchAuthority(filter);
        }

        /// <summary>
        /// 查询用户权限数据
        /// </summary>
        /// <param name="filter">权限筛选信息</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchUserAuthority(UserAuthorityFilterDto filter)
        {
            return SearchAuthority(filter);
        }

        /// <summary>
        /// 查询操作功能绑定的权限
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchAuthorityOperationBindAuthority(AuthorityOperationBindAuthorityFilterDto filter)
        {
            filter.LoadGroup = true;
            return SearchAuthority(filter);
        }

        #endregion

        #region 编辑/添加权限

        public ActionResult EditAuthority(EditAuthorityViewModel authority, long groupSysNo = 0)
        {
            if (IsPost)
            {
                SaveAuthorityCmdDto saveInfo = new SaveAuthorityCmdDto()
                {
                    Authority = authority.MapTo<AuthorityCmdDto>()
                };
                var result = authService.SaveAuthority(saveInfo);
                var ajaxResult = AjaxResult.CopyFromResult(result);
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else
            {
                if (authority.SysNo < 1)
                {
                    if (groupSysNo > 0)
                    {
                        authority.Group = authService.GetAuthorityGroup(new AuthorityGroupFilterDto()
                        {
                            SysNos = new List<long>() { groupSysNo }
                        })?.MapTo<EditAuthorityGroupViewModel>();
                        authority.Code = GuidCode.GetUniqueCode().ToUpper();
                    }
                }
                else
                {
                    AuthorityFilterDto filter = new AuthorityFilterDto()
                    {
                        SysNos = new List<long>()
                        {
                            authority.SysNo
                        },
                        LoadGroup = true
                    };
                    authority = authService.GetAuthority(filter).MapTo<EditAuthorityViewModel>();
                }
                return View(authority);
            }
        }

        #endregion

        #region 删除权限

        [HttpPost]
        public ActionResult DeleteAuthority(List<long> datas)
        {
            Result result = authService.DeleteAuthority(new DeleteAuthorityCmdDto()
            {
                SysNos = datas
            });
            return Json(result);
        }

        #endregion

        #region 启用/关闭权限

        [HttpPost]
        public ActionResult ModifyAuthorityStatus(string codes, AuthorityStatus status)
        {
            IEnumerable<string> codeList = codes.LSplit(",");
            Dictionary<string, AuthorityStatus> statusInfo = new Dictionary<string, AuthorityStatus>();
            foreach (var code in codeList)
            {
                statusInfo.Add(code, status);
            }
            var result = authService.ModifyAuthorityStatus(new ModifyAuthorityStatusCmdDto()
            {
                AuthorityStatusInfo = statusInfo
            });
            return Json(result);
        }

        #endregion

        #region 权限多选

        public ActionResult AuthorityMultiSelect()
        {
            //var result = InitAuthorityGroupSelectTreeData();
            //ViewBag.AllNodes = result.Item1;
            return View();
        }

        #endregion

        #region 权限&操作功能

        #region 权限关联的操作功能

        public ActionResult AuthorityBindOperationList(long sysNo)
        {
            ViewBag.AuthoritySysNo = sysNo;
            return View();
        }

        #endregion

        #region 权限绑定操作功能

        [HttpPost]
        public ActionResult AuthorityBindOperation(long sysNo, IEnumerable<long> operationIds)
        {
            ModifyAuthorityBindAuthorityOperationCmdDto bindInfo = new ModifyAuthorityBindAuthorityOperationCmdDto()
            {
                Binds = operationIds.Select(c => new Tuple<AuthorityCmdDto, AuthorityOperationCmdDto>(new AuthorityCmdDto()
                {
                    SysNo = sysNo
                },
                new AuthorityOperationCmdDto()
                {
                    SysNo = c
                }))
            };
            return Json(authService.ModifyAuthorityOperationBindAuthority(bindInfo));
        }

        #endregion

        #region 权限解绑操作功能

        [HttpPost]
        public ActionResult AuthorityUnBindOperation(long sysNo, IEnumerable<long> operationIds)
        {
            ModifyAuthorityBindAuthorityOperationCmdDto bindInfo = new ModifyAuthorityBindAuthorityOperationCmdDto()
            {
                UnBinds = operationIds.Select(c => new Tuple<AuthorityCmdDto, AuthorityOperationCmdDto>(new AuthorityCmdDto()
                {
                    SysNo = sysNo
                },
                new AuthorityOperationCmdDto()
                {
                    SysNo = c
                }))
            };
            return Json(authService.ModifyAuthorityOperationBindAuthority(bindInfo));
        }

        #endregion 

        #endregion

        #region 检查权限码是否可用

        [HttpPost]
        public ActionResult CheckAuthorityCode(AuthorityViewModel authority)
        {
            bool allowUse = !authService.ExistAuthorityCode(new ExistAuthorityCodeCmdDto()
            {
                SysNo = authority.SysNo,
                AuthCode = authority.Code
            });
            return Content(allowUse.ToString().ToLower());
        }

        #endregion

        #region 检查权限名称是否可用

        [HttpPost]
        public ActionResult CheckAuthorityName(AuthorityViewModel authority)
        {
            bool allowUse = !authService.ExistAuthorityName(new ExistAuthorityNameCmdDto()
            {
                SysNo = authority.SysNo,
                Name = authority.Name
            });
            return Content(allowUse.ToString().ToLower());
        }

        #endregion

        #endregion
    }
}