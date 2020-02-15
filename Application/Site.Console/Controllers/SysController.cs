using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Application.Identity.Auth;
using EZNEW.Application.Identity.User;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.AppServiceContract.Sys;
using EZNEW.Framework.Extension;
using EZNEW.Framework.Paging;
using EZNEW.Framework.Response;
using EZNEW.Framework.Serialize;
using EZNEW.ViewModel.Common;
using EZNEW.ViewModel.Sys.Filter;
using EZNEW.ViewModel.Sys.Request;
using EZNEW.ViewModel.Sys.Response;
using EZNEW.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using EZNEW.Framework.Code;

namespace Site.Console.Controllers
{
    public class SysController : WebBaseController
    {
        IUserAppService userService = null;
        IRoleAppService roleService = null;
        IAuthAppService authService = null;

        public SysController(IUserAppService userService, IRoleAppService roleService, IAuthAppService authService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.authService = authService;
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

        #region 角色管理

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

        #endregion

        #region 管理用户管理

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

        #endregion

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