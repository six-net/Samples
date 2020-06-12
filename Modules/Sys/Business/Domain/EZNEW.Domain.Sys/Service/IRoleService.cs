using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Paging;
using EZNEW.Response;
using System.Collections.Generic;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService
    {
        #region 批量删除角色

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns></returns>
        void RemoveRole(IEnumerable<long> roleIds);

        #endregion

        #region 获取指定用户绑定的角色

        /// <summary>
        /// 获取指定用户绑定的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>绑定的角色</returns>
        List<Role> GetUserBindRole(long userId);

        #endregion

        #region 获取用户可用的所有角色

        /// <summary>
        /// 获取用户可用的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        List<Role> GetUserAllRoles(long userId);

        #endregion

        #region 保存角色信息

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        Result<Role> SaveRole(Role role);

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        Role GetRole(IQuery query);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>角色信息</returns>
        Role GetRole(long id);

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        List<Role> GetRoleList(IQuery query);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns></returns>
        List<Role> GetRoleList(IEnumerable<long> roleIds);

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取Role分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<Role> GetRolePaging(IQuery query);

        #endregion

        #region 修改角色排序

        /// <summary>
        /// 修改角色排序
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="newSort">新的排序</param>
        /// <returns></returns>
        Result ModifyRoleSort(long roleId, int newSort);

        #endregion

        #region 检查角色名称是否存在

        /// <summary>
        /// 检查角色名称是否存在
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="excludeRoleId">除指定的角色之外</param>
        /// <returns></returns>
        bool ExistRoleName(string roleName, long excludeRoleId);

        #endregion
    }
}
