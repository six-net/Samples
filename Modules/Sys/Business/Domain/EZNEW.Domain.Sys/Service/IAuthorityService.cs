using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Service.Param;
using EZNEW.Framework.Paging;
using EZNEW.Framework.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IAuthorityService
    {
        #region 修改权限状态

        /// <summary>
        /// 修改权限状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        Result ModifyAuthorityStatus(params ModifyAuthorityStatus[] statusInfos);

        #endregion

        #region 删除权限

        /// <summary>
        /// 根据系统编号删除权限
        /// </summary>
        /// <param name="sysNos">权限系统编号</param>
        void DeleteAuthority(IEnumerable<long> sysNos);

        #endregion

        #region 保存权限

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="authority">权限对象</param>
        /// <returns>执行结果</returns>
        Result<Authority> SaveAuthority(Authority authority);

        #endregion

        #region 获取权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        Authority GetAuthority(IQuery query);

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="authCode">权限码</param>
        /// <returns></returns>
        Authority GetAuthority(string authCode);

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        List<Authority> GetAuthorityList(IQuery query);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="codes">权限码</param>
        /// <returns></returns>
        List<Authority> GetAuthorityList(IEnumerable<string> codes);

        #endregion

        #region 获取权限分页

        /// <summary>
        /// 获取权限分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<Authority> GetAuthorityPaging(IQuery query);

        #endregion

        #region 检查权限编码是否存在

        /// <summary>
        /// 检查权限编码是否存在
        /// </summary>
        /// <param name="nowSysNo">当前系统编号</param>
        /// <param name="code">权限编码</param>
        /// <returns></returns>
        bool ExistAuthorityCode(long nowSysNo, string code);

        #endregion

        #region 检查权限名称是否存在

        /// <summary>
        /// 检查权限名称是否存在
        /// </summary>
        /// <param name="nowSysNo">当前系统编号</param>
        /// <param name="name">权限名称</param>
        /// <returns></returns>
        bool ExistAuthorityName(long nowSysNo, string name);

        #endregion
    }
}
