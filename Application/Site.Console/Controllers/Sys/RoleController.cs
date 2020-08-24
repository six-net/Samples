using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.AppServiceContract.Sys;
using EZNEW.Domain.Sys.Model;
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
    [AuthorizationOperationGroup(Name = "角色", Parent = "账户/授权")]
    public class RoleController : WebBaseController
    {
        readonly IRoleAppService roleAppService;
        readonly IUserAppService userAppService;
        readonly IRolePermissionAppService rolePermissionAppService;

        public RoleController(IRoleAppService roleAppService
            , IUserAppService userAppService
            , IRolePermissionAppService rolePermissionAppService)
        {
            this.roleAppService = roleAppService;
            this.userAppService = userAppService;
            this.rolePermissionAppService = rolePermissionAppService;
        }

        #region 角色列表

        [AuthorizationOperation(Name = "角色列表")]
        public ActionResult RoleList()
        {
            return View();
        }

        #endregion

        #region 编辑/添加角色

        [AuthorizationOperation(Name = "添加/编辑角色")]
        public ActionResult EditRole(RoleViewModel role)
        {
            if (IsPost)
            {
                SaveRoleDto saveRoleDto = new SaveRoleDto()
                {
                    Role = role.MapTo<RoleDto>()
                };
                var saveResult = roleAppService.SaveRole(saveRoleDto);
                Result result = saveResult.Success ? Result.SuccessResult(saveResult.Message) : Result.FailedResult(saveResult.Message);
                var ajaxResult = AjaxResult.CopyFromResult(result);
                ajaxResult.Data = saveResult.Data?.MapTo<RoleViewModel>();
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else if (role.Id > 0)
            {
                RoleFilterDto filter = new RoleFilterDto()
                {
                    Ids = new List<long>()
                    {
                        role.Id
                    },
                    LoadParent = true
                };
                role = roleAppService.GetRole(filter).MapTo<RoleViewModel>();
            }
            return View(role);
        }

        #endregion

        #region 删除角色

        [HttpPost]
        [AuthorizationOperation(Name = "删除角色")]
        public ActionResult RemoveRole(List<long> ids)
        {
            Result result = roleAppService.RemoveRole(new RemoveRoleDto()
            {
                Ids = ids
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 修改角色排序

        [HttpPost]
        [AuthorizationOperation(Name = "修改角色排序")]
        public ActionResult ChangeRoleSort(long id, int sort)
        {
            Result result = null;
            result = roleAppService.ModifyRoleSort(new ModifyRoleSortDto()
            {
                Id = id,
                NewSort = sort
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 查询角色数据

        [HttpPost]
        [AuthorizationOperation(Name = "查询角色数据")]
        public IActionResult SearchRole(RoleFilterDto roleFilter)
        {
            var roles = roleAppService.GetRoleList(roleFilter).Select(c => c.MapTo<RoleViewModel>()).OrderBy(r => r.Sort).ToList();
            return Json(roles);
        }

        [HttpPost]
        [AuthorizationOperation(Name = "查询用户绑定角色数据")]
        public IActionResult SearchRolePaging(UserRoleFilterDto roleFilter)
        {
            var rolePaging = roleAppService.GetRolePaging(roleFilter).ConvertTo<RoleViewModel>();
            return Json(new
            {
                rolePaging.TotalCount,
                Datas = rolePaging.ToList()
            });
        }

        #endregion

        #region 角色多选

        [AuthorizationOperation(Name = "角色多选页面")]
        public ActionResult RoleMultipleSelect()
        {
            return View();
        }

        #endregion

        #region 角色&用户

        #region 添加角色用户

        [HttpPost]
        [AuthorizationOperation(Name = "添加角色用户")]
        public ActionResult AddRoleUser(long group, IEnumerable<long> datas)
        {
            if (group < 1)
            {
                return Json(Result.FailedResult("没有指定角色"));
            }
            if (datas.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定用户"));
            }
            ModifyUserRoleDto modifyUserRoleDto = new ModifyUserRoleDto()
            {
                Bindings = datas.Select(uid => new UserRoleDto()
                {
                    RoleId = group,
                    UserId = uid
                })
            };
            return Json(userAppService.ModifyUserRole(modifyUserRoleDto));
        }

        #endregion

        #region 删除角色用户

        [HttpPost]
        [AuthorizationOperation(Name = "删除角色用户")]
        public ActionResult RemoveRoleUser(long group, IEnumerable<long> datas)
        {
            if (group < 1)
            {
                return Json(Result.FailedResult("没有指定角色"));
            }
            if (datas.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定用户"));
            }
            ModifyUserRoleDto modifyUserRoleDto = new ModifyUserRoleDto()
            {
                Unbindings = datas.Select(uid => new UserRoleDto()
                {
                    RoleId = group,
                    UserId = uid
                })
            };
            return Json(userAppService.ModifyUserRole(modifyUserRoleDto));
        }

        #endregion

        #region 清除角色用户

        [HttpPost]
        [AuthorizationOperation(Name = "清除角色用户")]
        public ActionResult ClearRoleUser(long group)
        {
            var result = roleAppService.ClearUser(new long[1] { group });
            return Json(result);
        }

        #endregion

        #endregion

        #region 角色授权

        /// <summary>
        /// 添加角色授权
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="permissionIds">权限编码</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationOperation(Name = "添加角色权限")]
        public ActionResult AddRolePermission(long roleId, List<long> permissionIds)
        {
            if (roleId < 1)
            {
                return Json(Result.FailedResult("没有指定角色"));
            }
            if (permissionIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定权限"));
            }
            ModifyRolePermissionDto modifyRolePermissionDto = new ModifyRolePermissionDto()
            {
                Bindings = permissionIds?.Select(pid => new RolePermissionDto()
                {
                    PermissionId = pid,
                    RoleId = roleId
                })
            };
            return Json(rolePermissionAppService.ModifyRolePermission(modifyRolePermissionDto));
        }

        /// <summary>
        /// 删除角色授权
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="authSysNos">权限编码</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationOperation(Name = "删除角色权限")]
        public ActionResult RemoveRolePermission(long roleId, List<long> permissionIds)
        {
            if (roleId < 1)
            {
                return Json(Result.FailedResult("没有指定角色"));
            }
            if (permissionIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定权限"));
            }
            ModifyRolePermissionDto modifyRolePermissionDto = new ModifyRolePermissionDto()
            {
                Unbindings = permissionIds?.Select(pid => new RolePermissionDto()
                {
                    PermissionId = pid,
                    RoleId = roleId
                })
            };
            return Json(rolePermissionAppService.ModifyRolePermission(modifyRolePermissionDto));
        }

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationOperation(Name = "清除角色权限")]
        public ActionResult ClearRolePermission(long roleId)
        {
            var result = rolePermissionAppService.ClearRolePermission(new long[1] { roleId });
            return Json(result);
        }

        #endregion

        #region 检查角色名称是否存在

        [HttpPost]
        [AuthorizationOperation(Name = "检查角色名称是否存在")]
        public ActionResult CheckRoleName(RoleViewModel role)
        {
            bool allowUse = !roleAppService.ExistRoleName(new ExistRoleNameDto()
            {
                Name = role?.Name,
                ExcludeId = role?.Id ?? 0
            });
            return Content(allowUse.ToString().ToLower());
        }

        #endregion
    }
}
