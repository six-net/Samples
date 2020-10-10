using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Response;
using EZNEW.Develop.UnitOfWork;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Repository;
using EZNEW.DependencyInjection;
using EZNEW.Domain.Sys.Parameter;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 用户角色绑定服务
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        static readonly IUserRoleRepository userRoleRepository = ContainerManager.Resolve<IUserRoleRepository>();

        #region 用户和角色的绑定

        /// <summary>
        /// 绑定用户角色
        /// </summary>
        /// <param name="userRoleBindings">用户角色绑定信息</param>
        /// <returns>返回操作结果</returns>
        public Result Bind(params UserRole[] userRoleBindings)
        {
            if (userRoleBindings.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要绑定的用户&角色信息");
            }
            var bindingInfos = userRoleBindings.Select(c => new Tuple<User, Role>(User.Create(c.UserId), Role.Create(c.RoleId)));
            userRoleRepository.Remove(bindingInfos, new ActivationOption()
            {
                ForceExecute = true
            });
            userRoleRepository.Save(bindingInfos);
            return Result.SuccessResult("绑定成功");
        }

        #endregion

        #region 用户角色解绑

        /// <summary>
        /// 用户角色解绑
        /// </summary>
        /// <param name="userRoleUnbindings">用户角色绑定信息</param>
        /// <returns>返回操作结果</returns>
        public Result Unbind(params UserRole[] userRoleUnbindings)
        {
            if (userRoleUnbindings.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要解绑任何信息");
            }
            userRoleRepository.Remove(userRoleUnbindings.Select(c => new Tuple<User, Role>(User.Create(c.UserId), Role.Create(c.RoleId))));
            return Result.SuccessResult("解绑成功");
        }

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色系统编号</param>
        /// <returns>返回操作结果</returns>
        public Result ClearByRole(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何角色信息");
            }
            userRoleRepository.RemoveBySecond(roleIds.Select(c => Role.Create(c)));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 清除用户绑定的所有角色

        /// <summary>
        /// 清除用户绑定的所有角色
        /// </summary>
        /// <param name="userIds">用户系统编号</param>
        /// <returns>返回操作结果</returns>
        public Result ClearByUser(IEnumerable<long> userIds)
        {
            if (userIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何用户信息");
            }
            userRoleRepository.RemoveByFirst(userIds.Select(c => User.Create(c)));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 修改用户&角色绑定关系

        /// <summary>
        /// 修改用户&角色绑定关系
        /// </summary>
        /// <param name="modifyUserRole">用户&角色修绑定关系修改信息</param>
        /// <returns>返回操作结果</returns>
        public Result Modify(ModifyUserRoleParameter modifyUserRole)
        {
            if (modifyUserRole == null || (modifyUserRole.Bindings.IsNullOrEmpty() && modifyUserRole.Unbindings.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何需要修改的用户&角色关系信息");
            }
            Result result = null;
            //绑定
            if (!modifyUserRole.Bindings.IsNullOrEmpty())
            {
                result = Bind(modifyUserRole.Bindings.ToArray());
                if (!result.Success)
                {
                    return result;
                }
            }
            //解绑
            if (!modifyUserRole.Unbindings.IsNullOrEmpty())
            {
                result = Unbind(modifyUserRole.Unbindings.ToArray());
            }
            return result ?? Result.FailedResult("用户&角色修改失败");
        }

        #endregion
    }
}
