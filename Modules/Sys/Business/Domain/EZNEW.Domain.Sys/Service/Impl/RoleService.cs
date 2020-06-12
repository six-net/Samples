using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Domain.Sys.Repository;
using EZNEW.DependencyInjection;
using EZNEW.Response;
using EZNEW.Paging;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService : IRoleService
    {
        static IRoleRepository roleRepository = ContainerManager.Resolve<IRoleRepository>();

        #region 批量删除角色

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns></returns>
        public void RemoveRole(IEnumerable<long> roleIds)
        {
            #region 参数判断

            if (roleIds.IsNullOrEmpty())
            {
                return;
            }

            #endregion

            //删除角色信息
            IQuery removeQuery = QueryManager.Create<RoleQuery>(c => roleIds.Contains(c.SysNo));
            removeQuery.SetRecurve<RoleQuery>(c => c.SysNo, c => c.Parent);//删除角色所有的下级数据
            roleRepository.Remove(removeQuery);
        }

        #endregion

        #region 获取指定用户绑定的角色

        /// <summary>
        /// 获取指定用户绑定的角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>绑定的角色</returns>
        public List<Role> GetUserBindRole(long userId)
        {
            if (userId <= 0)
            {
                return new List<Role>(0);
            }
            return roleRepository.GetUserBindRole(userId);
        }

        #endregion

        #region 获取用户可用的所有角色

        /// <summary>
        /// 获取用户可用的所有角色
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public List<Role> GetUserAllRoles(long userId)
        {
            if (userId < 1)
            {
                return new List<Role>(0);
            }
            var roleQuery = QueryManager.Create<RoleQuery>();
            //用户绑定的角色
            var userRoleQuery = QueryManager.Create<UserRoleQuery>(ur => ur.UserSysNo == userId);
            roleQuery.EqualInnerJoin(userRoleQuery);
            //所有上级角色
            roleQuery.SetRecurve<RoleQuery>(r => r.SysNo, r => r.Parent, RecurveDirection.Up);
            return roleRepository.GetList(roleQuery);
        }

        #endregion

        #region 保存角色信息

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public Result<Role> SaveRole(Role role)
        {
            if (role == null)
            {
                return Result<Role>.FailedResult("没有指定要保存的");
            }
            if (role.SysNo <= 0)
            {
                return AddRole(role);
            }
            else
            {
                return EditRole(role);
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色对象</param>
        /// <returns>执行结果</returns>
        static Result<Role> AddRole(Role role)
        {
            #region 参数判断

            if (role == null)
            {
                return Result<Role>.FailedResult("没有指定要添加的角色数据");
            }

            #endregion

            #region 上级

            long parentRoleId = role.Parent == null ? 0 : role.Parent.SysNo;
            Role parentRole = null;
            if (parentRoleId > 0)
            {
                IQuery parentQuery = QueryManager.Create<RoleQuery>(c => c.SysNo == parentRoleId);
                parentRole = roleRepository.Get(parentQuery);
                if (parentRole == null)
                {
                    return Result<Role>.FailedResult("请选择正确的上级角色");
                }
            }
            role.SetParentRole(parentRole);

            #endregion

            role.CreateDate = DateTime.Now;
            role.Save();
            var result = Result<Role>.SuccessResult("添加成功");
            result.Data = role;
            return result;
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="newRole">角色对象</param>
        /// <returns>执行结果</returns>
        static Result<Role> EditRole(Role newRole)
        {
            #region 参数判断

            if (newRole == null)
            {
                return Result<Role>.FailedResult("没有指定要操作的角色信息");
            }

            #endregion

            IQuery roleQuery = QueryManager.Create<RoleQuery>(r => r.SysNo == newRole.SysNo);
            Role role = roleRepository.Get(roleQuery);
            if (role == null)
            {
                return Result<Role>.FailedResult("没有指定要操作的角色信息");
            }

            #region 修改上级

            long newParentRoleId = newRole.Parent == null ? 0 : newRole.Parent.SysNo;
            long oldParentRoleId = role.Parent == null ? 0 : role.Parent.SysNo;
            //上级改变后 
            if (newParentRoleId != oldParentRoleId)
            {
                Role parentRole = null;
                if (newParentRoleId > 0)
                {
                    IQuery parentQuery = QueryManager.Create<RoleQuery>(c => c.SysNo == newParentRoleId);
                    parentRole = roleRepository.Get(parentQuery);
                    if (parentRole == null)
                    {
                        return Result<Role>.FailedResult("请选择正确的上级角色");
                    }
                }
                role.SetParentRole(parentRole);
            }

            #endregion

            //修改信息
            role.Name = newRole.Name;
            role.Status = newRole.Status;
            role.Remark = newRole.Remark ?? string.Empty;
            role.Save();//保存角色信息
            var result = Result<Role>.SuccessResult("修改成功");
            result.Data = role;
            return result;
        }

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public Role GetRole(IQuery query)
        {
            var role = roleRepository.Get(query);
            return role;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns>角色信息</returns>
        public Role GetRole(long id)
        {
            if (id <= 0)
            {
                return null;
            }
            IQuery query = QueryManager.Create<RoleQuery>(c => c.SysNo == id);
            return GetRole(query);
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public List<Role> GetRoleList(IQuery query)
        {
            var roleList = roleRepository.GetList(query);
            return roleList;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns></returns>
        public List<Role> GetRoleList(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return new List<Role>(0);
            }
            IQuery query = QueryManager.Create<RoleQuery>(c => roleIds.Contains(c.SysNo));
            return GetRoleList(query);
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取Role分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public IPaging<Role> GetRolePaging(IQuery query)
        {
            var rolePaging = roleRepository.GetPaging(query);
            return rolePaging;
        }

        #endregion

        #region 修改角色排序

        /// <summary>
        /// 修改角色排序
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="newSort">新的排序</param>
        /// <returns></returns>
        public Result ModifyRoleSort(long roleId, int newSort)
        {
            #region 参数判断

            if (roleId <= 0)
            {
                return Result.FailedResult("没有指定要修改的角色");
            }

            #endregion

            Role role = GetRole(roleId);
            if (role == null)
            {
                return Result.FailedResult("没有指定要修改的角色");
            }
            role.ModifySort(newSort);
            role.Save();
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 检查角色名称是否存在

        /// <summary>
        /// 检查角色名称是否存在
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <param name="excludeRoleId">除指定的角色之外</param>
        /// <returns></returns>
        public bool ExistRoleName(string roleName, long excludeRoleId)
        {
            if (roleName.IsNullOrEmpty())
            {
                return false;
            }
            IQuery query = QueryManager.Create<RoleQuery>(c => c.Name == roleName && c.SysNo != excludeRoleId);
            return roleRepository.Exist(query);
        }

        #endregion
    }
}
