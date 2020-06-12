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
    /// 权限服务
    /// </summary>
    public class AuthAppService : IAuthAppService
    {
        IAuthBusiness authBusiness = null;

        public AuthAppService(IAuthBusiness authorityBusiness)
        {
            this.authBusiness = authorityBusiness;
        }

        #region 权限数据

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="authority">权限对象</param>
        /// <returns>执行结果</returns>
        public Result<AuthorityDto> SaveAuthority(SaveAuthorityCmdDto authority)
        {
            return authBusiness.SaveAuthority(authority);
        }

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public AuthorityDto GetAuthority(AuthorityFilterDto filter)
        {
            return authBusiness.GetAuthority(filter);
        }

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public List<AuthorityDto> GetAuthorityList(AuthorityFilterDto filter)
        {
            return authBusiness.GetAuthorityList(filter);
        }

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public IPaging<AuthorityDto> GetAuthorityPaging(AuthorityFilterDto filter)
        {
            return authBusiness.GetAuthorityPaging(filter);
        }

        #endregion

        #region 删除权限

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        public Result DeleteAuthority(DeleteAuthorityCmdDto deleteInfo)
        {
            return authBusiness.DeleteAuthority(deleteInfo);
        }

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyAuthorityStatus(ModifyAuthorityStatusCmdDto statusInfo)
        {
            return authBusiness.ModifyAuthorityStatus(statusInfo);
        }

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="codeInfo">权限编码</param>
        /// <returns></returns>
        public bool ExistAuthorityCode(ExistAuthorityCodeCmdDto codeInfo)
        {
            return authBusiness.ExistAuthorityCode(codeInfo);
        }

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="nameInfo">权限名信息</param>
        /// <returns></returns>
        public bool ExistAuthorityName(ExistAuthorityNameCmdDto nameInfo)
        {
            return authBusiness.ExistAuthorityName(nameInfo);
        }

        #endregion

        #endregion

        #region 权限分组

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="authorityGroup">权限分组对象</param>
        /// <returns>执行结果</returns>
        public Result<AuthorityGroupDto> SaveAuthorityGroup(SaveAuthorityGroupCmdDto authorityGroup)
        {
            return authBusiness.SaveAuthorityGroup(authorityGroup);
        }

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public AuthorityGroupDto GetAuthorityGroup(AuthorityGroupFilterDto filter)
        {
            return authBusiness.GetAuthorityGroup(filter);
        }

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public List<AuthorityGroupDto> GetAuthorityGroupList(AuthorityGroupFilterDto filter)
        {
            return authBusiness.GetAuthorityGroupList(filter);
        }

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public IPaging<AuthorityGroupDto> GetAuthorityGroupPaging(AuthorityGroupFilterDto filter)
        {
            return authBusiness.GetAuthorityGroupPaging(filter);
        }

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        public Result DeleteAuthorityGroup(DeleteAuthorityGroupCmdDto deleteInfo)
        {
            return authBusiness.DeleteAuthorityGroup(deleteInfo);
        }

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="sortInfo">排序信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyAuthorityGroupSort(ModifyAuthorityGroupSortCmdDto sortInfo)
        {
            return authBusiness.ModifyAuthorityGroupSort(sortInfo);
        }

        #endregion

        #region 验证权限分组名称是否存在

        /// <summary>
        /// 验证权限分组名称是否存在
        /// </summary>
        /// <param name="existInfo">验证信息</param>
        /// <returns></returns>
        public bool ExistAuthorityGroupName(ExistAuthorityGroupNameCmdDto existInfo)
        {
            return authBusiness.ExistAuthorityGroupName(existInfo);
        }

        #endregion

        #endregion

        #region 操作分组

        #region 保存授权操作组

        /// <summary>
        /// 保存授权操作组
        /// </summary>
        /// <param name="authorityOperationGroup">授权操作组对象</param>
        /// <returns>执行结果</returns>
        public Result<AuthorityOperationGroupDto> SaveAuthorityOperationGroup(SaveAuthorityOperationGroupCmdDto authorityOperationGroup)
        {
            return authBusiness.SaveAuthorityOperationGroup(authorityOperationGroup);
        }

        #endregion

        #region 获取授权操作组

        /// <summary>
        /// 获取授权操作组
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public AuthorityOperationGroupDto GetAuthorityOperationGroup(AuthorityOperationGroupFilterDto filter)
        {
            return authBusiness.GetAuthorityOperationGroup(filter);
        }

        #endregion

        #region 获取授权操作组列表

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public List<AuthorityOperationGroupDto> GetAuthorityOperationGroupList(AuthorityOperationGroupFilterDto filter)
        {
            return authBusiness.GetAuthorityOperationGroupList(filter);
        }

        #endregion

        #region 获取授权操作组分页

        /// <summary>
        /// 获取授权操作组分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public IPaging<AuthorityOperationGroupDto> GetAuthorityOperationGroupPaging(AuthorityOperationGroupFilterDto filter)
        {
            return authBusiness.GetAuthorityOperationGroupPaging(filter);
        }

        #endregion

        #region 删除授权操作组

        /// <summary>
        /// 删除授权操作组
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        public Result DeleteAuthorityOperationGroup(DeleteAuthorityOperationGroupCmdDto deleteInfo)
        {
            return authBusiness.DeleteAuthorityOperationGroup(deleteInfo);
        }

        #endregion

        #region 修改授权操作组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="sortInfo">排序信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyAuthorityOperationGroupSort(ModifyAuthorityOperationGroupSortCmdDto sortInfo)
        {
            return authBusiness.ModifyAuthorityOperationGroupSort(sortInfo);
        }

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="nameInfo">分组名称信息</param>
        /// <returns></returns>
        public bool ExistAuthorityOperationGroupName(ExistAuthorityOperationGroupNameCmdDto nameInfo)
        {
            return authBusiness.ExistAuthorityOperationGroupName(nameInfo);
        }

        #endregion

        #endregion

        #region 授权操作

        #region 保存授权操作

        /// <summary>
        /// 保存授权操作
        /// </summary>
        /// <param name="authorityOperation">授权操作对象</param>
        /// <returns>执行结果</returns>
        public Result<AuthorityOperationDto> SaveAuthorityOperation(SaveAuthorityOperationCmdDto authorityOperation)
        {
            return authBusiness.SaveAuthorityOperation(authorityOperation);
        }

        #endregion

        #region 获取授权操作

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public AuthorityOperationDto GetAuthorityOperation(AuthorityOperationFilterDto filter)
        {
            return authBusiness.GetAuthorityOperation(filter);
        }

        #endregion

        #region 获取授权操作列表

        /// <summary>
        /// 获取授权操作列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public List<AuthorityOperationDto> GetAuthorityOperationList(AuthorityOperationFilterDto filter)
        {
            return authBusiness.GetAuthorityOperationList(filter);
        }

        #endregion

        #region 获取授权操作分页

        /// <summary>
        /// 获取授权操作分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        public IPaging<AuthorityOperationDto> GetAuthorityOperationPaging(AuthorityOperationFilterDto filter)
        {
            return authBusiness.GetAuthorityOperationPaging(filter);
        }

        #endregion

        #region 删除授权操作

        /// <summary>
        /// 删除授权操作
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        public Result DeleteAuthorityOperation(DeleteAuthorityOperationCmdDto deleteInfo)
        {
            return authBusiness.DeleteAuthorityOperation(deleteInfo);
        }

        #endregion

        #region 修改授权操作状态

        /// <summary>
        /// 修改授权操作状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyAuthorityOperationStatus(ModifyAuthorityOperationStatusCmdDto statusInfo)
        {
            return authBusiness.ModifyAuthorityOperationStatus(statusInfo);
        }

        #endregion

        #region 修改授权操作绑定权限

        /// <summary>
        /// 修改授权操作绑定权限
        /// </summary>
        /// <param name="bindInfo">权限绑定信息</param>
        /// <returns></returns>
        public Result ModifyAuthorityOperationBindAuthority(ModifyAuthorityBindAuthorityOperationCmdDto bindInfo)
        {
            return authBusiness.ModifyAuthorityOperationBindAuthority(bindInfo);
        }

        #endregion

        #region 验证授权操作名是否存在

        /// <summary>
        /// 验证授权操作名是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="excludeId">排除指定的授权操作</param>
        /// <returns></returns>
        public bool ExistAuthorityOperationName(string name, long excludeId)
        {
            return authBusiness.ExistAuthorityOperationName(name, excludeId);
        }

        #endregion

        #endregion

        #region 授权

        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="authInfo">授权信息</param>
        /// <returns></returns>
        public Result ModifyRoleAuthorize(ModifyRoleAuthorizeCmdDto authInfo)
        {
            return authBusiness.ModifyRoleAuthorize(authInfo);
        }

        #endregion

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="authorizeInfo">用户授权信息</param>
        /// <returns></returns>
        public Result ModifyUserAuthorize(ModifyUserAuthorizeCmdDto authorizeInfo)
        {
            return authBusiness.ModifyUserAuthorize(authorizeInfo);
        }

        #endregion

        #region 清除用户授权

        /// <summary>
        /// 清除用户授权
        /// </summary>
        /// <param name="userSysNos">用户系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearUserAuthorize(IEnumerable<long> userSysNos)
        {
            return authBusiness.ClearUserAuthorize(userSysNos);
        }

        #endregion

        #region 清除角色授权

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleSysNos">角色系统编号</param>
        /// <returns>执行结果</returns>
        public Result ClearRoleAuthorize(IEnumerable<long> roleSysNos)
        {
            return authBusiness.ClearRoleAuthorize(roleSysNos);
        }

        #endregion

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="auth">授权验证信息</param>
        /// <returns></returns>
        public bool Authentication(AuthenticationCmdDto auth)
        {
            return authBusiness.Authentication(auth);
        }

        #endregion

        #endregion
    }
}
