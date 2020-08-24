using System.Collections.Generic;
using System.Linq;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Model;
using EZNEW.Paging;
using EZNEW.Response;
using EZNEW.Domain.Sys.Repository;
using EZNEW.DependencyInjection;
using EZNEW.Entity.Sys;
using EZNEW.Domain.Sys.Parameter.Filter;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 删除操作分组
    /// </summary>
    public class OperationGroupService : IOperationGroupService
    {
        static readonly IOperationGroupRepository operationGroupRepository = ContainerManager.Resolve<IOperationGroupRepository>();

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
            IQuery removeQuery = QueryManager.Create<OperationGroupEntity>(c => groupIds.Contains(c.Id));
            removeQuery.SetRecurve<OperationGroupEntity>(c => c.Id, c => c.Parent);
            operationGroupRepository.Remove(removeQuery);
            return Result.SuccessResult("删除成功");
        }

        #endregion

        #region 保存操作分组

        /// <summary>
        /// 保存操作分组
        /// </summary>
        /// <param name="operationGroup">操作分组对象</param>
        /// <returns>执行结果</returns>
        public Result<OperationGroup> Save(OperationGroup operationGroup)
        {
            if (operationGroup == null)
            {
                return Result<OperationGroup>.FailedResult("操作分组信息不完整");
            }
            return operationGroup.Id > 0 ? UpdateOperationGroup(operationGroup) : AddOperationGroup(operationGroup);
        }

        /// <summary>
        /// 添加操作分组
        /// </summary>
        /// <param name="operationGroup">操作分组对象</param>
        /// <returns>执行结果</returns>
        Result<OperationGroup> AddOperationGroup(OperationGroup operationGroup)
        {
            #region 上级

            long parentGroupId = operationGroup.Parent == null ? 0 : operationGroup.Parent.Id;
            OperationGroup parentGroup = null;
            if (parentGroupId > 0)
            {
                IQuery parentQuery = QueryManager.Create<OperationGroupEntity>(c => c.Id == parentGroupId);
                parentGroup = operationGroupRepository.Get(parentQuery);
                if (parentGroup == null)
                {
                    return Result<OperationGroup>.FailedResult("请选择正确的上级分组");
                }
            }
            operationGroup.SetParentGroup(parentGroup);

            #endregion

            operationGroup.Save();//保存

            var result = Result<OperationGroup>.SuccessResult("添加成功");
            result.Data = operationGroup;
            return result;
        }

        /// <summary>
        /// 更新操作分组
        /// </summary>
        /// <param name="newOperationGroup">操作分组对象</param>
        /// <returns>执行结果</returns>
        Result<OperationGroup> UpdateOperationGroup(OperationGroup newOperationGroup)
        {
            OperationGroup currentOperationGroup = Get(newOperationGroup.Id);
            if (currentOperationGroup == null)
            {
                return Result<OperationGroup>.FailedResult("没有指定要操作的分组信息");
            }
            //上级
            long newParentGroupId = newOperationGroup.Parent == null ? 0 : newOperationGroup.Parent.Id;
            long oldParentGroupId = currentOperationGroup.Parent == null ? 0 : currentOperationGroup.Parent.Id;
            //上级改变后 
            if (newParentGroupId != oldParentGroupId)
            {
                OperationGroup parentGroup = null;
                if (newParentGroupId > 0)
                {
                    IQuery parentQuery = QueryManager.Create<OperationGroupEntity>(c => c.Id == newParentGroupId);
                    parentGroup = operationGroupRepository.Get(parentQuery);
                    if (parentGroup == null)
                    {
                        return Result<OperationGroup>.FailedResult("请选择正确的上级分组");
                    }
                }
                currentOperationGroup.SetParentGroup(parentGroup);
            }
            //修改信息
            currentOperationGroup.Name = newOperationGroup.Name;
            currentOperationGroup.Remark = newOperationGroup.Remark;
            currentOperationGroup.Save();//保存

            var result = Result<OperationGroup>.SuccessResult("修改成功");
            result.Data = currentOperationGroup;
            return result;
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
            IQuery query = QueryManager.Create<OperationGroupEntity>(c => c.Id == groupId);
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
            IQuery query = QueryManager.Create<OperationGroupEntity>(c => groupIds.Contains(c.Id));
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
            var query = QueryManager.Create<OperationGroupEntity>(c => groupNames.Contains(c.Name));
            return GetList(query);
        }

        #endregion

        #region 获取操作分组分页

        /// <summary>
        /// 获取操作分组分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<OperationGroup> GetPaging(IQuery query)
        {
            var authorityOperationGroupPaging = operationGroupRepository.GetPaging(query);
            return authorityOperationGroupPaging;
        }

        /// <summary>
        /// 获取操作分组分页
        /// </summary>
        /// <param name="operationGroupFilter">操作分组筛选信息</param>
        /// <returns>返回操作分组分页</returns>
        public IPaging<OperationGroup> GetPaging(OperationGroupFilter operationGroupFilter)
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
            IQuery query = QueryManager.Create<OperationEntity>(c => c.Name == groupName && c.Id != excludeId);
            return operationGroupRepository.Exist(query);
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
            operationGroupRepository.Remove(QueryManager.Create<OperationGroupEntity>());
            //添加新的分组信息
            if (!operationGroups.IsNullOrEmpty())
            {
                //一级分组
                var levelOneGroups = operationGroups.Where(c => c.Parent == null);
                foreach (var group in levelOneGroups)
                {
                    InitializeSingleGroup(group, operationGroups);
                }
            }
            return Result.SuccessResult("初始化成功");
        }

        /// <summary>
        /// 初始化单个操作分组
        /// </summary>
        /// <param name="group">操作分组</param>
        /// <param name="allGroups">所有操作分组数据</param>
        /// <returns>返回执行结果</returns>
        Result InitializeSingleGroup(OperationGroup group, IEnumerable<OperationGroup> allGroups)
        {
            if (group == null)
            {
                return Result.FailedResult("分组信息为空");
            }
            var saveResult = Save(group);
            if (saveResult?.Success ?? false)
            {
                group = saveResult.Object;
                var childGroups = allGroups?.Where(c => c.Parent?.Name == group.Name);
                foreach (var childGroup in childGroups)
                {
                    childGroup.SetParentGroup(group);
                    InitializeSingleGroup(childGroup, childGroups);
                }
            }
            return saveResult;
        }

        #endregion
    }
}
