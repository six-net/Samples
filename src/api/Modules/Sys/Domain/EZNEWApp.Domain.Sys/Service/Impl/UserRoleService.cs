using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEW.Development.Domain.Repository;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 用户角色绑定服务
    /// </summary>
    public class UserRoleService : IUserRoleService
    {
        readonly IRepository<UserRole> userRoleRepository;

        public UserRoleService(IRepository<UserRole> userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

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
            userRoleRepository.RemoveByRelationData(roleIds.Select(c => new Role() { Id = c }));
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
            userRoleRepository.RemoveByRelationData(userIds.Select(c => new User() { Id = c }));
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 修改用户&角色绑定关系

        /// <summary>
        /// 修改用户&角色绑定关系
        /// </summary>
        /// <param name="modifyUserRoleParameter">用户&角色修绑定关系修改信息</param>
        /// <returns>返回操作结果</returns>
        public Result Modify(ModifyUserRoleParameter modifyUserRoleParameter)
        {
            if (modifyUserRoleParameter == null || (modifyUserRoleParameter.Bindings.IsNullOrEmpty() && modifyUserRoleParameter.Unbindings.IsNullOrEmpty()))
            {
                return Result.FailedResult("没有指定任何需要修改的用户&角色关系信息");
            }
            //绑定
            if (!modifyUserRoleParameter.Bindings.IsNullOrEmpty())
            {
                userRoleRepository.Save(modifyUserRoleParameter.Bindings);
            }
            //解绑
            if (!modifyUserRoleParameter.Unbindings.IsNullOrEmpty())
            {
                userRoleRepository.Remove(modifyUserRoleParameter.Unbindings);
            }
            return Result.SuccessResult("修改成功");
        }

        #endregion
    }
}
