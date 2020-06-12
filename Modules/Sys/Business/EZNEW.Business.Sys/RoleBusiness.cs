using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.UnitOfWork;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.BusinessContract.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Service;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Query.Sys;
using EZNEW.DependencyInjection;
using EZNEW.Response;
using EZNEW.Paging;

namespace EZNEW.Business.Sys
{
    /// <summary>
    /// 角色业务
    /// </summary>
    public class RoleBusiness : IRoleBusiness
    {
        static IRoleService roleService = ContainerManager.Resolve<IRoleService>();
        static IUserRoleService userRoleService = ContainerManager.Resolve<IUserRoleService>();

        public RoleBusiness()
        {
        }

        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveInfo">角色保存信息</param>
        /// <returns>执行结果</returns>
        public Result<RoleDto> SaveRole(SaveRoleCmdDto saveInfo)
        {
            if (saveInfo == null)
            {
                return Result<RoleDto>.FailedResult("没有指定任何要保存的信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var roleResult = roleService.SaveRole(saveInfo.Role.MapTo<Role>());
                if (!roleResult.Success)
                {
                    return Result<RoleDto>.FailedResult(roleResult.Message);
                }
                var commitResult = businessWork.Commit();
                Result<RoleDto> result = null;
                if (commitResult.EmptyResultOrSuccess)
                {
                    result = Result<RoleDto>.SuccessResult("保存成功");
                    result.Data = roleResult.Data.MapTo<RoleDto>();
                }
                else
                {
                    result = Result<RoleDto>.FailedResult("保存失败");
                }
                return result;
            }
        }

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public RoleDto GetRole(RoleFilterDto filter)
        {
            var role = roleService.GetRole(CreateQueryObject(filter));
            return role.MapTo<RoleDto>();
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        public Result DeleteRole(DeleteRoleCmdDto deleteInfo)
        {
            using (var businessWork = WorkManager.Create())
            {
                #region 参数判断

                if (deleteInfo == null || deleteInfo.RoleIds.IsNullOrEmpty())
                {
                    return Result.FailedResult("没有指定要删除的角色");
                }

                #endregion

                roleService.RemoveRole(deleteInfo.RoleIds);
                var exectVal = businessWork.Commit();
                return exectVal.ExecutedSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public List<RoleDto> GetRoleList(RoleFilterDto filter)
        {
            var roleList = roleService.GetRoleList(CreateQueryObject(filter));
            return roleList.Select(c => c.MapTo<RoleDto>()).ToList();
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取Role分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public IPaging<RoleDto> GetRolePaging(RoleFilterDto filter)
        {
            var rolePaging = roleService.GetRolePaging(CreateQueryObject(filter));
            return rolePaging.ConvertTo<RoleDto>();
        }

        #endregion

        #region 修改角色排序

        /// <summary>
        /// 修改角色排序
        /// </summary>
        /// <param name="sortInfo">排序修改信息</param>
        /// <returns></returns>
        public Result ModifyRoleSort(ModifyRoleSortCmdDto sortInfo)
        {
            using (var businessWork = WorkManager.Create())
            {
                #region 参数判断

                if (sortInfo == null || sortInfo.RoleSysNo <= 0)
                {
                    return Result.FailedResult("没有指定要修改的角色");
                }

                #endregion

                var modifyResult = roleService.ModifyRoleSort(sortInfo.RoleSysNo, sortInfo.NewSort);
                if (!modifyResult.Success)
                {
                    return modifyResult;
                }
                var executeVal = businessWork.Commit();
                return executeVal.ExecutedSuccess ? Result.SuccessResult("排序修改成功") : Result.FailedResult("排序修改失败");
            }
        }

        #endregion

        #region 验证角色名称是否存在

        /// <summary>
        /// 验证角色名称是否存在
        /// </summary>
        /// <param name="existInfo">验证信息</param>
        /// <returns></returns>
        public bool ExistRoleName(ExistRoleNameCmdDto existInfo)
        {
            if (existInfo == null)
            {
                return false;
            }
            return roleService.ExistRoleName(existInfo.RoleName, existInfo.ExcludeRoleId);
        }

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleSysNos">角色编号</param>
        /// <returns></returns>
        public Result ClearRoleUser(IEnumerable<long> roleSysNos)
        {
            if (roleSysNos.IsNullOrEmpty())
            {
                return Result.FailedResult("没有任何要操作的角色");
            }
            using (var work = WorkManager.Create())
            {
                var result = userRoleService.ClearRoleUser(roleSysNos);
                if (!result.Success)
                {
                    return result;
                }
                var commitResult = work.Commit();
                if (!commitResult.EmptyResultOrSuccess)
                {
                    result = Result.FailedResult("修改失败");
                }
                return result;
            }
        }

        #endregion

        #region 根据查询条件生成查询对象

        /// <summary>
        /// 根据查询条件生成查询对象
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns>查询表达式对象</returns>
        public IQuery CreateQueryObject(RoleFilterDto filter, bool useBaseFilter = false)
        {
            if (filter == null)
            {
                return null;
            }
            IQuery query = null;
            if (useBaseFilter)
            {
                query = QueryManager.Create<RoleQuery>(filter);

                #region 数据筛选

                if (!filter.SysNos.IsNullOrEmpty())
                {
                    query.In<RoleQuery>(c => c.SysNo, filter.SysNos);
                }
                if (!filter.ExcludeIds.IsNullOrEmpty())
                {
                    query.NotIn<RoleQuery>(c => c.SysNo, filter.ExcludeIds);
                }
                if (!filter.Name.IsNullOrEmpty())
                {
                    query.Equal<RoleQuery>(c => c.Name, filter.Name);
                }
                if (!filter.NameMateKey.IsNullOrEmpty())
                {
                    query.Like<RoleQuery>(c => c.Name, filter.NameMateKey.Trim());
                }
                if (filter.Level.HasValue)
                {
                    query.Equal<RoleQuery>(c => c.Level, filter.Level.Value);
                }
                if (filter.Parent.HasValue)
                {
                    query.Equal<RoleQuery>(c => c.Parent, filter.Parent.Value);
                }
                if (filter.Sort.HasValue)
                {
                    query.Equal<RoleQuery>(c => c.Sort, filter.Sort.Value);
                }
                if (filter.Status.HasValue)
                {
                    query.Equal<RoleQuery>(c => c.Status, filter.Status.Value);
                }
                if (filter.CreateDate.HasValue)
                {
                    query.Equal<RoleQuery>(c => c.CreateDate, filter.CreateDate.Value);
                }
                if (!filter.Remark.IsNullOrEmpty())
                {
                    query.Equal<RoleQuery>(c => c.Remark, filter.Remark);
                }

                #endregion

                #region 数据加载

                if (filter.LoadParent)
                {
                    query.SetLoadPropertys<Role>(true, r => r.Parent);
                }

                #endregion

                #region 加载上级

                if (filter.QuerySuperiorRole)
                {
                    query.SetRecurve<RoleQuery>(r => r.SysNo, r => r.Parent, RecurveDirection.Up);
                }

                #endregion
            }
            else
            {
                if (filter is UserRoleFilterDto)
                {
                    query = CreateQueryObject(filter as UserRoleFilterDto);
                }
                else
                {
                    query = CreateQueryObject(filter, true);
                }
            }
            return query;
        }

        /// <summary>
        /// 根据用户&角色查询条件生成查询对象
        /// </summary>
        /// <param name="userRoleFilter">用户角色查询条件</param>
        /// <returns></returns>
        IQuery CreateQueryObject(UserRoleFilterDto userRoleFilter)
        {
            if (userRoleFilter == null)
            {
                return null;
            }

            IQuery roleQuery = CreateQueryObject(userRoleFilter, true) ?? QueryManager.Create<RoleQuery>();

            #region 用户筛选

            if (userRoleFilter.UserFilter != null)
            {
                IQuery userQuery = this.Instance<IUserBusiness>().CreateQueryObject(userRoleFilter.UserFilter);
                if (userQuery != null)
                {
                    IQuery userRoleQuery = QueryManager.Create<UserRoleQuery>();
                    userRoleQuery.EqualInnerJoin(userQuery);
                    roleQuery.EqualInnerJoin(userRoleQuery);
                }
            }

            #endregion

            return roleQuery;
        }

        #endregion
    }
}
