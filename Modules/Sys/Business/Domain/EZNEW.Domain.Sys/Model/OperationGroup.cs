using System;
using System.Collections.Generic;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Develop.CQuery;
using EZNEW.ValueType;
using EZNEW.Code;
using EZNEW.Develop.Command.Modify;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;
using EZNEW.Response;
using EZNEW.Domain.Sys.Service;
using EZNEW.DependencyInjection;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 授权操作组
    /// </summary>
    public class OperationGroup : AggregationRoot<OperationGroup>
    {
        //操作分组服务
        private static readonly IOperationGroupService operationGroupService = ContainerManager.Resolve<IOperationGroupService>();

        #region	字段

        /// <summary>
        /// 上级
        /// </summary>
        protected LazyMember<OperationGroup> parent;

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个操作分组对象
        /// </summary>
        internal OperationGroup()
        {
            parent = new LazyMember<OperationGroup>(LoadParent);
            repository = this.Instance<IOperationGroupRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public OperationGroup Parent
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
        /// 说明
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 内部方法

        #region 加载上级分组

        /// <summary>
        /// 加载上级分组
        /// </summary>
        OperationGroup LoadParent()
        {
            if (AllowLoad(r => r.Parent, parent))
            {
                return operationGroupService.Get(parent.CurrentValue.Id);
            }
            return parent.CurrentValue;
        }

        #endregion

        #region 获取对象标识值

        /// <summary>
        /// 获取对象标识值
        /// </summary>
        /// <returns></returns>
        protected override string GetIdentityValue()
        {
            return Id.ToString();
        }

        #endregion

        #region 更新对象时触发

        /// <summary>
        /// 更新对象时触发
        /// </summary>
        /// <param name="newData">新的数据对象</param>
        /// <returns>返回更新后的对象</returns>
        protected override OperationGroup OnUpdating(OperationGroup newData)
        {
            if (newData != null)
            {
                //修改上级分组
                long originalParentId = parent.CurrentValue?.Id ?? 0;
                SetParent(newData.Parent);
                long newParentId = parent.CurrentValue?.Id ?? 0;
                if (originalParentId != newParentId)
                {
                    //上级修改后重新设置排序
                    InitSort();
                }
                Name = newData.Name;
                Remark = newData.Remark;
            }
            return this;
        }

        #endregion

        #region 添加对象时触发

        /// <summary>
        /// 添加对象时触发
        /// </summary>
        /// <returns>返回要保存的对象</returns>
        protected override OperationGroup OnAdding()
        {
            base.OnAdding();
            //初始化排序信息
            InitSort();
            return this;
        }

        #endregion

        #region 初始化排序

        /// <summary>
        /// 初始化排序
        /// </summary>
        void InitSort()
        {
            var parentId = parent.CurrentValue?.Id ?? 0;
            IQuery sortQuery = QueryManager.Create<OperationGroupEntity>(r => r.Parent == parentId && r.Id != Id);
            sortQuery.AddQueryFields<OperationGroupEntity>(c => c.Sort);
            int maxSort = repository.Max<int>(sortQuery);
            Sort = maxSort + 1;
        }

        #endregion

        #endregion

        #region 静态方法

        #region 生成分组对象编号

        /// <summary>
        /// 生成操作分组对象编号
        /// </summary>
        /// <returns>返回操作分组编号</returns>
        public static long GenerateOperationGroupId()
        {
            return SysManager.GetId(SysModuleObject.OperationGroup);
        }

        #endregion

        #region 创建新的操作分组对象

        /// <summary>
        /// 创建操作分组对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static OperationGroup Create(long id, string name = "")
        {
            var operationGroup = new OperationGroup()
            {
                Id = id,
                Name = name
            };
            return operationGroup;
        }

        #endregion

        #endregion

        #region 功能方法

        #region 验证对象标识是否为空

        /// <summary>
        /// 验证对象标识是否为空
        /// </summary>
        /// <returns>返回标识值是否为空</returns>
        public override bool IdentityValueIsNone()
        {
            return Id < 1;
        }

        #endregion

        #region 设置上级分组

        /// <summary>
        /// 设置上级分组
        /// </summary>
        /// <param name="parentGroup">上级分组</param>
        public void SetParent(OperationGroup parentGroup)
        {
            long newParentId = 0;//新的上级编号
            long nowParentId = parent?.CurrentValue?.Id ?? 0;//当前上级编号
            bool identityHasValue = !IdentityValueIsNone();
            if (parentGroup != null)
            {
                newParentId = parentGroup.Id;
            }
            //上级相同不需要修改
            if (newParentId == nowParentId)
            {
                return;
            }
            //不能将对象本身设置为上级
            if (newParentId == Id && identityHasValue)
            {
                throw new Exception("不能将分组本身设置为上级分组");
            }
            //修改上级
            parent.SetValue(parentGroup, true);
        }

        #endregion

        #region 初始化标识信息

        /// <summary>
        /// 初始化标识信息
        /// </summary>
        public override void InitIdentityValue()
        {
            base.InitIdentityValue();
            Id = GenerateOperationGroupId();
        }

        #endregion

        #region 修改排序

        /// <summary>
        /// 修改排序
        /// </summary>
        /// <param name="newSort">新排序,排序编号必须大于0</param>
        public void ModifySort(int newSort)
        {
            if (newSort < 1)
            {
                throw new Exception("请填写正确的排序号");
            }
            Sort = newSort;
            //同级后面的数据排序顺延
            var parentId = parent.CurrentValue?.Id ?? 0;
            IQuery sortQuery = QueryManager.Create<OperationGroupEntity>(r => r.Parent == parentId && r.Sort >= newSort);
            IModify modifyExpression = ModifyFactory.Create();
            modifyExpression.Add<OperationGroupEntity>(r => r.Sort, 1);
            repository.Modify(modifyExpression, sortQuery);
        }

        #endregion

        #endregion
    }
}