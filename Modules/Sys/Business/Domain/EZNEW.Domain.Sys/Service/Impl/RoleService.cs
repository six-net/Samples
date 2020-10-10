using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Repository;
using EZNEW.DependencyInjection;
using EZNEW.Paging;
using EZNEW.Response;
using EZNEW.Domain.Sys.Model;
using EZNEW.Entity.Sys;
using EZNEW.Domain.Sys.Parameter.Filter;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService : IRoleService
    {
        static readonly IRoleRepository roleRepository = ContainerManager.Resolve<IRoleRepository>();

        #region 保存角色信息

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public Result<Role> Save(Role role)
        {
            return role?.Save() ?? Result<Role>.FailedResult("角色保存失败");
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回删除角色执行结果</returns>
        public Result Remove(IEnumerable<long> roleIds)
        {
            #region 参数判断

            if (roleIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定要删除的角色");
            }

            #endregion

            //删除角色信息
            IQuery removeQuery = QueryManager.Create<RoleEntity>(c => roleIds.Contains(c.Id));
            roleRepository.Remove(removeQuery);
            return Result.SuccessResult("角色移除成功");
        }

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        Role GetRole(IQuery query)
        {
            var role = roleRepository.Get(query);
            return role;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>角色信息</returns>
        public Role Get(long id)
        {
            if (id <= 0)
            {
                return null;
            }
            IQuery query = QueryManager.Create<RoleEntity>(c => c.Id == id);
            return GetRole(query);
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色</returns>
        public Role Get(RoleFilter roleFilter)
        {
            return GetRole(roleFilter?.CreateQuery());
        }

        #endregion

        #region 获取指定用户绑定的角色

        /// <summary>
        /// 获取指定用户绑定的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>返回用户绑定的角色</returns>
        public List<Role> GetUserRoles(long userId)
        {
            if (userId < 1)
            {
                return new List<Role>(0);
            }
            var userRoleQuery = new UserRoleFilter()
            {
                UserFilter = new UserFilter()
                {
                    Ids = new List<long>() { userId }
                }
            }.CreateQuery();
            return GetList(userRoleQuery);
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        List<Role> GetList(IQuery query)
        {
            var roleList = roleRepository.GetList(query);
            return roleList;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns></returns>
        public List<Role> GetList(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return new List<Role>(0);
            }
            IQuery query = QueryManager.Create<RoleEntity>(c => roleIds.Contains(c.Id));
            return GetList(query);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>获取角色列表</returns>
        public List<Role> GetList(RoleFilter roleFilter)
        {
            return GetList(roleFilter?.CreateQuery());
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回角色分页</returns>
        IPaging<Role> GetPaging(IQuery query)
        {
            var rolePaging = roleRepository.GetPaging(query);
            return rolePaging;
        }

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="roleFilter">角色筛选信息</param>
        /// <returns>返回角色分页</returns>
        public IPaging<Role> GetPaging(RoleFilter roleFilter)
        {
            return GetPaging(roleFilter?.CreateQuery());
        }

        #endregion

        #region 检查角色名称是否存在

        /// <summary>
        /// 检查角色名称是否存在
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="excludeId">执行在检查角色名称的时候排除的角色编号</param>
        /// <returns>返回角色名称是否存在</returns>
        public bool ExistName(string roleName, long excludeId)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return false;
            }
            IQuery query = QueryManager.Create<RoleEntity>(c => c.Name == roleName && c.Id != excludeId);
            return roleRepository.Exist(query);
        }

        #endregion
    }
}
