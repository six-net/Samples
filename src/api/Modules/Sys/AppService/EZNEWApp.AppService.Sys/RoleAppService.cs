using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.AppServiceContract.Sys;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 角色业务
    /// </summary>
    public class RoleAppService : IRoleAppService
    {
        IRoleBusiness roleBusiness;

        public RoleAppService(IRoleBusiness roleBusiness)
        {
            this.roleBusiness = roleBusiness;
        }

        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveRoleParameter">角色保存信息</param>
        /// <returns>返回角色保存执行结果</returns>
        public Result<Role> SaveRole(SaveRoleParameter saveRoleParameter)
        {
            return roleBusiness.SaveRole(saveRoleParameter);
        }

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns></returns>
        public Role GetRole(RoleFilter roleFilter)
        {
            return roleBusiness.GetRole(roleFilter);
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="removeRoleParameter">删除角色信息</param>
        /// <returns>返回删除角色执行结果</returns>
        public Result RemoveRole(RemoveRoleParameter removeRoleParameter)
        {
            return roleBusiness.RemoveRole(removeRoleParameter);
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleFilterParameter">角色筛选信息</param>
        /// <returns>返回角色列表</returns>
        public List<Role> GetRoleList(RoleFilter roleFilterParameter)
        {
            return roleBusiness.GetRoleList(roleFilterParameter);
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="roleFilterParameter">角色筛选信息</param>
        /// <returns>返回角色分页</returns>
        public PagingInfo<Role> GetRolePaging(RoleFilter roleFilterParameter)
        {
            return roleBusiness.GetRolePaging(roleFilterParameter);
        }

        #endregion

        #region 验证角色名称是否存在

        /// <summary>
        /// 验证角色名称是否存在
        /// </summary>
        /// <param name="existRoleNameParameter">角色名称验证信息</param>
        /// <returns>返回角色名称是否存在</returns>
        public bool ExistRoleName(ExistRoleNameParameter existRoleNameParameter)
        {
            return roleBusiness.ExistRoleName(existRoleNameParameter);
        }

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回清除用户执行结果</returns>
        public Result CleanUser(IEnumerable<long> roleIds)
        {
            return roleBusiness.ClearUser(roleIds);
        }

        #endregion
    }
}
