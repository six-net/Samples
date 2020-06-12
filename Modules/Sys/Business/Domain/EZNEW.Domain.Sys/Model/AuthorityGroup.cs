using System;
using System.Collections.Generic;
using EZNEW.Module.Sys;
using EZNEW.Develop.Command.Modify;
using EZNEW.Code;
using EZNEW.ValueType;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Develop.CQuery;
using EZNEW.Query.Sys;
using EZNEW.Domain.Sys.Repository;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 权限分组
    /// </summary>
    public class AuthorityGroup : AggregationRoot<AuthorityGroup>
    {
        #region	字段

        /// <summary>
        /// 上级分组
        /// </summary>
        protected LazyMember<AuthorityGroup> parent;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化一个权限分组
        /// </summary>
        internal AuthorityGroup()
        {
            parent = new LazyMember<AuthorityGroup>(LoadParentGroup);
            repository = this.Instance<IAuthorityGroupRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 编号
        /// </summary>
        public long SysNo
        {
            get;
            protected set;
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
            get;
            protected set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public AuthorityGroupStatus Status
        {
            get; set;
        }

        /// <summary>
        /// 上级分组
        /// </summary>
        public AuthorityGroup Parent
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
        /// 分组等级
        /// </summary>
        public int Level
        {
            get;
            protected set;
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
        public void SetParentGroup(AuthorityGroup parentGroup)
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
            IQuery sortQuery = QueryManager.Create<AuthorityGroupQuery>(r => r.Parent == parentSysNo);
            sortQuery.AddQueryFields<AuthorityGroupQuery>(c => c.Sort);
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
            IQuery sortQuery = QueryManager.Create<AuthorityGroupQuery>(r => r.Parent == (parent.CurrentValue == null ? 0 : parent.CurrentValue.SysNo) && r.Sort >= newSort);
            IModify modifyExpression = ModifyFactory.Create();
            modifyExpression.Add<AuthorityGroupQuery>(r => r.Sort, 1);
            repository.Modify(modifyExpression, sortQuery);
        }

        #endregion

        #region 初始化标识信息

        /// <summary>
        /// 初始化标识信息
        /// </summary>
        public override void InitIdentityValue()
        {
            base.InitIdentityValue();
            SysNo = GenerateAuthorityGroupId();
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
            IQuery query = QueryManager.Create<AuthorityGroupQuery>(r => r.Parent == SysNo);
            List<AuthorityGroup> childGroupList = repository.GetList(query);
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
        AuthorityGroup LoadParentGroup()
        {
            if (!AllowLazyLoad(r => r.Parent))
            {
                return parent.CurrentValue;
            }
            if (Level <= 1 || parent.CurrentValue == null)
            {
                return null;
            }
            return repository.Get(QueryManager.Create<AuthorityGroupQuery>(r => r.SysNo == parent.CurrentValue.SysNo));
        }

        #endregion

        #region 验证对象标识信息是否未设置

        /// <summary>
        /// 判断对象标识信息是否未设置
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return SysNo <= 0;
        }

        #endregion

        #endregion

        #region 静态方法

        #region 生成分组编号

        /// <summary>
        /// 生成角色编号
        /// </summary>
        /// <returns></returns>
        public static long GenerateAuthorityGroupId()
        {
            return SysManager.GetId(SysModuleObject.AuthorityGroup);
        }

        #endregion

        #region 创建分组对象

        /// <summary>
        /// 创建分组对象
        /// </summary>
        /// <param name="sysNo">分组编号</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public static AuthorityGroup CreateAuthorityGroup(long sysNo, string name = "")
        {
            var authorityGroup = new AuthorityGroup()
            {
                SysNo = sysNo,
                Name = name
            };
            return authorityGroup;

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