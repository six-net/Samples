using System.Collections.Generic;
using System.Linq;
using EZNEW.Development.Query;
using EZNEWApp.Domain.Sys.Model;
using EZNEW.Paging;
using EZNEW.Model;
using EZNEW.DependencyInjection;
using EZNEWApp.Domain.Sys.Parameter.Filter;
using EZNEW.Development.Domain.Repository;

namespace EZNEWApp.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 删除操作分组
    /// </summary>
    public class OperationGroupService : IOperationGroupService
    {
        readonly IRepository<OperationGroup> operationGroupRepository;

        public OperationGroupService(IRepository<OperationGroup> operationGroupRepository)
        {
            this.operationGroupRepository = operationGroupRepository;
        }

        #region 保存操作分组

        /// <summary>
        /// 保存操作分组
        /// </summary>
        /// <param name="newOperationGroup">操作分组对象</param>
        /// <returns>执行结果</returns>
        public Result<OperationGroup> Save(OperationGroup newOperationGroup)
        {
            return newOperationGroup?.Save() ?? Result<OperationGroup>.FailedResult("操作分组保存失败");
        }

        #endregion

        #region 删除操作分组

        /// <summary>
        /// 删除操作分组
        /// </summary>
        /// <param name="groupIds">操作分组编号</param>
        public Result Remove(IEnumerable<long> groupIds)
        {
            #region 参数判断

            if (groupIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的信息");
            }

            #endregion

            //删除分组信息
            IQuery removeQuery = QueryManager.Create<OperationGroup>(c => groupIds.Contains(c.Id));
            removeQuery.SetRecurve<OperationGroup>(c => c.Id, c => c.Parent);
            operationGroupRepository.Remove(removeQuery);
            return Result.SuccessResult("删除成功");
        }

        #endregion

        #region 获取操作分组

        /// <summary>
        /// 获取操作分组
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回操作分组</returns>
        OperationGroup Get(IQuery query)
        {
            var authorityOperationGroup = operationGroupRepository.Get(query);
            return authorityOperationGroup;
        }

        /// <summary>
        /// 获取授权操作分组
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns>返回操作分组</returns>
        public OperationGroup Get(long groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }
            IQuery query = QueryManager.Create<OperationGroup>(c => c.Id == groupId);
            return Get(query);
        }

        /// <summary>
        /// 获取操作分组
        /// </summary>
        /// <param name="operationGroupFilter">操作分组筛选信息</param>
        /// <returns>返回操作分组</returns>
        public OperationGroup Get(OperationGroupFilter operationGroupFilter)
        {
            return Get(operationGroupFilter?.CreateQuery());
        }

        #endregion

        #region 获取操作分组列表

        /// <summary>
        /// 获取操作分组列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns>返回操作分组列表</returns>
        List<OperationGroup> GetList(IQuery query)
        {
            var authorityOperationGroupList = operationGroupRepository.GetList(query);
            return authorityOperationGroupList;
        }

        /// <summary>
        /// 获取操作分组列表
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        public List<OperationGroup> GetList(IEnumerable<long> groupIds)
        {
            if (groupIds.IsNullOrEmpty())
            {
                return new List<OperationGroup>(0);
            }
            IQuery query = QueryManager.Create<OperationGroup>(c => groupIds.Contains(c.Id));
            return GetList(query);
        }

        /// <summary>
        /// 获取操作分组列表
        /// </summary>
        /// <param name="operationGroupFilter">操作分组筛选信息</param>
        /// <returns>返回操作分组列表</returns>
        public List<OperationGroup> GetList(OperationGroupFilter operationGroupFilter)
        {
            return GetList(operationGroupFilter?.CreateQuery());
        }

        /// <summary>
        /// 根据名称获取操作分组
        /// </summary>
        /// <param name="groupNames">分组名称</param>
        /// <returns>返回操作分组列表</returns>
        public List<OperationGroup> GetListByNames(IEnumerable<string> groupNames)
        {
            if (groupNames.IsNullOrEmpty())
            {
                return new List<OperationGroup>();
            }
            var query = QueryManager.Create<OperationGroup>(c => groupNames.Contains(c.Name));
            return GetList(query);
        }

        #endregion

        #region 获取操作分组分页

        /// <summary>
        /// 获取操作分组分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        PagingInfo<OperationGroup> GetPaging(IQuery query)
        {
            var authorityOperationGroupPaging = operationGroupRepository.GetPaging(query);
            return authorityOperationGroupPaging;
        }

        /// <summary>
        /// 获取操作分组分页
        /// </summary>
        /// <param name="operationGroupFilter">操作分组筛选信息</param>
        /// <returns>返回操作分组分页</returns>
        public PagingInfo<OperationGroup> GetPaging(OperationGroupFilter operationGroupFilter)
        {
            return GetPaging(operationGroupFilter?.CreateQuery());
        }

        #endregion

        #region 修改操作分组排序

        /// <summary>
        /// 修改授权操作分组排序
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <param name="newSort">新的排序</param>
        /// <returns>返回执行结果</returns>
        public Result ModifySort(long groupId, int newSort)
        {
            #region 参数判断

            if (groupId <= 0)
            {
                return Result.FailedResult("没有指定要修改的分组");
            }

            #endregion

            var group = Get(groupId);
            if (group == null)
            {
                return Result.FailedResult("没有指定要修改的分组");
            }
            group.ModifySort(newSort);
            group.Save();
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 检查操作分组名称是否存在

        /// <summary>
        /// 检查操作分组名称是否存在
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="excludeId">需要排除的操作分组编号</param>
        /// <returns>返回操作分组名称是否存在</returns>
        public bool ExistName(string groupName, long excludeId)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                return false;
            }
            IQuery query = QueryManager.Create<Operation>(c => c.Name == groupName && c.Id != excludeId);
            return operationGroupRepository.Exists(query);
        }

        #endregion

        #region 检查操作分组是否存在

        /// <summary>
        /// 检查操作分组是否存在
        /// </summary>
        /// <param name="ids">操作分组编号</param>
        /// <returns>返回操作分组是否存在</returns>
        public bool Exist(params long[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return false;
            }
            var existQuery = QueryManager.Create<OperationGroup>(o => ids.Contains(o.Id));
            return operationGroupRepository.Exists(existQuery);
        }

        #endregion

        #region 初始化操作分组

        /// <summary>
        /// 初始化操作分组
        /// 会清除当前所有的操作分组信息
        /// </summary>
        /// <param name="operationGroups">要初始化的操作分组信息</param>
        /// <returns>返回执行结果</returns>
        public Result Initialize(IEnumerable<OperationGroup> operationGroups)
        {
            //删除当前操作分组
            operationGroupRepository.Remove(QueryManager.Create<OperationGroup>());
            //添加新的分组信息
            if (!operationGroups.IsNullOrEmpty())
            {
                ////一级分组
                //var levelOneGroups = operationGroups.Where(c => c.Parent < 1);
                //foreach (var group in levelOneGroups)
                //{
                //    InitializeSingleGroup(group, operationGroups);
                //}
                operationGroupRepository.Save(operationGroups);
            }
            return Result.SuccessResult("初始化成功");
        }

        /// <summary>
        /// 初始化单个操作分组
        /// </summary>
        /// <param name="group">操作分组</param>
        /// <param name="allGroups">所有操作分组数据</param>
        /// <returns>返回执行结果</returns>
        Result<OperationGroup> InitializeSingleGroup(OperationGroup group, IEnumerable<OperationGroup> allGroups)
        {
            if (group == null)
            {
                return Result<OperationGroup>.FailedResult("分组信息为空");
            }
            var saveResult = Save(group);
            if (saveResult?.Success ?? false)
            {
                group = saveResult.Data;
                var childGroups = allGroups;//?.Where(c => c.Parent?.Name == group.Name);
                foreach (var childGroup in childGroups)
                {
                    //childGroup.SetParent(group);
                    InitializeSingleGroup(childGroup, childGroups);
                }
            }
            return saveResult;
        }

        #endregion
    }
}
