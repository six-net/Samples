using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Framework.Extension;
using EZNEW.Framework.Response;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.ViewModel.Sys.Response;
using EZNEW.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Site.Console.Controllers.Sys
{
    public class RoleController : WebBaseController
    {

        IRoleAppService roleService = null;
        IUserAppService userService = null;
        IAuthAppService authService = null;

        public RoleController(IRoleAppService roleAppService, IUserAppService userAppService, IAuthAppService authAppService)
        {
            roleService = roleAppService;
            userService = userAppService;
            authService = authAppService;
        }

        #region 角色列表

        public ActionResult RoleList()
        {
            return View();
        }

        #endregion

        #region 编辑/添加角色

        public ActionResult EditRole(EditRoleViewModel role)
        {
            if (IsPost)
            {
                SaveRoleCmdDto roleCmd = new SaveRoleCmdDto()
                {
                    Role = role.MapTo<RoleCmdDto>()
                };
                var saveResult = roleService.SaveRole(roleCmd);
                Result result = saveResult.Success ? Result.SuccessResult(saveResult.Message) : Result.FailedResult(saveResult.Message);
                var ajaxResult = AjaxResult.CopyFromResult(result);
                ajaxResult.Data = saveResult.Data?.MapTo<RoleViewModel>();
                ajaxResult.SuccessClose = true;
                return Json(ajaxResult);
            }
            else if (role.SysNo > 0)
            {
                RoleFilterDto filter = new RoleFilterDto()
                {
                    SysNos = new List<long>()
                    {
                        role.SysNo
                    },
                    LoadParent = true
                };
                role = roleService.GetRole(filter).MapTo<EditRoleViewModel>();
            }
            return View(role);
        }

        #endregion

        #region 删除角色

        [HttpPost]
        public ActionResult DeleteRole(List<long> ids)
        {
            Result result = roleService.DeleteRole(new DeleteRoleCmdDto()
            {
                RoleIds = ids
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 修改角色排序

        [HttpPost]
        public ActionResult ChangeRoleSort(long sysNo, int sort)
        {
            Result result = null;
            result = roleService.ModifyRoleSort(new ModifyRoleSortCmdDto()
            {
                RoleSysNo = sysNo,
                NewSort = sort
            });
            var ajaxResult = AjaxResult.CopyFromResult(result);
            return Json(ajaxResult);
        }

        #endregion

        #region 查询角色数据

        [HttpPost]
        public IActionResult SearchRole(RoleFilterDto roleFilter)
        {
            var roles = roleService.GetRoleList(roleFilter).Select(c => c.MapTo<RoleViewModel>()).OrderBy(r => r.Sort).ToList();
            return Json(roles);
        }

        [HttpPost]
        public IActionResult SearchRolePaging(UserRoleFilterDto roleFilter)
        {
            var rolePaging = roleService.GetRolePaging(roleFilter).ConvertTo<RoleViewModel>();
            return Json(new
            {
                rolePaging.TotalCount,
                Datas = rolePaging.ToList()
            });
        }

        #endregion

        #region 角色多选

        public ActionResult RoleMultipleSelect()
        {
            return View();
        }

        #endregion

        #region 角色&用户

        #region 添加角色用户

        [HttpPost]
        public ActionResult AddRoleUser(long group, IEnumerable<long> datas)
        {
            ModifyUserBindRoleCmdDto bindInfo = new ModifyUserBindRoleCmdDto()
            {
                Binds = datas.Select(c => new Tuple<UserCmdDto, RoleCmdDto>(new UserCmdDto()
                {
                    SysNo = c
                }, new RoleCmdDto()
                {
                    SysNo = group
                }))
            };
            return Json(userService.ModifyUserBindRole(bindInfo));
        }

        #endregion

        #region 删除角色用户

        [HttpPost]
        public ActionResult RemoveRoleUser(long group, IEnumerable<long> datas)
        {
            ModifyUserBindRoleCmdDto bindInfo = new ModifyUserBindRoleCmdDto()
            {
                UnBinds = datas.Select(c => new Tuple<UserCmdDto, RoleCmdDto>(new UserCmdDto()
                {
                    SysNo = c
                }, new RoleCmdDto()
                {
                    SysNo = group
                }))
            };
            return Json(userService.ModifyUserBindRole(bindInfo));
        }

        #endregion

        #region 清除角色用户

        [HttpPost]
        public ActionResult ClearRoleUser(long group)
        {
            var result = roleService.ClearRoleUser(new long[1] { group });
            return Json(result);
        }

        #endregion

        #endregion

        #region 角色授权

        /// <summary>
        /// 添加角色授权
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="authSysNos">权限编码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRoleAuthorize(long roleId, List<long> authSysNos)
        {
            ModifyRoleAuthorizeCmdDto authorizeInfo = new ModifyRoleAuthorizeCmdDto()
            {
                Binds = authSysNos?.Select(c => new Tuple<RoleCmdDto, AuthorityCmdDto>(new RoleCmdDto()
                {
                    SysNo = roleId
                }, new AuthorityCmdDto()
                {
                    SysNo = c
                }))
            };
            return Json(authService.ModifyRoleAuthorize(authorizeInfo));
        }

        /// <summary>
        /// 删除角色授权
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="authSysNos">权限编码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveRoleAuthorize(long roleId, List<long> authSysNos)
        {
            ModifyRoleAuthorizeCmdDto authorizeInfo = new ModifyRoleAuthorizeCmdDto()
            {
                UnBinds = authSysNos?.Select(c => new Tuple<RoleCmdDto, AuthorityCmdDto>(new RoleCmdDto()
                {
                    SysNo = roleId
                }, new AuthorityCmdDto()
                {
                    SysNo = c
                }))
            };
            return Json(authService.ModifyRoleAuthorize(authorizeInfo));
        }

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ClearRoleAuthorize(long roleId)
        {
            var result = authService.ClearRoleAuthorize(new long[1] { roleId });
            return Json(result);
        }

        #endregion

        #region 检查角色名称是否存在

        [HttpPost]
        public ActionResult CheckRoleName(RoleViewModel role)
        {
            bool allowUse = !roleService.ExistRoleName(new ExistRoleNameCmdDto()
            {
                RoleName = role?.Name,
                ExcludeRoleId = role?.SysNo ?? 0
            });
            return Content(allowUse.ToString().ToLower());
        }

        #endregion
    }
}