using System.Collections.Generic;
using EZNEW.BusinessContract.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.Response;
using EZNEW.Paging;
using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Filter;
using EZNEW.AppServiceContract.Sys;

namespace EZNEW.AppService.Sys
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleAppService : IRoleAppService
    {
        /// <summary>
        /// 角色业务
        /// </summary>
        readonly IRoleBusiness roleBusiness = null;

        public RoleAppService(IRoleBusiness roleBusiness)
        {
            this.roleBusiness = roleBusiness;
        }

        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveInfo">角色保存信息</param>
        /// <returns>返回执行结果</returns>
        public Result<RoleDto> SaveRole(SaveRoleDto saveRoleDto)
        {
            return roleBusiness.SaveRole(saveRoleDto);
        }

        #endregion

        #region 获取角色

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="filter">角色筛选信息</param>
        /// <returns>返回角色</returns>
        public RoleDto GetRole(RoleFilterDto filter)
        {
            return roleBusiness.GetRole(filter);
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="removeRoleDto">角色删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveRole(RemoveRoleDto removeRoleDto)
        {
            return roleBusiness.RemoveRole(removeRoleDto);
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="filter">角色删除信息</param>
        /// <returns>返回角色列表</returns>
        public List<RoleDto> GetRoleList(RoleFilterDto filter)
        {
            return roleBusiness.GetRoleList(filter);
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="filter">角色删除信息</param>
        /// <returns>返回角色分页</returns>
        public IPaging<RoleDto> GetRolePaging(RoleFilterDto filter)
        {
            return roleBusiness.GetRolePaging(filter);
        }

        #endregion

        #region 修改角色排序

        /// <summary>
        /// 修改角色排序
        /// </summary>
        /// <param name="modifyRoleSortDto">角色排序修改信息</param>
        /// <returns>返回执行结果</returns>
        public Result ModifyRoleSort(ModifyRoleSortDto modifyRoleSortDto)
        {
            return roleBusiness.ModifyRoleSort(modifyRoleSortDto);
        }

        #endregion

        #region 验证角色名称是否存在

        /// <summary>
        /// 验证角色名称是否存在
        /// </summary>
        /// <param name="existInfo">角色名称验证信息</param>
        /// <returns>返回角色名称是否存在</returns>
        public bool ExistRoleName(ExistRoleNameDto existRoleNameDto)
        {
            return roleBusiness.ExistRoleName(existRoleNameDto);
        }

        #endregion

        #region 清除角色下所有的用户

        /// <summary>
        /// 清除角色下所有的用户
        /// </summary>
        /// <param name="roleIds">角色编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearUser(IEnumerable<long> roleIds)
        {
            return roleBusiness.ClearUser(roleIds);
        }

        #endregion
    }
}
