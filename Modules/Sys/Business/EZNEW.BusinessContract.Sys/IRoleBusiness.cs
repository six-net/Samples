using System.Collections.Generic;
using EZNEW.Response;
using EZNEW.DTO.Sys.Filter;
using EZNEW.DTO.Sys;
using EZNEW.Paging;
using EZNEW.DTO.Sys.Cmd;

namespace EZNEW.BusinessContract.Sys
{
    /// <summary>
    /// 角色业务接口
    /// </summary>
    public interface IRoleBusiness
    {
        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveRoleDto">角色保存信息</param>
        /// <returns></returns>
        Result<RoleDto> SaveRole(SaveRoleDto saveRoleDto);

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleFilterDto">角色筛选信息</param>
        /// <returns>返回角色信息</returns>
        RoleDto GetRole(RoleFilterDto roleFilterDto);

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="deleteRoleDto">角色删除信息</param>
        /// <returns>返回删除角色执行结果</returns>
        Result RemoveRole(RemoveRoleDto deleteRoleDto);

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleFilterDto">角色筛选信息</param>
        /// <returns>返回角色列表</returns>
        List<RoleDto> GetRoleList(RoleFilterDto roleFilterDto);

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="roleFilterDto">角色筛选信息</param>
        /// <returns>返回角色分页</returns>
        IPaging<RoleDto> GetRolePaging(RoleFilterDto roleFilterDto);

        #endregion

        #region 修改角色排序

        /// <summary>
        /// 修改角色排序
        /// </summary>
        /// <param name="modifyRoleSortDto">角色排序修改信息</param>
        /// <returns>返回角色排序修改执行结果</returns>
        Result ModifyRoleSort(ModifyRoleSortDto modifyRoleSortDto);

        #endregion

        #region 验证角色名称是否存在

        /// <summary>
        /// 验证角色名称是否存在
        /// </summary>
        /// <param name="existRoleNameDto">角色名称验证信息</param>
        /// <returns>返回角色名称是否存在</returns>
        bool ExistRoleName(ExistRoleNameDto existRoleNameDto);

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleSysNos">角色编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearUser(IEnumerable<long> roleSysNos);

        #endregion
    }
}
