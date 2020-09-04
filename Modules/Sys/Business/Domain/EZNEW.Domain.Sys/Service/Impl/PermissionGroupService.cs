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
    /// 权限分组服务
    /// </summary>
    public class PermissionGroupService : IPermissionGroupService
    {
        static readonly IPermissionGroupRepository permissionGroupRepository = ContainerManager.Resolve<IPermissionGroupRepository>();

        #region 保存权限分组

        /// <summary>
        /// 保存权限分组
        /// </summary>
        /// <param name="permissionGroup">权限分组对象</param>
        /// <returns>执行结果</returns>
        public Result<PermissionGroup> Save(PermissionGroup permissionGroup)
        {
            if (permissionGroup == null)
            {
                return Result<PermissionGroup>.FailedResult("没有指定任何要保存的分组信息");
            }
            if (permissionGroup.Id > 0)
            {
                return UpdatePermissionGroup(permissionGroup);
            }
            return AddPermissionGroup(permissionGroup);
        }

        /// <summary>
        /// 添加权限分组
        /// </summary>
        /// <param name="permissionGroup">权限信息</param>
        /// <returns>执行结果</returns>
        Result<PermissionGroup> AddPermissionGroup(PermissionGroup permissionGroup)
        {
            #region 上级

            long parentGroupId = permissionGroup.Parent == null ? 0 : permissionGroup.Parent.Id;
            PermissionGroup parentGroup = null;
            if (parentGroupId > 0)
            {
                IQuery parentQuery = QueryManager.Create<PermissionGroupEntity>(c => c.Id == parentGroupId);
                parentGroup = permissionGroupRepository.Get(parentQuery);
                if (parentGroup == null)
                {
                    return Result<PermissionGroup>.FailedResult("请选择正确的上级分组");
                }
            }
            permissionGroup.SetParent(parentGroup);

            #endregion

            permissionGroup.Save();//保存
            var result = Result<PermissionGroup>.SuccessResult("添加成功");
            result.Data = permissionGroup;
            return result;
        }

        /// <summary>
        /// 更新权限分组
        /// </summary>
        /// <param name="newPermissionGroup">权限信息</param>
        /// <returns>执行结果</returns>
        Result<PermissionGroup> UpdatePermissionGroup(PermissionGroup newPermissionGroup)
        {
            PermissionGroup currentPermissionGroup = permissionGroupRepository.Get(QueryManager.Create<PermissionGroupEntity>(r => r.Id == newPermissionGroup.Id));
            if (currentPermissionGroup == null)
            {
                return Result<PermissionGroup>.FailedResult("没有指定要操作的分组信息");
            }
            //上级
            long newParentGroupId = newPermissionGroup.Parent == null ? 0 : newPermissionGroup.Parent.Id;
            long oldParentGroupId = currentPermissionGroup.Parent == null ? 0 : currentPermissionGroup.Parent.Id;
            //上级改变后 
            if (newParentGroupId != oldParentGroupId)
            {
                PermissionGroup parentGroup = null;
                if (newParentGroupId > 0)
                {
                    IQuery parentQuery = QueryManager.Create<PermissionGroupEntity>(c => c.Id == newParentGroupId);
                    parentGroup = permissionGroupRepository.Get(parentQuery);
                    if (parentGroup == null)
                    {
                        return Result<PermissionGroup>.FailedResult("请选择正确的上级分组");
                    }
                }
                currentPermissionGroup.SetParent(parentGroup);
            }
            //修改信息
            currentPermissionGroup.Name = newPermissionGroup.Name;
            currentPermissionGroup.Remark = newPermissionGroup.Remark;
            currentPermissionGroup.Save();//保存
            var result = Result<PermissionGroup>.SuccessResult("更新成功");
            result.Data = currentPermissionGroup;
            return result;
        }

        #endregion

        #region 删除权限分组

        /// <summary>
        /// 删除权限分组
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        public Result Remove(IEnumerable<long> groupIds)
        {
            #region 参数判断

            if (groupIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的权限分组");
            }

            #endregion

            //删除分组信息
            IQuery removeQuery = QueryManager.Create<PermissionGroupEntity>(c => groupIds.Contains(c.Id));
            removeQuery.SetRecurve<PermissionGroupEntity>(c => c.Id, c => c.Parent);
            permissionGroupRepository.Remove(removeQuery);
            return Result.SuccessResult("删除成功");
        }

        #endregion

        #region 获取权限分组

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        PermissionGroup Get(IQuery query)
        {
            var currentPermissionGroup = permissionGroupRepository.Get(query);
            return currentPermissionGroup;
        }

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns></returns>
        public PermissionGroup Get(long groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }
            IQuery query = QueryManager.Create<PermissionGroupEntity>(c => c.Id == groupId);
            return Get(query);
        }

        /// <summary>
        /// 获取权限分组
        /// </summary>
        /// <param name="permissionGroupFilter">权限分组筛选信息</param>
        /// <returns>返回权限分组</returns>
        public PermissionGroup Get(PermissionGroupFilter permissionGroupFilter)
        {
            return Get(permissionGroupFilter?.CreateQuery());
        }

        #endregion

        #region 获取权限分组列表

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        List<PermissionGroup> GetList(IQuery query)
        {
            var currentPermissionGroupList = permissionGroupRepository.GetList(query);
            return currentPermissionGroupList;
        }

        /// <summary>
        /// 获取权限分组列表
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        public List<PermissionGroup> GetList(IEnumerable<long> groupIds)
        {
            if (groupIds.IsNullOrEmpty())
            {
                return new List<PermissionGroup>(0);
            }
            IQuery query = QueryManager.Create<PermissionGroupEntity>(c => groupIds.Contains(c.Id));
            return GetList(query);
        }

        /// <summary>
        /// 返回权限分组列表
        /// </summary>
        /// <param name="permissionGroupFilter">权限分组筛选信息</param>
        /// <returns>返回权限分组列表</returns>
        public List<PermissionGroup> GetList(PermissionGroupFilter permissionGroupFilter)
        {
            return GetList(permissionGroupFilter?.CreateQuery());
        }

        #endregion

        #region 获取权限分组分页

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        IPaging<PermissionGroup> GetPaging(IQuery query)
        {
            var currentPermissionGroupPaging = permissionGroupRepository.GetPaging(query);
            return currentPermissionGroupPaging;
        }

        /// <summary>
        /// 获取权限分组分页
        /// </summary>
        /// <param name="permissionGroupFilter">权限分组筛选信息</param>
        /// <returns>返回权限分组分页</returns>
        public IPaging<PermissionGroup> GetPaging(PermissionGroupFilter permissionGroupFilter)
        {
            return GetPaging(permissionGroupFilter?.CreateQuery());
        }

        #endregion

        #region 修改分组排序

        /// <summary>
        /// 修改分组排序
        /// </summary>
        /// <param name="sortIndexInfo">排序修改信息</param>
        /// <returns></returns>
        public Result ModifySort(long groupId, int newSort)
        {
            #region 参数判断

            if (groupId < 1)
            {
                return Result.FailedResult("没有指定要修改的分组");
            }

            #endregion

            PermissionGroup group = Get(groupId);
            if (group == null)
            {
                return Result.FailedResult("没有指定要修改的分组");
            }
            group.ModifySort(newSort);
            group.Save();
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 验证权限分组是否存在

        /// <summary>
        /// 验证权限分组是否存在
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns></returns>
        public bool Exist(long groupId)
        {
            if (groupId < 1)
            {
                return false;
            }
            IQuery query = QueryManager.Create<PermissionGroupEntity>(c => c.Id == groupId);
            return permissionGroupRepository.Exist(query);
        }

        #endregion

        #region 验证权限分组名是否存在

        /// <summary>
        /// 验证权限分组名是否存在
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <param name="excludeId">需要排除的权限分组编号</param>
        /// <returns></returns>
        public bool ExistName(string name, long excludeId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            IQuery query = QueryManager.Create<PermissionGroupEntity>(c => c.Name == name && c.Id != excludeId);
            return permissionGroupRepository.Exist(query);
        }

        #endregion
    }
}
