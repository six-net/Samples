using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.UnitOfWork;
using EZNEW.DependencyInjection;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 用户角色绑定服务
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        static IUserRoleRepository userRoleRepository = ContainerManager.Container.Resolve<IUserRoleRepository>();

        #region 用户和角色的绑定

        /// <summary>
        /// 绑定用户角色
        /// </summary>
        /// <param name="userRoleBinds">用户角色绑定信息</param>
        /// <returns></returns>
        public Result BindUserAndRole(params Tuple<User, Role>[] userRoleBinds)
        {
            if (userRoleBinds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要绑定的信息");
            }
            userRoleRepository.Remove(userRoleBinds, new ActivationOption()
            {
                ForceExecute = true
            });
            userRoleRepository.Save(userRoleBinds);
            return Result.SuccessResult("绑定成功");
        }

        #endregion

        #region 用户角色解绑

        /// <summary>
        /// 用户角色解绑
        /// </summary>
        /// <param name="userRoleBinds">用户角色绑定信息</param>
        /// <returns></returns>
        public Result UnBindUserAndRole(params Tuple<User, Role>[] userRoleBinds)
        {
            if (userRoleBinds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要解绑任何信息");
            }
            userRoleRepository.Remove(userRoleBinds);
            return Result.SuccessResult("解绑成功");
        }

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleSysNos">角色系统编号</param>
        /// <returns></returns>
        public Result ClearRoleUser(IEnumerable<long> roleSysNos)
        {
            if (roleSysNos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何角色信息");
            }
            userRoleRepository.RemoveBySecond(roleSysNos.Select(c => Role.CreateRole(c)));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 清除用户绑定的所有角色

        /// <summary>
        /// 清除用户绑定的所有角色
        /// </summary>
        /// <param name="userSysNos">用户系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearUserRole(IEnumerable<long> userSysNos)
        {
            if (userSysNos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何用户信息");
            }
            userRoleRepository.RemoveByFirst(userSysNos.Select(c => User.CreateUser(c)));
            return Result.SuccessResult("修改成功");
        }

        #endregion
    }
}
