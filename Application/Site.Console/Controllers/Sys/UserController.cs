using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Application.Identity.User;
using EZNEW.AppServiceContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Framework.Extension;
using EZNEW.Framework.Paging;
using EZNEW.Framework.Response;
using EZNEW.ViewModel.Sys.Filter;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.ViewModel.Sys.Response;
using EZNEW.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Site.Console.Controllers.Sys
{
    public class UserController : WebBaseController
    {
        IUserAppService userService = null;
        IAuthAppService authService = null;

        public UserController(IUserAppService userAppService, IAuthAppService authAppService)
        {
            userService = userAppService;
            authService = authAppService;
        }

        #region 修改密码

        public IActionResult ModifyPassword(ModifyPasswordViewModel modifyInfo)
        {
            if (IsPost)
            {
                if (!ModelState.IsValid)
                {
                    return Json(Result.FailedResult("提交数据有错误"));
                }
                var modifyInfoDto = modifyInfo.MapTo<ModifyPasswordCmdDto>();
                modifyInfoDto.CheckOldPassword = true;
                modifyInfoDto.SysNo = User.Id;
                var result = AjaxResult.CopyFromResult(userService.ModifyPassword(modifyInfoDto));
                result.SuccessClose = true;
                return Json(result);
            }
            return View("ModifyPassword");
        }

        #endregion

        #region 管理用户列表

        public ActionResult AdminUserList()
        {
            return View();
        }

        #endregion

        #region 查询管理用户数据

        [HttpPost]
        public ActionResult SearchAdminUser(AdminUserFilterViewModel filter)
        {
            filter.UserType = UserType.管理账户;
            IPaging<UserViewModel> userPager = userService.GetUserPaging(filter.MapTo<AdminUserFilterDto>()).ConvertTo<UserViewModel>();
            object objResult = new
            {
                userPager.TotalCount,
                Datas = userPager.ToList()
            };
            return Json(objResult);
        }

        #endregion

        #region 管理用户详情

        public ActionResult AdminUserDetail(long id)
        {
            #region 用户信息

            UserViewModel user = null;
            if (id > 0)
            {
                AdminUserFilterDto filter = new AdminUserFilterDto()
                {
                    SysNos = new List<long>()
                    {
                        id
                    },
                };
                user = userService.GetUser(filter).MapTo<UserViewModel>();
            }
            if (user == null || !(user is AdminUserViewModel))
            {
                return Content("没有找到用户信息");
            }
            var adminUser = user as AdminUserViewModel;

            #endregion

            return View(adminUser);
        }

        #endregion

        #region 编辑/添加管理用户

        public ActionResult EditUser(EditUserViewModel user)
        {
            if (IsPost)
            {
                user.UserType = UserType.管理账户;
                AdminUserCmdDto adminUser = user.MapTo<AdminUserCmdDto>();
                SaveUserCmdDto saveInfo = new SaveUserCmdDto()
                {
                    User = adminUser
                };
                var result = userService.SaveUser(saveInfo);
                return Json(result);
            }
            else if (user.SysNo > 0)
            {
                AdminUserFilterDto filter = new AdminUserFilterDto()
                {
                    SysNos = new List<long>()
                    {
                        user.SysNo
                    }
                };
                user = userService.GetUser(filter).MapTo<EditUserViewModel>();
            }
            return View(user);
        }

        #endregion

        #region 删除管理用户

        public ActionResult DeleteUser(List<long> sysNos)
        {
            Result result = userService.DeleteUser(new DeleteUserCmdDto()
            {
                UserIds = sysNos
            });
            return Json(result);
        }

        #endregion

        #region 验证登陆名是否存在

        [HttpPost]
        public ActionResult CheckUserName(string userName)
        {
            bool allowUse = true;
            if (!userName.IsNullOrEmpty())
            {
                UserFilterDto filter = new UserFilterDto()
                {
                    UserName = userName
                };
                var user = userService.GetUser(filter);
                allowUse = user == null;
            }
            return Content(allowUse ? "true" : "false");
        }

        #endregion

        #region 修改用户状态

        [HttpPost]
        public ActionResult ModifyUserStatus(long id, UserStatus status)
        {
            ModifyUserStatusCmdDto statusInfo = new ModifyUserStatusCmdDto()
            {
                Status = status,
                UserId = id
            };
            return Json(userService.ModifyStatus(statusInfo));
        }

        #endregion

        #region 修改密码

        public ActionResult AdminModifyPassword(ModifyPasswordViewModel modifyInfo)
        {
            if (IsPost)
            {
                ModelState.Remove("NowPassword");
                if (!ModelState.IsValid)
                {
                    return Json(Result.FailedResult("提交数据有错误"));
                }
                var modifyInfoDto = modifyInfo.MapTo<ModifyPasswordCmdDto>();
                modifyInfoDto.CheckOldPassword = false;
                var modifyResult = userService.ModifyPassword(modifyInfoDto);
                var result = AjaxResult.CopyFromResult(modifyResult);
                result.SuccessClose = true;
                return Json(result);
            }
            return View(modifyInfo);
        }

        #endregion

        #region 管理用户多选

        public ActionResult AdminUserMultiSelect()
        {
            return View();
        }

        #endregion

        #region 管理用户&角色

        #region 移除管理用户角色

        [HttpPost]
        public ActionResult RemoveUserRole(long userId, IEnumerable<long> roleIds)
        {
            ModifyUserBindRoleCmdDto bindInfo = new ModifyUserBindRoleCmdDto()
            {
                UnBinds = roleIds.Select(c => new Tuple<UserCmdDto, RoleCmdDto>(new UserCmdDto()
                {
                    SysNo = userId
                }, new RoleCmdDto()
                {
                    SysNo = c
                }))
            };
            return Json(userService.ModifyUserBindRole(bindInfo));
        }

        #endregion

        #region 添加管理用户角色

        [HttpPost]
        public ActionResult AddUserRole(long userId, IEnumerable<long> roleIds)
        {
            ModifyUserBindRoleCmdDto bindInfo = new ModifyUserBindRoleCmdDto()
            {
                Binds = roleIds.Select(c => new Tuple<UserCmdDto, RoleCmdDto>(new UserCmdDto()
                {
                    SysNo = userId
                }, new RoleCmdDto()
                {
                    SysNo = c
                }))
            };
            return Json(userService.ModifyUserBindRole(bindInfo));
        }

        #endregion

        #region 清除管理用户角色

        [HttpPost]
        public ActionResult ClearUserRole(long userId)
        {
            var result = userService.ClearUserRole(new long[1] { userId });
            return Json(result);
        }

        #endregion 

        #endregion

        #region 管理账户授权

        #region 添加管理账户授权

        [HttpPost]
        public ActionResult AddUserAuthorize(long userId, IEnumerable<long> authSysNos)
        {
            List<UserAuthorizeCmdDto> userAuthorizeList = new List<UserAuthorizeCmdDto>();
            var user = new AdminUserCmdDto()
            {
                SysNo = userId,
                UserType = UserType.管理账户
            };
            if (!authSysNos.IsNullOrEmpty())
            {
                userAuthorizeList.AddRange(authSysNos?.Select(c => new UserAuthorizeCmdDto()
                {
                    Disable = false,
                    Authority = new AuthorityCmdDto()
                    {
                        SysNo = c
                    },
                    User = user
                }));
            }
            ModifyUserAuthorizeCmdDto userAuthInfo = new ModifyUserAuthorizeCmdDto()
            {
                UserAuthorizes = userAuthorizeList
            };
            return Json(authService.ModifyUserAuthorize(userAuthInfo));
        }

        #endregion

        #region 删除管理账户授权

        [HttpPost]
        public ActionResult RemoveUserAuthorize(long userId, IEnumerable<long> authSysNos)
        {
            List<UserAuthorizeCmdDto> userAuthorizeList = new List<UserAuthorizeCmdDto>();
            var user = new AdminUserCmdDto()
            {
                SysNo = userId,
                UserType = UserType.管理账户
            };
            if (!authSysNos.IsNullOrEmpty())
            {
                userAuthorizeList.AddRange(authSysNos?.Select(c => new UserAuthorizeCmdDto()
                {
                    Disable = true,
                    Authority = new AuthorityCmdDto()
                    {
                        SysNo = c
                    },
                    User = user
                }));
            }
            ModifyUserAuthorizeCmdDto userAuthInfo = new ModifyUserAuthorizeCmdDto()
            {
                UserAuthorizes = userAuthorizeList
            };
            return Json(authService.ModifyUserAuthorize(userAuthInfo));
        }

        #endregion

        #region 还原管理用户角色授权

        [HttpPost]
        public ActionResult RestoreUserRoleAuthorize(long userId)
        {
            var result = authService.ClearUserAuthorize(new long[1] { userId });
            return Json(result);
        }

        #endregion

        #endregion
    }
}