using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Module.Sys;
using EZNEW.Develop.UnitOfWork;
using EZNEW.DependencyInjection;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public class AuthorizeService : IAuthorizeService
    {
        static IRoleAuthorizeRepository roleAuthRepository = ContainerManager.Resolve<IRoleAuthorizeRepository>();
        static IUserAuthorizeRepository userAuthRepository = ContainerManager.Resolve<IUserAuthorizeRepository>();
        static IAuthorityRepository authorityRepository = ContainerManager.Resolve<IAuthorityRepository>();
        static IAuthorityService authorityService = ContainerManager.Resolve<IAuthorityService>();
        static IUserService userService = ContainerManager.Resolve<IUserService>();
        static IAuthorityOperationService authorityOperationService = ContainerManager.Resolve<IAuthorityOperationService>();

        #region 角色授权

        #region 修改角色授权信息

        /// <summary>
        /// 修改角色授权信息
        /// </summary>
        /// <param name="roleAuthorizes">角色授权信息</param>
        /// <returns></returns>
        public Result ModifyRoleAuthorize(ModifyRoleAuthorize roleAuthorizes)
        {
            if (roleAuthorizes == null || (roleAuthorizes.Binds.IsNullOrEmpty() && roleAuthorizes.UnBinds.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何要修改的绑定信息");
            }
            //解绑
            if (!roleAuthorizes.UnBinds.IsNullOrEmpty())
            {
                roleAuthRepository.Remove(roleAuthorizes.UnBinds);
            }
            //绑定
            if (!roleAuthorizes.Binds.IsNullOrEmpty())
            {
                roleAuthRepository.Remove(roleAuthorizes.Binds, new ActivationOption()
                {
                    ForceExecute = true
                });
                roleAuthRepository.Save(roleAuthorizes.Binds);
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 清除角色授权

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleSysNos">角色系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearRoleAuthorize(IEnumerable<long> roleSysNos)
        {
            if (roleSysNos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何角色信息");
            }
            roleAuthRepository.RemoveByFirst(roleSysNos.Select(c => Role.CreateRole(c)));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #endregion

        #region 用户授权

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="userAuthorizes">用户授权信息</param>
        /// <returns></returns>
        public Result ModifyUserAuthorize(IEnumerable<UserAuthorize> userAuthorizes)
        {
            if (userAuthorizes.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要修改的用户授权信息");
            }

            #region 角色授权

            //用户绑定角色
            List<long> userIds = userAuthorizes.Select(c => c.User?.SysNo ?? 0).Distinct().ToList();
            IQuery userRoleBindQuery = QueryManager.Create<UserRoleQuery>(c => userIds.Contains(c.UserSysNo));

            //角色授权
            IQuery roleAuthBindQuery = QueryManager.Create<RoleAuthorizeQuery>();
            roleAuthBindQuery.EqualInnerJoin(userRoleBindQuery);

            List<long> roleAuthorityIds = roleAuthRepository.GetList(roleAuthBindQuery).Select(c => c.Item2.SysNo).ToList();

            #endregion

            //移除当前存在的授权数据
            userAuthRepository.Remove(userAuthorizes, new ActivationOption()
            {
                ForceExecute = true
            });
            var saveUserAuthorizes = new List<UserAuthorize>();
            //角色拥有但是用户显示禁用掉的授权
            var disableAuthorizes = userAuthorizes.Where(c => c.Disable && roleAuthorityIds.Contains(c.Authority.SysNo)).ToList();
            if (!disableAuthorizes.IsNullOrEmpty())
            {
                saveUserAuthorizes.AddRange(disableAuthorizes);
            }
            //用户单独授权的权限
            var enableAuthorizes = userAuthorizes.Where(c => !c.Disable && !roleAuthorityIds.Contains(c.Authority.SysNo)).ToList();
            if (!enableAuthorizes.IsNullOrEmpty())
            {
                saveUserAuthorizes.AddRange(enableAuthorizes);
            }
            if (!saveUserAuthorizes.IsNullOrEmpty())
            {
                userAuthRepository.Save(saveUserAuthorizes.ToArray());
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 清除用户授权

        /// <summary>
        /// 清除用户授权
        /// </summary>
        /// <param name="userSysNos">用户系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearUserAuthorize(IEnumerable<long> userSysNos)
        {
            if (userSysNos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何用户信息");
            }
            userAuthRepository.RemoveByFirst(userSysNos.Select(c => User.CreateUser(c)));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #endregion

        #region 授权验证

        /// <summary>
        /// 用户授权验证
        /// </summary>
        /// <param name="auth">授权验证信息</param>
        /// <returns></returns>
        public bool Authentication(Authentication auth)
        {
            if (auth == null || auth.User == null || auth.Operation == null)
            {
                return false;
            }

            #region 用户信息验证

            User nowUser = userService.GetUser(auth.User.SysNo);//当前用户
            if (nowUser == null)
            {
                return false;
            }
            if (nowUser.SuperUser)
            {
                return true;//超级用户不受权限控制
            }

            #endregion

            #region 授权操作信息验证

            AuthorityOperation nowOperation = authorityOperationService.GetAuthorityOperation(auth.Operation.ControllerCode, auth.Operation.ActionCode);//授权操作信息
            if (nowOperation == null || nowOperation.Status == AuthorityOperationStatus.关闭)
            {
                return false;
            }
            if (nowOperation.AuthorizeType == AuthorityOperationAuthorizeType.无限制)
            {
                return true;
            }

            #endregion

            #region 授权验证

            //权限
            IQuery authorityQuery = QueryManager.Create<AuthorityQuery>(a => a.Status == AuthorityStatus.启用);
            authorityQuery.AddQueryFields<AuthorityQuery>(a => a.Code);
            //操作绑定权限
            IQuery operationBindQuery = QueryManager.Create<AuthorityBindOperationQuery>(a => a.AuthorityOperationSysNo == nowOperation.SysNo);
            operationBindQuery.AddQueryFields<AuthorityBindOperationQuery>(a => a.AuthoritySysNo);
            authorityQuery.And<AuthorityQuery>(a => a.Code, CriteriaOperator.In, operationBindQuery);
            //当前用户可以使用
            IQuery userAuthorizeQuery = QueryManager.Create<UserAuthorizeQuery>(a => a.UserSysNo == auth.User.SysNo && a.Disable == false);
            userAuthorizeQuery.AddQueryFields<UserAuthorizeQuery>(a => a.AuthoritySysNo);
            //用户角色
            IQuery userRoleQuery = QueryManager.Create<UserRoleQuery>(a => a.UserSysNo == auth.User.SysNo);
            userRoleQuery.AddQueryFields<UserRoleQuery>(r => r.RoleSysNo);
            //角色权限
            IQuery roleAuthorizeQuery = QueryManager.Create<RoleAuthorizeQuery>();
            roleAuthorizeQuery.AddQueryFields<RoleAuthorizeQuery>(a => a.AuthoritySysNo);
            roleAuthorizeQuery.And<RoleAuthorizeQuery>(a => a.RoleSysNo, CriteriaOperator.In, userRoleQuery);
            //用户或用户角色拥有权限
            IQuery userAndRoleAuthorityQuery = QueryManager.Create();
            userAndRoleAuthorityQuery.And<AuthorityQuery>(a => a.Code, CriteriaOperator.In, userAuthorizeQuery);//用户拥有权限
            userAndRoleAuthorityQuery.Or<AuthorityQuery>(a => a.Code, CriteriaOperator.In, roleAuthorizeQuery);//或者角色拥有权限
            authorityQuery.And(userAndRoleAuthorityQuery);
            //去除用户禁用的
            IQuery userDisableAuthorizeQuery = QueryManager.Create<UserAuthorizeQuery>(a => a.UserSysNo == auth.User.SysNo && a.Disable == true);
            userDisableAuthorizeQuery.AddQueryFields<UserAuthorizeQuery>(a => a.AuthoritySysNo);
            authorityQuery.And<AuthorityQuery>(a => a.Code, CriteriaOperator.NotIn, userDisableAuthorizeQuery);
            return authorityRepository.Exist(authorityQuery);

            #endregion
        }

        #endregion
    }
}
