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
    /// 授权操作服务
    /// </summary>
    public interface IAuthorityOperationService
    {
        #region 修改授权操作状态

        /// <summary>
        /// 修改授权操作状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        Result ModifyStatus(params ModifyAuthorityOperationStatus[] statusInfos);

        #endregion

        #region 删除授权操作

        /// <summary>
        /// 删除授权操作
        /// </summary>
        /// <param name="authorityOperationIds">要删除的数据编号</param>
        Result DeleteAuthorityOperation(IEnumerable<long> authorityOperationIds);

        #endregion

        #region 保存授权操作

        /// <summary>
        /// 保存授权操作
        /// </summary>
        /// <param name="authorityOperation">授权操作对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityOperation> SaveAuthorityOperation(AuthorityOperation authorityOperation);

        #endregion

        #region 获取授权操作

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        AuthorityOperation GetAuthorityOperation(IQuery query);

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="operationId">操作编号</param>
        /// <returns></returns>
        AuthorityOperation GetAuthorityOperation(long operationId);

        /// <summary>
        /// 获取授权操作
        /// </summary>
        /// <param name="controllerCode">操作控制器编码（不区分大小写）</param>
        /// <param name="actionCode">操作方法编码（不区分大小写）</param>
        /// <returns></returns>
        AuthorityOperation GetAuthorityOperation(string controllerCode, string actionCode);

        #endregion

        #region 获取授权操作列表

        /// <summary>
        /// 获取授权操作列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        List<AuthorityOperation> GetAuthorityOperationList(IQuery query);

        /// <summary>
        /// 获取授权操作列表
        /// </summary>
        /// <param name="ids">授权操作编号</param>
        /// <returns></returns>
        List<AuthorityOperation> GetAuthorityOperationList(IEnumerable<long> ids);

        #endregion

        #region 获取授权操作分页

        /// <summary>
        /// 获取授权操作分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<AuthorityOperation> GetAuthorityOperationPaging(IQuery query);

        #endregion

        #region 验证操作名称是否存在

        /// <summary>
        /// 验证操作名称是否存在
        /// </summary>
        /// <param name="name">操作名称</param>
        /// <param name="excludeId">排除的编号</param>
        /// <returns></returns>
        bool ExistOperationName(string name, long excludeId);

        #endregion
    }
}
