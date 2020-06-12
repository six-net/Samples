using EZNEW.AppServiceContract.Sys;
using System.Collections.Generic;
using EZNEW.BusinessContract.Sys;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.AppService.Sys
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleAppService : IRoleAppService
    {
        IRoleBusiness roleBusiness = null;

        public RoleAppService(IRoleBusiness roleBusiness)
        {
            this.roleBusiness = roleBusiness;
        }

        #region 保存角色

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="saveInfo">角色保存信息</param>
        /// <returns>执行结果</returns>
        public Result<RoleDto> SaveRole(SaveRoleCmdDto saveInfo)
        {
            return roleBusiness.SaveRole(saveInfo);
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
            return roleBusiness.GetRole(filter);
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
            return roleBusiness.DeleteRole(deleteInfo);
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
            return roleBusiness.GetRoleList(filter);
        }

        #endregion

        #region 获取角色分页

        /// <summary>
        /// 获取角色分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public IPaging<RoleDto> GetRolePaging(RoleFilterDto filter)
        {
            return roleBusiness.GetRolePaging(filter);
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
            return roleBusiness.ModifyRoleSort(sortInfo);
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
            return roleBusiness.ExistRoleName(existInfo);
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
            return roleBusiness.ClearRoleUser(roleSysNos);
        }

        #endregion
    }
}
