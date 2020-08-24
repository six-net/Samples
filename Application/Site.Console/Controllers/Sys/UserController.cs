using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Module.Sys;
using EZNEW.Paging;
using EZNEW.Response;
using EZNEW.ViewModel.Sys;
using EZNEW.Web.Mvc;
using EZNEW.Web.Security.Authorization;

namespace Site.Console.Controllers.Sys
{
    [AuthorizationOperationGroup(Name = "用户", Parent = "账户/授权")]
    public class UserController : WebBaseController
    {
        readonly IUserAppService userAppService;
        readonly IUserPermissionAppService userPermissionAppService;

        public UserController(IUserAppService userAppService
            , IUserPermissionAppService userPermissionAppService)
        {
            this.userAppService = userAppService;
            this.userPermissionAppService = userPermissionAppService;
        }

        #region 用户列表

        [AuthorizationOperation(Name = "用户列表")]
        public ActionResult UserList()
        {
            return View();
        }

        #endregion

        #region 搜索用户数据

        [HttpPost]
        [AuthorizationOperation(Name = "查询用户数据")]
        public ActionResult SearchUser(RoleUserFilterDto filter)
        {
            filter.UserType = UserType.Management;
            IPaging<UserViewModel> userPager = userAppService.GetUserPaging(filter).ConvertTo<UserViewModel>();
            object dataResult = new
            {
                userPager.TotalCount,
                Datas = userPager.ToList()
            };
            return Json(dataResult);
        }

        #endregion

        #region 用户详情

        [AuthorizationOperation(Name = "用户详情页面")]
        public ActionResult UserDetail(long id)
        {
            UserViewModel user = null;
            if (id > 0)
            {
                UserFilterDto filter = new UserFilterDto()
                {
                    Ids = new List<long>()
                    {
                        id
                    },
                };
                user = userAppService.GetUser(filter).MapTo<UserViewModel>();
            }
            if (user == null)
            {
                return Content("没有找到用户信息");
            }
            return View(user);
        }

        #endregion

        #region 添加/编辑用户

        [AuthorizationOperation(Name = "添加/编辑用户")]
        public ActionResult EditUser(UserViewModel user)
        {
            if (IsPost)
            {
                user.UserType = UserType.Management;
                UserDto userDto = user.MapTo<UserDto>();
                SaveUserDto saveUserDto = new SaveUserDto()
                {
                    User = userDto
                };
                var result = userAppService.SaveUser(saveUserDto);
                return Json(result);
            }
            else if (user.Id > 0)
            {
                UserFilterDto filter = new UserFilterDto()
                {
                    Ids = new List<long>()
                    {
                        user.Id
                    }
                };
                user = userAppService.GetUser(filter).MapTo<UserViewModel>();
            }
            return View(user);
        }

        #endregion

        #region 删除用户

        [AuthorizationOperation(Name = "删除用户")]
        public ActionResult RemoveUser(List<long> ids)
        {
            Result result = userAppService.RemoveUser(new RemoveUserDto()
            {
                Ids = ids
            });
            return Json(result);
        }

        #endregion

        #region 验证登陆名是否存在

        [HttpPost]
        [AuthorizationOperation(Name = "验证用户名是否存在")]
        public ActionResult CheckUserName(string userName)
        {
            bool allowUse = true;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                UserFilterDto filter = new UserFilterDto()
                {
                    UserName = userName
                };
                var user = userAppService.GetUser(filter);
                allowUse = user == null;
            }
            return Content(allowUse ? "true" : "false");
        }

        #endregion

        #region 修改用户状态

        [HttpPost]
        [AuthorizationOperation(Name = "修改用户状态")]
        public ActionResult ModifyUserStatus(long id, UserStatus status)
        {
            ModifyUserStatusDto statusInfo = new ModifyUserStatusDto()
            {
                StatusInfos = new Dictionary<long, UserStatus>() { { id, status } }
            };
            return Json(userAppService.ModifyStatus(statusInfo));
        }

        #endregion

        #region 修改密码

        [AuthorizationOperation(Name = "修改用户密码")]
        public ActionResult ModifyUserPassword(ModifyUserPasswordViewModel modifyInfo)
        {
            if (IsPost)
            {
                ModelState.Remove("CurrentPassword");
                if (!ModelState.IsValid)
                {
                    return Json(Result.FailedResult("提交数据有错误"));
                }
                var modifyInfoDto = modifyInfo.MapTo<ModifyUserPasswordDto>();
                modifyInfoDto.CheckCurrentPassword = false;
                var modifyResult = userAppService.ModifyPassword(modifyInfoDto);
                var result = AjaxResult.CopyFromResult(modifyResult);
                result.SuccessClose = true;
                return Json(result);
            }
            return View(modifyInfo);
        }

        [AuthorizationOperation(Name = "修改自己的登录密码")]
        public IActionResult ModifySelfPassword(ModifyUserPasswordViewModel modifyInfo)
        {
            if (IsPost)
            {
                if (!ModelState.IsValid)
                {
                    return Json(Result.FailedResult("提交数据有错误"));
                }
                var modifyInfoDto = modifyInfo.MapTo<ModifyUserPasswordDto>();
                modifyInfoDto.CheckCurrentPassword = true;
                modifyInfoDto.UserId = User.Id;
                var result = AjaxResult.CopyFromResult(userAppService.ModifyPassword(modifyInfoDto));
                result.SuccessClose = true;
                return Json(result);
            }
            return View("ModifySelfPassword");
        }

        #endregion

        #region 用户多选

        [AuthorizationOperation(Name = "用户多选页面")]
        public ActionResult UserMultiSelect()
        {
            return View();
        }

        #endregion

        #region 用户&角色

        #region 移除用户角色

        [HttpPost]
        [AuthorizationOperation(Name = "删除用户角色")]
        public ActionResult RemoveUserRole(long userId, IEnumerable<long> roleIds)
        {
            if (userId < 1)
            {
                return Json(Result.FailedResult("没有指定用户数据"));
            }
            if (roleIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定角色数据"));
            }
            ModifyUserRoleDto modifyUserRoleDto = new ModifyUserRoleDto()
            {
                Unbindings = roleIds.Select(rid => new UserRoleDto()
                {
                    RoleId = rid,
                    UserId = userId
                })
            };
            return Json(userAppService.ModifyUserRole(modifyUserRoleDto));
        }

        #endregion

        #region 添加用户角色

        [HttpPost]
        [AuthorizationOperation(Name = "添加用户角色")]
        public ActionResult AddUserRole(long userId, IEnumerable<long> roleIds)
        {
            if (userId < 1)
            {
                return Json(Result.FailedResult("没有指定用户数据"));
            }
            if (roleIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定角色数据"));
            }
            ModifyUserRoleDto modifyUserRoleDto = new ModifyUserRoleDto()
            {
                Bindings = roleIds.Select(rid => new UserRoleDto()
                {
                    RoleId = rid,
                    UserId = userId
                })
            };
            return Json(userAppService.ModifyUserRole(modifyUserRoleDto));
        }

        #endregion

        #region 清除用户角色

        [HttpPost]
        [AuthorizationOperation(Name = "清除用户角色")]
        public ActionResult ClearUserRole(long userId)
        {
            var result = userAppService.ClearRole(new long[1] { userId });
            return Json(result);
        }

        #endregion 

        #endregion

        #region 账户授权

        #region 添加账户授权

        [HttpPost]
        [AuthorizationOperation(Name = "添加用户权限")]
        public ActionResult AddUserPermission(long userId, IEnumerable<long> permissionIds)
        {
            if (userId < 1)
            {
                return Json(Result.FailedResult("没有指定用户"));
            }
            if (permissionIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定权限"));
            }
            ModifyUserPermissionDto modifyUserPermissionDto = new ModifyUserPermissionDto()
            {
                UserPermissions = permissionIds.Select(pid => new UserPermissionDto()
                {
                    Disable = false,
                    PermissionId = pid,
                    UserId = userId
                })
            };
            return Json(userPermissionAppService.ModifyUserPermission(modifyUserPermissionDto));
        }

        #endregion

        #region 删除账户授权

        [HttpPost]
        [AuthorizationOperation(Name = "删除用户权限")]
        public ActionResult RemoveUserPermission(long userId, IEnumerable<long> permissionIds)
        {
            if (userId < 1)
            {
                return Json(Result.FailedResult("没有指定用户"));
            }
            if (permissionIds.IsNullOrEmpty())
            {
                return Json(Result.FailedResult("没有指定权限"));
            }
            ModifyUserPermissionDto modifyUserPermissionDto = new ModifyUserPermissionDto()
            {
                UserPermissions = permissionIds.Select(pid => new UserPermissionDto()
                {
                    Disable = true,
                    PermissionId = pid,
                    UserId = userId
                })
            };
            return Json(userPermissionAppService.ModifyUserPermission(modifyUserPermissionDto));
        }

        #endregion

        #region 还原用户角色授权

        [HttpPost]
        [AuthorizationOperation(Name = "清除用户权限")]
        public ActionResult RestoreUserRolePermission(long userId)
        {
            var result = userPermissionAppService.ClearUserPermission(new long[1] { userId });
            return Json(result);
        }

        #endregion

        #endregion
    }
}
