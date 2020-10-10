using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService
    {
        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色编号</param>
        /// <returns>返回执行结果</returns>
        Result Remove(IEnumerable<long> ids);

        #endregion

        #region 获取指定用户绑定的角色

        /// <summary>
        /// 获取指定用户绑定的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户绑定的角色</returns>
        List<Role> GetUserRoles(long userId);

        #endregion

        #region 保存角色信息

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>返回角色保存执行结果</returns>
        Result<Role> Save(Role role);

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>角色信息</returns>
        Role Get(long id);

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色</returns>
        Role Get(RoleFilter roleFilter);

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回角色列表</returns>
        List<Role> GetList(IEnumerable<long> roleIds);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色列表</returns>
        List<Role> GetList(RoleFilter roleFilter);

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色列表</returns>
        IPaging<Role> GetPaging(RoleFilter roleFilter);

        #endregion

        #region 检查角色名称是否存在

        /// <summary>
        /// 检查角色名称是否存在
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="excludeId">需要排除的角色编号</param>
        /// <returns>返回角色名称是否存在 </returns>
        bool ExistName(string roleName, long excludeId);

        #endregion
    }
}
