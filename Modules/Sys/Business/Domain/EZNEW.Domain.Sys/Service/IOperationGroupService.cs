using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Domain.Sys.Parameter.Filter;
using EZNEW.Paging;
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Service
{
    /// <summary>
    /// 操作分组服务
    /// </summary>
    public interface IOperationGroupService
    {
        #region 删除操作分组

        /// <summary>
        /// 删除操作分组
        /// </summary>
        /// <param name="Ids">操作分组编号</param>
        /// <returns>返回执行结果</returns>
        Result Remove(IEnumerable<long> Ids);

        #endregion

        #region 保存操作分组

        /// <summary>
        /// 保存操作分组
        /// </summary>
        /// <param name="operationGroup">操作分组对象</param>
        /// <returns>返回执行结果</returns>
        Result<OperationGroup> Save(OperationGroup operationGroup);

        #endregion

        #region 获取操作分组

        /// <summary>
        /// 获取操作分组
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns>返回操作分组</returns>
        OperationGroup Get(long groupId);

        /// <summary>
        /// 获取操作分组
        /// </summary>
        /// <param name="operationGroupFilter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        OperationGroup Get(OperationGroupFilter operationGroupFilter);

        #endregion

        #region 获取操作分组列表

        /// <summary>
        /// 获取操作分组列表
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        List<OperationGroup> GetList(IEnumerable<long> groupIds);

        /// <summary>
        /// 获取操作分组列表
        /// </summary>
        /// <param name="operationGroupFilter">操作分组筛选信息</param>
        /// <returns>返回操作分组列表</returns>
        List<OperationGroup> GetList(OperationGroupFilter operationGroupFilter);

        /// <summary>
        /// 根据名称获取操作分组
        /// </summary>
        /// <param name="groupNames">分组名称</param>
        /// <returns>返回操作分组列表</returns>
        List<OperationGroup> GetListByNames(IEnumerable<string> groupNames);

        #endregion

        #region 获取操作分组分页

        /// <summary>
        /// 获取操作分组分页
        /// </summary>
        /// <param name="operationGroupFilter">操作分组筛选信息</param>
        /// <returns>返回操作分页</returns>
        IPaging<OperationGroup> GetPaging(OperationGroupFilter operationGroupFilter);

        #endregion

        #region 修改操作分组排序

        /// <summary>
        /// 修改授权操作分组排序
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <param name="newSort">新的排序</param>
        /// <returns>返回执行结果</returns>
        Result ModifySort(long groupId, int newSort);

        #endregion

        #region 检查操作分组名称是否存在

        /// <summary>
        /// 检查操作分组名称是否存在
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <param name="excludeId">排除验证的分组编号</param>
        /// <returns>返回操作分组名称是否存在</returns>
        bool ExistName(string name, long excludeId);

        #endregion

        #region 初始化操作分组

        /// <summary>
        /// 初始化操作分组
        /// 会清除当前所有的操作分组信息
        /// </summary>
        /// <param name="operationGroups">要初始化的操作分组信息</param>
        /// <returns>返回执行结果</returns>
        Result Initialize(IEnumerable<OperationGroup> operationGroups);

        #endregion
    }
}
