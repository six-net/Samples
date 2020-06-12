using System;
using System.Collections.Generic;
using EZNEW.Module.Sys;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Develop.Command.Modify;
using EZNEW.Code;
using EZNEW.ValueType;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 授权操作组
    /// </summary>
    public class AuthorityOperationGroup : AggregationRoot<AuthorityOperationGroup>
    {
        #region	字段

        /// <summary>
        /// 上级
        /// </summary>
        protected LazyMember<AuthorityOperationGroup> parent;

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个操作分组对象
        /// </summary>
        internal AuthorityOperationGroup()
        {
            parent = new LazyMember<AuthorityOperationGroup>(LoadParentGroup);
            repository = this.Instance<IAuthorityOperationGroupRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 编号
        /// </summary>
        public long SysNo
        {
            get; set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get; set;
        }

        /// <summary>
        /// 上级
        /// </summary>
        public AuthorityOperationGroup Parent
        {
            get
            {
                return parent.Value;
            }
            protected set
            {
                parent.SetValue(value, false);
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get; set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityOperationGroupStatus Status
        {
            get; set;
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark
        {
            get; set;
        }

        #endregion

        #region 方法

        #region 功能方法

        #region 设置上级分组

        /// <summary>
        /// 设置上级分组
        /// </summary>
        /// <param name="parentGroup">上级分组</param>
        public void SetParentGroup(AuthorityOperationGroup parentGroup)
        {
            int parentLevel = 0;
            long parentSysNo = 0;
            if (parentGroup != null)
            {
                parentLevel = parentGroup.Level;
                parentSysNo = parentGroup.SysNo;
            }
            if (parentSysNo == SysNo && !IdentityValueIsNone())
            {
                throw new Exception("不能将分组本身设置为上级分组");
            }
            //排序
            IQuery sortQuery = QueryManager.Create<AuthorityOperationGroupQuery>(r => r.Parent == parentSysNo);
            sortQuery.AddQueryFields<AuthorityOperationGroupQuery>(c => c.Sort);
            int maxSortIndex = repository.Max<int>(sortQuery);
            Sort = maxSortIndex + 1;
            parent.SetValue(parentGroup, true);
            //等级
            int newLevel = parentLevel + 1;
            bool modifyChild = newLevel != Level;
            Level = newLevel;
            if (modifyChild)
            {
                //修改所有子集信息
                ModifyChildAuthorityGroupParentGroup();
            }
        }

        #endregion

        #region 初始化标识信息

        /// <summary>
        /// 初始化标识信息
        /// </summary>
        public override void InitIdentityValue()
        {
            base.InitIdentityValue();
            SysNo = GenerateAuthorityOperationGroupId();
        }

        #endregion

        #region 修改排序

        /// <summary>
        /// 修改排序
        /// </summary>
        /// <param name="newSort">新排序,排序编号必须大于0</param>
        public void ModifySort(int newSort)
        {
            if (newSort <= 0)
            {
                throw new Exception("请填写正确的排序编号");
            }
            Sort = newSort;
            //其它分组顺延
            IQuery sortQuery = QueryManager.Create<AuthorityOperationGroupQuery>(r => r.Parent == (parent.CurrentValue == null ? 0 : parent.CurrentValue.SysNo) && r.Sort >= newSort);
            IModify modifyExpression = ModifyFactory.Create();
            modifyExpression.Add<AuthorityOperationGroupQuery>(r => r.Sort, 1);
            repository.Modify(modifyExpression, sortQuery);
        }

        #endregion

        #endregion

        #region 内部方法

        #region 修改下级分组

        /// <summary>
        /// 修改下级分组
        /// </summary>
        void ModifyChildAuthorityGroupParentGroup()
        {
            if (IsNew)
            {
                return;
            }
            IQuery query = QueryManager.Create<AuthorityOperationGroupQuery>(r => r.Parent == SysNo);
            List<AuthorityOperationGroup> childGroupList = repository.GetList(query);
            foreach (var group in childGroupList)
            {
                group.SetParentGroup(this);
                group.Save();
            }
        }

        #endregion

        #region 加载上级分组

        /// <summary>
        /// 加载上级分组
        /// </summary>
        AuthorityOperationGroup LoadParentGroup()
        {
            if (!AllowLazyLoad(r => r.Parent))
            {
                return parent.CurrentValue;
            }
            if (Level <= 1 || parent.CurrentValue == null)
            {
                return parent.CurrentValue;
            }
            IQuery parentQuery = QueryManager.Create<AuthorityOperationGroupQuery>(r => r.SysNo == parent.CurrentValue.SysNo);
            return repository.Get(parentQuery);
        }

        #endregion

        #region 验证对象标识信息是否未设置

        /// <summary>
        /// 判断对象标识信息是否未设置
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return SysNo < 1;
        }

        #endregion

        #endregion

        #region 静态方法

        #region 生成分组对象编号

        /// <summary>
        /// 生成分组对象编号
        /// </summary>
        /// <returns></returns>
        public static long GenerateAuthorityOperationGroupId()
        {
            return SysManager.GetId(SysModuleObject.AuthorityOperationGroup);
        }

        #endregion

        #region 创建新的操作分组对象

        /// <summary>
        /// 创建操作分组对象
        /// </summary>
        /// <param name="sysNo">编号</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static AuthorityOperationGroup CreateAuthorityOperationGroup(long sysNo, string name = "")
        {
            var operationGroup = new AuthorityOperationGroup()
            {
                SysNo = sysNo,
                Name = name
            };
            return operationGroup;
        }

        protected override string GetIdentityValue()
        {
            return SysNo.ToString();
        }

        #endregion

        #endregion

        #endregion
    }
}