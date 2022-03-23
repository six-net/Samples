using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZNEWApp.AppServiceContract.Sys;
using EZNEW.Development.UnitOfWork;
using EZNEWApp.Domain.Sys.Model;
using EZNEWApp.Domain.Sys.Parameter;
using EZNEWApp.Domain.Sys.Service;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEWApp.BusinessContract.Sys;

namespace EZNEWApp.AppService.Sys
{
    /// <summary>
    /// 操作功能业务逻辑
    /// </summary>
    public class OperationAppService : IOperationAppService
    {
        IOperationBusiness operationBusiness;

        public OperationAppService(IOperationBusiness operationBusiness)
        {
            this.operationBusiness = operationBusiness;
        }

        #region 保存操作功能

        /// <summary>
        /// 保存操作功能
        /// </summary>
        /// <param name="saveOperationParameter">操作功能保存信息</param>
        /// <returns>执行结果</returns>
        public Result<Operation> SaveOperation(SaveOperationParameter saveOperationParameter)
        {
            return operationBusiness.SaveOperation(saveOperationParameter);
        }

        #endregion

        #region 获取操作功能

        /// <summary>
        /// 获取操作功能
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能</returns>
        public Operation GetOperation(OperationFilter filter)
        {
            return operationBusiness.GetOperation(filter);
        }

        #endregion

        #region 获取操作功能列表

        /// <summary>
        /// 获取操作功能列表
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能列表</returns>
        public List<Operation> GetOperationList(OperationFilter filter)
        {
            return operationBusiness.GetOperationList(filter);
        }

        #endregion

        #region 获取操作功能分页

        /// <summary>
        /// 获取操作功能分页
        /// </summary>
        /// <param name="filter">操作功能筛选信息</param>
        /// <returns>返回操作功能分页</returns>
        public PagingInfo<Operation> GetOperationPaging(OperationFilter filter)
        {
            return operationBusiness.GetOperationPaging(filter);
        }

        #endregion

        #region 删除操作功能

        /// <summary>
        /// 删除操作功能
        /// </summary>
        /// <param name="removeOperationParameter">操作功能删除信息</param>
        /// <returns>返回执行结果</returns>
        public Result RemoveOperation(RemoveOperationParameter removeOperationParameter)
        {
            return operationBusiness.RemoveOperation(removeOperationParameter);
        }

        #endregion

        #region 修改操作状态

        /// <summary>
        /// 修改操作功能状态
        /// </summary>
        /// <param name="statusInfo">状态信息</param>
        /// <returns>执行结果</returns>
        public Result ModifyOperationStatus(ModifyOperationStatusParameter modifyOperationStatusParameter)
        {
            return operationBusiness.ModifyOperationStatus(modifyOperationStatusParameter);
        }

        #endregion

        #region 验证操作功能名是否存在

        /// <summary>
        /// 验证操作功能名是否存在
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="excludeId">排除指定的操作功能</param>
        /// <returns>返回操作功能名称是否存在</returns>
        public bool ExistOperationName(string name, long excludeId)
        {
            return operationBusiness.ExistOperationName(name, excludeId);
        }

        #endregion

        #region 清除操作授权的权限

        /// <summary>
        /// 清除操作授权的权限
        /// </summary>
        /// <param name="operationIds">操作功能编号</param>
        /// <returns>返回执行结果</returns>
        public Result ClearPermission(IEnumerable<long> operationIds)
        {
            return operationBusiness.ClearPermission(operationIds);
        }

        #endregion

        #region 授权验证

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="checkAuthorizationParameter">授权验证信息</param>
        /// <returns>返回授权是否验证通过</returns>
        public bool CheckAuthorization(CheckAuthorizationParameter checkAuthorizationParameter)
        {
            return operationBusiness.CheckAuthorization(checkAuthorizationParameter);
        }

        #endregion

        #region 初始化操作功能

        /// <summary>
        /// 初始化操作功能
        /// </summary>
        /// <param name="initializeOperationParameter">操作功能初始化信息</param>
        /// <returns>返回执行结果</returns>
        public Result Initialize(InitializeOperationParameter initializeOperationParameter)
        {
            return operationBusiness.Initialize(initializeOperationParameter);
        }

        #endregion
    }
}
