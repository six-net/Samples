using EZNEW.Domain.Sys.Repository;
using EZNEW.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Domain.Sys.Model;
using EZNEW.Framework;
using EZNEW.Framework.Paging;
using EZNEW.Framework.Response;

namespace EZNEW.Domain.Sys.Service.Impl
{
    /// <summary>
    /// 删除操作分组
    /// </summary>
    public class AuthorityOperationGroupService : IAuthorityOperationGroupService
    {
        static readonly IAuthorityOperationGroupRepository authorityOperationGroupRepository = ContainerManager.Resolve<IAuthorityOperationGroupRepository>();

        #region 删除操作分组

        /// <summary>
        /// 删除操作分组
        /// </summary>
        /// <param name="deleteInfo">删除信息</param>
        public Result DeleteAuthorityOperationGroup(IEnumerable<long> groupIds)
        {
            #region 参数判断

            if (groupIds.IsNullOrEmpty())
            {
                return Result.FailedResult("没有指定任何要删除的信息");
            }

            #endregion

            //删除分组信息
            IQuery removeQuery = QueryFactory.Create<AuthorityOperationGroupQuery>(c => groupIds.Contains(c.SysNo));
            removeQuery.SetRecurve<AuthorityOperationGroupQuery>(c => c.SysNo, c => c.Parent);
            authorityOperationGroupRepository.Remove(removeQuery);
            return Result.SuccessResult("删除成功");
        }

        #endregion

        #region 保存授权操作组

        /// <summary>
        /// 保存授权操作组
        /// </summary>
        /// <param name="authorityOperationGroup">授权操作组对象</param>
        /// <returns>执行结果</returns>
        public Result<AuthorityOperationGroup> SaveAuthorityOperationGroup(AuthorityOperationGroup authorityOperationGroup)
        {
            if (authorityOperationGroup == null)
            {
                return Result<AuthorityOperationGroup>.FailedResult("操作分组信息不完整");
            }
            return authorityOperationGroup.SysNo > 0 ? UpdateAuthorityOperationGroup(authorityOperationGroup) : AddAuthorityOperationGroup(authorityOperationGroup);
        }

        /// <summary>
        /// 添加授权操作组
        /// </summary>
        /// <param name="authorityOperationGroup">授权操作组对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityOperationGroup> AddAuthorityOperationGroup(AuthorityOperationGroup authorityOperationGroup)
        {
            #region 上级

            long parentGroupId = authorityOperationGroup.Parent == null ? 0 : authorityOperationGroup.Parent.SysNo;
            AuthorityOperationGroup parentGroup = null;
            if (parentGroupId > 0)
            {
                IQuery parentQuery = QueryFactory.Create<AuthorityOperationGroupQuery>(c => c.SysNo == parentGroupId);
                parentGroup = authorityOperationGroupRepository.Get(parentQuery);
                if (parentGroup == null)
                {
                    return Result<AuthorityOperationGroup>.FailedResult("请选择正确的上级分组");
                }
            }
            authorityOperationGroup.SetParentGroup(parentGroup);

            #endregion

            authorityOperationGroup.Save();//保存

            var result = Result<AuthorityOperationGroup>.SuccessResult("添加成功");
            result.Data = authorityOperationGroup;
            return result;
        }

        /// <summary>
        /// 更新授权操作组
        /// </summary>
        /// <param name="newAuthorityOperationGroup">授权操作组对象</param>
        /// <returns>执行结果</returns>
        Result<AuthorityOperationGroup> UpdateAuthorityOperationGroup(AuthorityOperationGroup newAuthorityOperationGroup)
        {
            AuthorityOperationGroup authorityOperationGroup = GetAuthorityOperationGroup(newAuthorityOperationGroup.SysNo);
            if (authorityOperationGroup == null)
            {
                return Result<AuthorityOperationGroup>.FailedResult("没有指定要操作的分组信息");
            }
            //上级
            long newParentGroupId = newAuthorityOperationGroup.Parent == null ? 0 : newAuthorityOperationGroup.Parent.SysNo;
            long oldParentGroupId = authorityOperationGroup.Parent == null ? 0 : authorityOperationGroup.Parent.SysNo;
            //上级改变后 
            if (newParentGroupId != oldParentGroupId)
            {
                AuthorityOperationGroup parentGroup = null;
                if (newParentGroupId > 0)
                {
                    IQuery parentQuery = QueryFactory.Create<AuthorityOperationGroupQuery>(c => c.SysNo == newParentGroupId);
                    parentGroup = authorityOperationGroupRepository.Get(parentQuery);
                    if (parentGroup == null)
                    {
                        return Result<AuthorityOperationGroup>.FailedResult("请选择正确的上级分组");
                    }
                }
                authorityOperationGroup.SetParentGroup(parentGroup);
            }
            //修改信息
            authorityOperationGroup.Name = newAuthorityOperationGroup.Name;
            authorityOperationGroup.Status = newAuthorityOperationGroup.Status;
            authorityOperationGroup.Remark = newAuthorityOperationGroup.Remark;
            authorityOperationGroup.Save();//保存

            var result = Result<AuthorityOperationGroup>.SuccessResult("修改成功");
            result.Data = authorityOperationGroup;
            return result;
        }

        #endregion

        #region 获取授权操作组

        /// <summary>
        /// 获取授权操作组
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public AuthorityOperationGroup GetAuthorityOperationGroup(IQuery query)
        {
            var authorityOperationGroup = authorityOperationGroupRepository.Get(query);
            return authorityOperationGroup;
        }

        /// <summary>
        /// 获取授权操作分组
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns></returns>
        public AuthorityOperationGroup GetAuthorityOperationGroup(long groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }
            IQuery query = QueryFactory.Create<AuthorityOperationGroupQuery>(c => c.SysNo == groupId);
            return GetAuthorityOperationGroup(query);
        }

        #endregion

        #region 获取授权操作组列表

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public List<AuthorityOperationGroup> GetAuthorityOperationGroupList(IQuery query)
        {
            var authorityOperationGroupList = authorityOperationGroupRepository.GetList(query);
            return authorityOperationGroupList;
        }

        /// <summary>
        /// 获取授权操作组列表
        /// </summary>
        /// <param name="groupIds">分组编号</param>
        /// <returns></returns>
        public List<AuthorityOperationGroup> GetAuthorityOperationGroupList(IEnumerable<long> groupIds)
        {
            if (groupIds.IsNullOrEmpty())
            {
                return new List<AuthorityOperationGroup>(0);
            }
            IQuery query = QueryFactory.Create<AuthorityOperationGroupQuery>(c => groupIds.Contains(c.SysNo));
            return GetAuthorityOperationGroupList(query);
        }

        #endregion

        #region 获取授权操作组分页

        /// <summary>
        /// 获取授权操作组分页
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <returns></returns>
        public IPaging<AuthorityOperationGroup> GetAuthorityOperationGroupPaging(IQuery query)
        {
            var authorityOperationGroupPaging = authorityOperationGroupRepository.GetPaging(query);
            return authorityOperationGroupPaging;
        }

        #endregion

        #region 修改授权操作组排序

        /// <summary>
        /// 修改授权操作分组排序
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <param name="newSort">新的排序</param>
        /// <returns></returns>
        public Result ModifySort(long groupId, int newSort)
        {
            #region 参数判断

            if (groupId <= 0)
            {
                return Result.FailedResult("没有指定要修改的分组");
            }

            #endregion

            var group = GetAuthorityOperationGroup(groupId);
            if (group == null)
            {
                return Result.FailedResult("没有指定要修改的分组");
            }
            group.ModifySort(newSort);
            group.Save();
            return Result.SuccessResult("修改成功");
        }

        #endregion

        #region 检查操作分组名称是否可用

        /// <summary>
        /// 检查操作分组名称是否可用
        /// </summary>
        /// <param name="groupName">分组名称</param>
        /// <param name="excludeId">排除验证的分组编号</param>
        /// <returns></returns>
        public bool ExistGroupName(string groupName, long excludeId)
        {
            if (groupName.IsNullOrEmpty())
            {
                return false;
            }
            IQuery query = QueryFactory.Create<AuthorityOperationQuery>(c => c.Name == groupName && c.SysNo != excludeId);
            return authorityOperationGroupRepository.Exist(query);
        }

        #endregion
    }
}
