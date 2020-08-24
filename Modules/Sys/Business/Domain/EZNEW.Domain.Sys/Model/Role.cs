using System;
using System.Collections.Generic;
using EZNEW.ValueType;
using EZNEW.Code;
using EZNEW.Develop.Command.Modify;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : AggregationRoot<Role>
    {
        #region	字段

        /// <summary>
        /// 上级
        /// </summary>
        protected LazyMember<Role> parent;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化一个角色
        /// </summary>
        internal Role()
        {
            Initialization();
        }

        #endregion

        #region	属性

        /// <summary>
        /// 角色编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public Role Parent
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
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public RoleStatus Status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 内部方法

        #region 初始化对象

        /// <summary>
        /// 初始化对象
        /// </summary>
        void Initialization()
        {
            CreateDate = DateTime.Now;
            Status = RoleStatus.Enable;
            parent = new LazyMember<Role>(LoadParentRole);
            repository = this.Instance<IRoleRepository>();
        }

        #endregion

        #region 加载上级角色

        /// <summary>
        /// 加载上级角色
        /// </summary>
        /// <returns></returns>
        Role LoadParentRole()
        {
            if (!AllowLazyLoad(r => r.Parent))
            {
                return parent.CurrentValue;
            }
            if (Level <= 1 || parent.CurrentValue == null)
            {
                return null;
            }
            return repository.Get(QueryManager.Create<RoleEntity>(r => r.Id == parent.CurrentValue.Id));
        }

        #endregion

        #region 修改子集信息

        /// <summary>
        /// 修改子集信息
        /// </summary>
        void ModifyChildRole()
        {
            if (IsNew)
            {
                return;
            }
            IQuery query = QueryManager.Create<RoleEntity>(r => r.Parent == Id);
            List<Role> childRoleList = repository.GetList(query);
            foreach (var role in childRoleList)
            {
                role.SetParentRole(this);
                role.Save();
            }
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

        #region 获取对象标识信息

        /// <summary>
        /// 获取对象标识信息
        /// </summary>
        /// <returns>返回对象标识</returns>
        protected override string GetIdentityValue()
        {
            return Id.ToString();
        }

        #endregion

        #endregion

        #region 静态方法

        #region 生成角色编号

        /// <summary>
        /// 生成角色编号
        /// </summary>
        /// <returns></returns>
        public static long GenerateRoleId()
        {
            return SysManager.GetId(SysModuleObject.Role);
        }

        #endregion

        #region 创建角色对象

        /// <summary>
        /// 创建一个角色对象
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="name">角色名</param>
        /// <returns>角色对象</returns>
        public static Role Create(long roleId, string name = "")
        {
            var role = new Role()
            {
                Id = roleId,
                Name = name
            };
            return role;
        }

        #endregion

        #endregion

        #region 功能方法

        #region 设置上级角色

        /// <summary>
        /// 设置上级角色
        /// </summary>
        /// <param name="role">上级角色</param>
        public void SetParentRole(Role parentRole)
        {
            int parentLevel = 0;
            long parentId = 0;
            if (parentRole != null)
            {
                parentLevel = parentRole.Level;
                parentId = parentRole.Id;
            }
            if (parentId == Id && !IdentityValueIsNone())
            {
                throw new Exception("不能将角色本身设置为自己的上级角色");
            }
            //排序
            IQuery sortQuery = QueryManager.Create<RoleEntity>(r => r.Parent == parentId);
            sortQuery.AddQueryFields<RoleEntity>(c => c.Sort);
            int maxSortIndex = repository.Max<int>(sortQuery);
            Sort = maxSortIndex + 1;
            parent.SetValue(parentRole, true);
            //等级
            int newLevel = parentLevel + 1;
            bool modifyChild = newLevel != Level;
            Level = newLevel;
            if (modifyChild)
            {
                //修改所有子集信息
                ModifyChildRole();
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
            if (newSort < 1)
            {
                throw new Exception("请填写正确的角色排序");
            }
            Sort = newSort;
            //其它角色顺延
            IQuery sortQuery = QueryManager.Create<RoleEntity>(r => r.Parent == (parent.CurrentValue == null ? 0 : parent.CurrentValue.Id) && r.Sort >= newSort);
            IModify modifyExpression = ModifyFactory.Create();
            modifyExpression.Add<RoleEntity>(r => r.Sort, 1);
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
            Id = GenerateRoleId();
        }

        #endregion

        #endregion
    }
}
