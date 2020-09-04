using System.Collections.Generic;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 操作功能服务
    /// </summary>
    public interface IOperationService
    {
        #region 修改操作功能状态

        /// <summary>
        /// 修改操作功能状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        Result ModifyStatus(ModifyOperationStatusParameter modifyOperationStatus);

        #endregion

        #region 删除操作功能

        /// <summary>
        /// 删除操作功能
        /// </summary>
        /// <param name="ids">要删除的功能编号</param>
        /// <returns>返回执行结果</returns>
        Result Remove(IEnumerable<long> ids);

        #endregion

        #region 保存操作功能

        /// <summary>
        /// 保存操作功能
        /// </summary>
        /// <param name="operation">操作功能对象</param>
        /// <returns>执行结果</returns>
        Result<Operation> Save(Operation operation);

        #endregion

        #region 获取操作功能

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="operationId">操作编号</param>
        /// <returns>返回操作功能</returns>
        Operation Get(long operationId);

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="controllerCode">操作控制器编码（不区分大小写）</param>
        /// <param name="actionCode">操作方法编码（不区分大小写）</param>
        /// <returns>返回操作功能</returns>
        Operation Get(string controllerCode, string actionCode);

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="operationFilter">操作功能筛选信息</param>
        /// <returns>返回操作功能</returns>
        Operation Get(OperationFilter operationFilter);

        #endregion

        #region 获取操作功能列表

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="ids">操作功能编号</param>
        /// <returns>返回操作功能列表</returns>
        List<Operation> GetList(IEnumerable<long> ids);

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="operationFilter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        List<Operation> GetList(OperationFilter operationFilter);

        #endregion

        #region 获取操作功能分页

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="operationFilter">操作功能筛选信息</param>
        /// <returns>返回操作功能分页</returns>
        IPaging<Operation> GetPaging(OperationFilter operationFilter);

        #endregion

        #region 验证操作名称是否存在

        /// <summary>
        /// 验证操作名称是否存在
        /// </summary>
        /// <param name="name">操作名称</param>
        /// <param name="excludeId">需要排除的操作编号</param>
        /// <returns>返回操作功能名称是否存在</returns>
        bool ExistName(string name, long excludeId);

        #endregion

        #region 验证用户操作授权

        /// <summary>
        /// 验证用户操作授权
        /// </summary>
        /// <param name="checkOperationAuthorization">用户操作功能授权信息</param>
        /// <returns>返回用户是否已授权</returns>
        bool CheckAuthorization(CheckAuthorizationParameter checkOperationAuthorization);

        #endregion

        #region 初始化操作功能

        /// <summary>
        /// 初始化操作功能
        /// 将清除当前所有的操作功能
        /// </summary>
        /// <param name="initializeOperation">操作功能初始化信息</param>
        /// <returns>返回执行结果</returns>
        Result Initialize(InitializeOperationParameter initializeOperation);

        #endregion
    }
}
