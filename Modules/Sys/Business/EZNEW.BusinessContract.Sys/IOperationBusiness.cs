using EZNEW.DTO.Sys;
using EZNEW.DTO.Sys.Cmd;
using EZNEW.DTO.Sys.Filter;
using EZNEW.Paging;
using EZNEW.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.BusinessContract.Sys
{
    /// <summary>
    /// 操作功能业务逻辑
    /// </summary>
    public interface IOperationBusiness
    {
        #region 保存操作功能

        /// <summary>
        /// 保存操作功能
        /// </summary>
        /// <param name="saveOperationDto">操作功能保存信息</param>
        /// <returns>返回执行结果</returns>
        Result<OperationDto> SaveOperation(SaveOperationDto saveOperationDto);

        #endregion

        #region 获取操作功能

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能</returns>
        OperationDto GetOperation(OperationFilterDto filter);

        #endregion

        #region 获取操作功能列表

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        List<OperationDto> GetOperationList(OperationFilterDto filter);

        #endregion

        #region 获取操作功能分页

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        IPaging<OperationDto> GetOperationPaging(OperationFilterDto filter);

        #endregion

        #region 删除操作功能

        /// <summary>
        /// 删除操作功能
        /// </summary>
        /// <param name="removeOperationDto">操作功能删除信息</param>
        /// <returns>返回执行结果</returns>
        Result RemoveOperation(RemoveOperationDto removeOperationDto);

        #endregion

        #region 修改操作功能状态

        /// <summary>
        /// 修改操作功能状态
        /// </summary>
        /// <param name="modifyOperationStatusDto">操作功能状态修改信息</param>
        /// <returns>返回执行结果</returns>
        Result ModifyOperationStatus(ModifyOperationStatusDto modifyOperationStatusDto);

        #endregion

        #region 清除操作授权的权限

        /// <summary>
        /// 清除操作授权的权限
        /// </summary>
        /// <param name="operationIds">操作功能编号</param>
        /// <returns>返回执行结果</returns>
        Result ClearOperationPermission(IEnumerable<long> operationIds);

        #endregion

        #region 验证操作功能名是否存在

        /// <summary>
        /// 验证操作功能名是否存在
        /// </summary>
        /// <param name="name">操作功能名称</param>
        /// <param name="excludeId">需要排除的操作功能编号</param>
        /// <returns>返回操作功能名称是否存在</returns>
        bool ExistOperationName(string name, long excludeId);

        #endregion

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="checkAuthorizationDto">授权验证信息</param>
        /// <returns>返回授权是否验证通过</returns>
        bool CheckAuthorization(CheckAuthorizationDto checkAuthorizationDto);

        #endregion

        #region 初始化操作功能

        /// <summary>
        /// 初始化操作功能
        /// </summary>
        /// <param name="initializeOperationDto">操作功能初始化信息</param>
        /// <returns>返回执行结果</returns>
        Result Initialize(InitializeOperationDto initializeOperationDto);

        #endregion
    }
}
