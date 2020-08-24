using System;
using EZNEW.ValueType;
using EZNEW.Code;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Develop.CQuery;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 授权操作
    /// </summary>
    public class Operation : AggregationRoot<Operation>
    {
        #region	字段

        /// <summary>
        /// 操作分组
        /// </summary>
        protected LazyMember<OperationGroup> group;

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化授权操作对象
        /// </summary>
        private Operation()
        {
            group = new LazyMember<OperationGroup>(LoadAuthorityOperationGroup);
            repository = this.Instance<IOperationRepository>();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 主键编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerCode { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public OperationStatus Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 操作分组
        /// </summary>
        public OperationGroup Group
        {
            get
            {
                return group.Value;
            }
            protected set
            {
                group.SetValue(value, false);
            }
        }

        /// <summary>
        /// 访问级别
        /// </summary>
        public OperationAccessLevel AccessLevel { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 内部方法

        #region 加载操作对应的分组

        /// <summary>
        /// 加载操作对应的分组
        /// </summary>
        /// <returns></returns>
        OperationGroup LoadAuthorityOperationGroup()
        {
            if (!AllowLazyLoad(r => r.Group))
            {
                return group.CurrentValue;
            }
            if (group.CurrentValue == null || group.CurrentValue.Id <= 0)
            {
                return group.CurrentValue;
            }
            return this.Instance<IOperationGroupRepository>().Get(QueryManager.Create<OperationGroupEntity>(r => r.Id == group.CurrentValue.Id));
        }

        #endregion

        #region 保存数据验证

        /// <summary>
        /// 保存数据验证
        /// </summary>
        /// <returns></returns>
        protected override bool SaveValidation()
        {
            bool valResult = base.SaveValidation();
            if (!valResult)
            {
                return valResult;
            }
            if (group.CurrentValue == null || group.CurrentValue.Id < 1)
            {
                throw new Exception("请设置操作所属分组");
            }
            IQuery groupQuery = QueryManager.Create<OperationGroupEntity>(c => c.Id == group.CurrentValue.Id);
            if (!this.Instance<IOperationGroupRepository>().Exist(groupQuery))
            {
                throw new Exception("请设置正确的分组");
            }
            ActionCode = ActionCode?.ToUpper();
            ControllerCode = ControllerCode?.ToUpper();
            return true;
        }

        #endregion

        #region 验证对象标识信息是否未设置

        /// <summary>
        /// 判断对象标识信息是否未设置
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return Id < 1;
        }

        #endregion

        #region 获取对象的标识值

        /// <summary>
        /// 获取对象的标识值
        /// </summary>
        /// <returns>返回对象的标识值</returns>
        protected override string GetIdentityValue()
        {
            return Id.ToString();
        }

        #endregion

        #endregion

        #region 静态方法

        #region 生成一个操作编号

        /// <summary>
        /// 生成一个操作编号
        /// </summary>
        /// <returns>返回一个操作编号</returns>
        public static long GenerateOperationId()
        {
            return SysManager.GetId(SysModuleObject.Operation);
        }

        #endregion

        #region 创建授权操作

        /// <summary>
        /// 创建一个授权操作对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="controllerCode">控制编码</param>
        /// <param name="actionCode">方法编码</param>
        /// <returns>返回一个新的操作对象</returns>
        public static Operation Create(long id = 0, string name = "", string controllerCode = "", string actionCode = "")
        {
            id = id < 1 ? GenerateOperationId() : id;
            return new Operation()
            {
                Id = id,
                Name = name,
                ControllerCode = controllerCode,
                ActionCode = actionCode
            };
        }

        #endregion

        #endregion

        #region 功能方法

        #region 设置操作分组

        /// <summary>
        /// 设置操作分组
        /// </summary>
        /// <param name="group">分组信息</param>
        /// <param name="init">是否初始化</param>
        public void SetGroup(OperationGroup group, bool init = true)
        {
            this.group.SetValue(group, init);
        }

        #endregion

        #region 初始化标识信息

        /// <summary>
        /// 初始化标识信息
        /// </summary>
        public override void InitIdentityValue()
        {
            base.InitIdentityValue();
            Id = GenerateOperationId();
        }

        #endregion

        #endregion
    }
}