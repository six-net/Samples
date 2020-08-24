using System;
using System.Collections.Generic;
using System.Linq;
using EZNEW.Paging;
using EZNEW.Response;
using EZNEW.Develop.UnitOfWork;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.BusinessContract.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Service;
using EZNEW.DependencyInjection;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.DTO.Sys;

namespace EZNEW.Business.Sys
{
    /// <summary>
    /// 角色业务
    /// </summary>
    public class RoleBusiness : IRoleBusiness
    {
        static readonly IRoleService roleService = ContainerManager.Resolve<IRoleService>();
        static readonly IUserRoleService userRoleService = ContainerManager.Resolve<IUserRoleService>();

        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveRoleDto">角色保存信息</param>
        /// <returns>返回角色保存执行结果</returns>
        public Result<RoleDto> SaveRole(SaveRoleDto saveRoleDto)
        {
            if (saveRoleDto?.Role == null)
            {
                return Result<RoleDto>.FailedResult("没有指定任何要保存的信息");
            }
            using (var businessWork = WorkManager.Create())
            {
                var roleResult = roleService.Save(saveRoleDto.Role.MapTo<Role>());
                if (!roleResult.Success)
                {
                    return Result<RoleDto>.FailedResult(roleResult.Message);
                }
                var commitResult = businessWork.Commit();
                Result<RoleDto> result;
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
        /// <param name="roleFilterDto">角色筛选信息</param>
        /// <returns></returns>
        public RoleDto GetRole(RoleFilterDto roleFilterDto)
        {
            var role = roleService.Get(roleFilterDto?.ConvertToFilter());
            return role.MapTo<RoleDto>();
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="deleteRoleDto">删除角色信息</param>
        /// <returns>返回删除角色执行结果</returns>
        public Result RemoveRole(RemoveRoleDto deleteRoleDto)
        {
            using (var businessWork = WorkManager.Create())
            {
                #region 参数判断

                if (deleteRoleDto?.Ids.IsNullOrEmpty() ?? true)
                {
                    return Result.FailedResult("没有指定要删除的角色");
                }

                #endregion

                var removeRoleResult = roleService.Remove(deleteRoleDto.Ids);
                if (!removeRoleResult.Success)
                {
                    return removeRoleResult;
                }
                var commitResult = businessWork.Commit();
                return commitResult.ExecutedSuccess ? Result.SuccessResult("删除成功") : Result.FailedResult("删除失败");
            }
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleFilterDto">角色筛选信息</param>
        /// <returns>返回角色列表</returns>
        public List<RoleDto> GetRoleList(RoleFilterDto roleFilterDto)
        {
            var roleList = roleService.GetList(roleFilterDto?.ConvertToFilter());
            return roleList.Select(c => c.MapTo<RoleDto>()).ToList();
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="roleFilterDto">角色筛选信息</param>
        /// <returns>返回角色分页</returns>
        public IPaging<RoleDto> GetRolePaging(RoleFilterDto roleFilterDto)
        {
            var rolePaging = roleService.GetPaging(roleFilterDto?.ConvertToFilter());
            return rolePaging.ConvertTo<RoleDto>();
        }

        #endregion

        #region 修改角色排序

        /// <summary>
        /// 修改角色排序
        /// </summary>
        /// <param name="modifyRoleSortDto">角色排序修改信息</param>
        /// <returns>返回角色排序修改执行结果</returns>
        public Result ModifyRoleSort(ModifyRoleSortDto modifyRoleSortDto)
        {
            using (var businessWork = WorkManager.Create())
            {
                #region 参数判断

                if (modifyRoleSortDto?.Id <= 0)
                {
                    return Result.FailedResult("没有指定要修改的角色");
                }

                #endregion

                var modifyResult = roleService.ModifySort(modifyRoleSortDto.Id, modifyRoleSortDto.NewSort);
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
        /// <param name="existRoleNameDto">角色名称验证信息</param>
        /// <returns>返回角色名称是否存在</returns>
        public bool ExistRoleName(ExistRoleNameDto existRoleNameDto)
        {
            if (existRoleNameDto == null)
            {
                return false;
            }
            return roleService.ExistName(existRoleNameDto.Name, existRoleNameDto.ExcludeId);
        }

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回清除用户执行结果</returns>
        public Result ClearUser(IEnumerable<long> roleIds)
        {
            if (roleIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有任何要操作的角色");
            }
            using (var work = WorkManager.Create())
            {
                var result = userRoleService.ClearByRole(roleIds);
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
    }
}
