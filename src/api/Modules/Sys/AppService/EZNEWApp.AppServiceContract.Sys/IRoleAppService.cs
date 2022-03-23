using System.Collections.Generic;
using EZNEW.Model;
using EZNEW.Paging;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Parameter.Filter;

namespace EZNEWApp.AppServiceContract.Sys
{
    /// <summary>
    /// 角色业务接口
    /// </summary>
    public interface IRoleAppService
    {
        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveRoleParameter">角色保存信息</param>
        /// <returns></returns>
        Result<Role> SaveRole(SaveRoleParameter saveRoleParameter);

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色信息</returns>
        Role GetRole(RoleFilter roleFilter);

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="deleteRoleParameter">角色删除信息</param>
        /// <returns>返回删除角色执行结果</returns>
        Result RemoveRole(RemoveRoleParameter deleteRoleParameter);

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色列表</returns>
        List<Role> GetRoleList(RoleFilter roleFilter);

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色分页</returns>
        PagingInfo<Role> GetRolePaging(RoleFilter roleFilter);

        #endregion

        #region 验证角色名称是否存在

        /// <summary>
        /// 验证角色名称是否存在
        /// </summary>
        /// <param name="existRoleNameParameter">角色名称验证信息</param>
        /// <returns>返回角色名称是否存在</returns>
        bool ExistRoleName(ExistRoleNameParameter existRoleNameParameter);

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回执行结果</returns>
        Result CleanUser(IEnumerable<long> roleIds);

        #endregion
    }
}
