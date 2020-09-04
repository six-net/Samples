using System;
using System.Collections.Generic;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Develop.CQuery;
using EZNEW.Domain.Sys.Repository;
using EZNEW.ValueType;
using EZNEW.Develop.Command.Modify;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;
using EZNEW.Response;
using EZNEW.Domain.Sys.Service;
using EZNEW.DependencyInjection;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 权限分组
    /// </summary>
    public class PermissionGroup : AggregationRoot<PermissionGroup>
    {
        //权限分组服务
        private static readonly IPermissionGroupService permissionGroupService = ContainerManager.Resolve<IPermissionGroupService>();

        #region	字段

        /// <summary>
        /// 上级分组
        /// </summary>
        protected LazyMember<PermissionGroup> parent;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化一个权限分组
        /// </summary>
        internal PermissionGroup()
        {
            parent = new LazyMember<PermissionGroup>(LoadParent);
            repository = this.Instance<IPermissionGroupRepository>();
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
        /// 上级分组
        /// </summary>
        public PermissionGroup Parent
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
        PermissionGroup LoadParent()
        {
            if (AllowLoad(c => c.Parent, parent))
            {
                return permissionGroupService.Get(parent.CurrentValue.Id);
            }
            return parent.CurrentValue;
        }

        #endregion

        #region 获取权限分组对象标识值

        /// <summary>
        /// 获取权限分组对象标识值
        /// </summary>
        /// <returns>返回标识值</returns>
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
        protected override PermissionGroup OnUpdating(PermissionGroup newData)
        {
            if (newData != null)
            {
                //修改上级分组
                var originalParentId = parent?.CurrentValue?.Id ?? 0;
                SetParent(newData.Parent);
                var newParentId = parent?.CurrentValue?.Id ?? 0;
                if (originalParentId != newParentId)
                {
                    //修改上级后重新修改排序
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
        /// <returns>返回要保存的对象值</returns>
        protected override PermissionGroup OnAdding()
        {
            base.OnAdding();
            InitSort();//初始化排序
            return this;
        }

        #endregion

        #endregion

        #region 静态方法

        #region 生成分组编号

        /// <summary>
        /// 生成角色编号
        /// </summary>
        /// <returns></returns>
        public static long GeneratePermissionGroupId()
        {
            return SysManager.GetId(SysModuleObject.PermissionGroup);
        }

        #endregion

        #region 创建分组对象

        /// <summary>
        /// 创建分组对象
        /// </summary>
        /// <param name="id">分组编号</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public static PermissionGroup Create(long id, string name = "")
        {
            var authorityGroup = new PermissionGroup()
            {
                Id = id,
                Name = name
            };
            return authorityGroup;
        }

        #endregion

        #endregion

        #region 功能方法

        #region 验证对象标识信息是否为空

        /// <summary>
        /// 判断对象标识信息是否未设置
        /// </summary>
        /// <returns></returns>
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
        public void SetParent(PermissionGroup parentGroup)
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

        /// <summary>
        /// 初始化排序
        /// </summary>
        void InitSort()
        {
            var parentId = parent.CurrentValue?.Id ?? 0;
            IQuery sortQuery = QueryManager.Create<PermissionGroupEntity>(r => r.Parent == parentId && r.Id != Id);
            sortQuery.AddQueryFields<PermissionGroupEntity>(c => c.Sort);
            int maxSort = repository.Max<int>(sortQuery);
            Sort = maxSort + 1;
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
                throw new Exception("请填写正确的排序号");
            }
            Sort = newSort;
            //同级其它数据顺延
            var parentId = parent.CurrentValue?.Id ?? 0;
            IQuery sortQuery = QueryManager.Create<PermissionGroupEntity>(r => r.Parent == parentId && r.Sort >= newSort);
            IModify modifyExpression = ModifyFactory.Create();
            modifyExpression.Add<PermissionGroupEntity>(r => r.Sort, 1);
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
            Id = GeneratePermissionGroupId();
        }

        #endregion

        #endregion
    }
}