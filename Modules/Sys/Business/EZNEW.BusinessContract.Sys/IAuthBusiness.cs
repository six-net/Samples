using EZNEW.Develop.CQuery;
using EZNEW.Framework.Paging;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Query;
using EZNEW.DTO.Sys.Query.Filter;
using EZNEW.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Response;

namespace EZNEW.BusinessContract.Sys
{
    /// <summary>
    /// 权限业务接口
    /// </summary>
    public interface IAuthBusiness
    {
        #region 权限数据

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="authority">权限对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityDto> SaveAuthority(SaveAuthorityCmdDto authority);

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        AuthorityDto GetAuthority(AuthorityFilterDto filter);

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        List<AuthorityDto> GetAuthorityList(AuthorityFilterDto filter);

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        IPaging<AuthorityDto> GetAuthorityPaging(AuthorityFilterDto filter);

        #endregion

        #region 删除权限

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        Result DeleteAuthority(DeleteAuthorityCmdDto deleteInfo);

        #endregion

        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        Result ModifyAuthorityStatus(ModifyAuthorityStatusCmdDto statusInfo);

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="codeInfo">权限编码</param>
        /// <returns></returns>
        bool ExistAuthorityCode(ExistAuthorityCodeCmdDto codeInfo);

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="nameInfo">权限名信息</param>
        /// <returns></returns>
        bool ExistAuthorityName(ExistAuthorityNameCmdDto nameInfo);

        #endregion

        #endregion

        #region 权限分组

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="authorityGroup">保存分组对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityGroupDto> SaveAuthorityGroup(SaveAuthorityGroupCmdDto authorityGroup);

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        AuthorityGroupDto GetAuthorityGroup(AuthorityGroupFilterDto filter);

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        List<AuthorityGroupDto> GetAuthorityGroupList(AuthorityGroupFilterDto filter);

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        IPaging<AuthorityGroupDto> GetAuthorityGroupPaging(AuthorityGroupFilterDto filter);

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        Result DeleteAuthorityGroup(DeleteAuthorityGroupCmdDto deleteInfo);

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="sortInfo">排序修改信息</param>
        /// <returns></returns>
        Result ModifyAuthorityGroupSort(ModifyAuthorityGroupSortCmdDto sortInfo);

        #endregion

        #region 验证权限分组名称是否存在

        /// <summary>
        /// 验证权限分组名称是否存在
        /// </summary>
        /// <param name="existInfo">验证信息</param>
        /// <returns></returns>
        bool ExistAuthorityGroupName(ExistAuthorityGroupNameCmdDto existInfo);

        #endregion

        #endregion

        #region 操作分组

        #region 保存授权操作组

        /// <summary>
        /// 保存授权操作组
        /// </summary>
        /// <param name="authorityOperationGroup">授权操作组对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityOperationGroupDto> SaveAuthorityOperationGroup(SaveAuthorityOperationGroupCmdDto authorityOperationGroup);

        #endregion

        #region 获取授权操作组

        /// <summary>
        /// 获取授权操作组
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        AuthorityOperationGroupDto GetAuthorityOperationGroup(AuthorityOperationGroupFilterDto filter);

        #endregion

        #region 获取授权操作组列表

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        List<AuthorityOperationGroupDto> GetAuthorityOperationGroupList(AuthorityOperationGroupFilterDto filter);

        #endregion

        #region 获取授权操作组分页

        /// <summary>
        /// 获取授权操作组分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        IPaging<AuthorityOperationGroupDto> GetAuthorityOperationGroupPaging(AuthorityOperationGroupFilterDto filter);

        #endregion

        #region 删除授权操作组

        /// <summary>
        /// 删除授权操作组
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        Result DeleteAuthorityOperationGroup(DeleteAuthorityOperationGroupCmdDto deleteInfo);

        #endregion

        #region 修改授权操作组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="sortInfo">排序信息</param>
        /// <returns>执行结果</returns>
        Result ModifyAuthorityOperationGroupSort(ModifyAuthorityOperationGroupSortCmdDto sortInfo);

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="nameInfo">分组名称信息</param>
        /// <returns></returns>
        bool ExistAuthorityOperationGroupName(ExistAuthorityOperationGroupNameCmdDto nameInfo);

        #endregion

        #endregion

        #region 授权操作

        #region 保存授权操作

        /// <summary>
        /// 保存授权操作
        /// </summary>
        /// <param name="authorityOperation">授权操作对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityOperationDto> SaveAuthorityOperation(SaveAuthorityOperationCmdDto authorityOperation);

        #endregion

        #region 获取授权操作

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        AuthorityOperationDto GetAuthorityOperation(AuthorityOperationFilterDto filter);

        #endregion

        #region 获取授权操作列表

        /// <summary>
        /// 获取授权操作列表
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        List<AuthorityOperationDto> GetAuthorityOperationList(AuthorityOperationFilterDto filter);

        #endregion

        #region 获取授权操作分页

        /// <summary>
        /// 获取授权操作分页
        /// </summary>
        /// <param name="filter">查询对象</param>
        /// <returns></returns>
        IPaging<AuthorityOperationDto> GetAuthorityOperationPaging(AuthorityOperationFilterDto filter);

        #endregion

        #region 删除授权操作

        /// <summary>
        /// 删除授权操作
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        /// <returns>执行结果</returns>
        Result DeleteAuthorityOperation(DeleteAuthorityOperationCmdDto deleteInfo);

        #endregion

        #region 修改授权操作状态

        /// <summary>
        /// 修改授权操作状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        Result ModifyAuthorityOperationStatus(ModifyAuthorityOperationStatusCmdDto statusInfo);

        #endregion

        #region 修改授权操作绑定权限

        /// <summary>
        /// 修改授权操作绑定权限
        /// </summary>
        /// <param name="bindInfo">权限绑定信息</param>
        /// <returns></returns>
        Result ModifyAuthorityOperationBindAuthority(ModifyAuthorityBindAuthorityOperationCmdDto bindInfo);

        #endregion

        #region 验证授权操作名是否存在

        /// <summary>
        /// 验证授权操作名是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="excludeId">排除指定的授权操作</param>
        /// <returns></returns>
        bool ExistAuthorityOperationName(string name, long excludeId);

        #endregion

        #endregion

        #region 授权

        #region 修改角色授权

        /// <summary>
        /// 修改角色授权
        /// </summary>
        /// <param name="authInfo">授权信息</param>
        /// <returns></returns>
        Result ModifyRoleAuthorize(ModifyRoleAuthorizeCmdDto authInfo);

        #endregion

        #region 修改用户授权

        /// <summary>
        /// 修改用户授权
        /// </summary>
        /// <param name="authorizeInfo">用户授权信息</param>
        /// <returns></returns>
        Result ModifyUserAuthorize(ModifyUserAuthorizeCmdDto authorizeInfo);

        #endregion

        #region 清除用户授权

        /// <summary>
        /// 清除用户授权
        /// </summary>
        /// <param name="userSysNos">用户系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearUserAuthorize(IEnumerable<long> userSysNos);

        #endregion

        #region 清除角色授权

        /// <summary>
        /// 清除角色授权
        /// </summary>
        /// <param name="roleSysNos">角色系统编号</param>
        /// <returns>执行结果</returns>
        Result ClearRoleAuthorize(IEnumerable<long> roleSysNos);

        #endregion

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="auth">授权验证信息</param>
        /// <returns></returns>
        bool Authentication(AuthenticationCmdDto auth);

        #endregion

        #endregion
    }
}
