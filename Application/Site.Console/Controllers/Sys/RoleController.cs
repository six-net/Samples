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
    [AuthorizationGroup(Name = "角色", Parent = "账户/授权")]
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

        [AuthorizationAction(Name = "角色列表")]
        public ActionResult RoleList()
        {
            return View();
        }

        #endregion

        #region 编辑/添加角色

        [AuthorizationAction(Name = "添加/编辑角色")]
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

        #region 角色详情

        [AuthorizationAction(Name = "查看角色详情")]
        public ActionResult RoleDetail(long id)
        {
            RoleViewModel role = null;
            if (id > 0)
            {
                RoleFilterDto filter = new RoleFilterDto()
                {
                    Ids = new List<long>()
                    {
                        id
                    }
                };
                role = roleAppService.GetRole(filter).MapTo<RoleViewModel>();
            }
            if (role == null)
            {
                return Content("没有找到角色信息");
            }
            return View(role);
        }

        #endregion

        #region 删除角色

        [HttpPost]
        [AuthorizationAction(Name = "删除角色")]
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

        #region 查询角色数据

        [HttpPost]
        [AuthorizationAction(Name = "查询角色数据")]
        public IActionResult SearchRole(RoleFilterDto roleFilter)
        {
            var rolePaging = roleAppService.GetRolePaging(roleFilter).ConvertTo<RoleViewModel>();
            object dataResult = new
            {
                rolePaging.TotalCount,
                Datas = rolePaging.ToList()
            };
            return Json(dataResult);
        }

        [HttpPost]
        [AuthorizationAction(Name = "查询用户绑定角色数据")]
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

        public ActionResult RoleMultiSelect()
        {
            return View();
        }

        #endregion

        #region 角色&用户

        #region 添加角色用户

        [HttpPost]
        [AuthorizationAction(Name = "添加角色用户")]
        public ActionResult AddRoleUser(long roleId, IEnumerable<long> userIds)
        {
            if (roleId < 1)
            {
                return Json(Result.FailedResult("没有指定角色"));
            }
            if (userIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定用户"));
            }
            ModifyUserRoleDto modifyUserRoleDto = new ModifyUserRoleDto()
            {
                Bindings = userIds.Select(uid => new UserRoleDto()
                {
                    RoleId = roleId,
                    UserId = uid
                })
            };
            return Json(userAppService.ModifyUserRole(modifyUserRoleDto));
        }

        #endregion

        #region 删除角色用户

        [HttpPost]
        [AuthorizationAction(Name = "删除角色用户")]
        public ActionResult RemoveRoleUser(long roleId, IEnumerable<long> userIds)
        {
            if (roleId < 1)
            {
                return Json(Result.FailedResult("没有指定角色"));
            }
            if (userIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定用户"));
            }
            ModifyUserRoleDto modifyUserRoleDto = new ModifyUserRoleDto()
            {
                Unbindings = userIds.Select(uid => new UserRoleDto()
                {
                    RoleId = roleId,
                    UserId = uid
                })
            };
            return Json(userAppService.ModifyUserRole(modifyUserRoleDto));
        }

        #endregion

        #region 清除角色用户

        [HttpPost]
        [AuthorizationAction(Name = "清除角色用户")]
        public ActionResult ClearRoleUser(long roleId)
        {
            var result = roleAppService.ClearUser(new long[1] { roleId });
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
        [AuthorizationAction(Name = "添加角色权限")]
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
        [AuthorizationAction(Name = "删除角色权限")]
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
        [AuthorizationAction(Name = "清除角色权限")]
        public ActionResult ClearRolePermission(long roleId)
        {
            var result = rolePermissionAppService.ClearRolePermission(new long[1] { roleId });
            return Json(result);
        }

        #endregion

        #region 检查角色名称是否存在

        [HttpPost]
        [AuthorizationAction(Name = "检查角色名称是否存在")]
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
