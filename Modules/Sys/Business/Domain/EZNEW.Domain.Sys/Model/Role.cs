using EZNEW.Develop.CQuery;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Domain.Sys.Repository;
using EZNEW.Query.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Framework.Extension;
using EZNEW.Framework.ValueType;
using EZNEW.Framework;
using EZNEW.Application.Identity.User;
using EZNEW.Application.Identity;
using EZNEW.Framework.Code;
using Newtonsoft.Json;
using EZNEW.Develop.Command.Modify;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : AggregationRoot<Role>
    {
        #region	字段

        /// <summary>
        /// 角色编号
        /// </summary>
        protected long sysNo;

        /// <summary>
        /// 名称
        /// </summary>
        protected string name;

        /// <summary>
        /// 等级
        /// </summary>
        protected int level;

        /// <summary>
        /// 上级
        /// </summary>
        protected LazyMember<Role> parent;

        /// <summary>
        /// 排序
        /// </summary>
        protected int sort;

        /// <summary>
        /// 状态
        /// </summary>
        protected RoleStatus status;

        /// <summary>
        /// 添加时间
        /// </summary>
        protected DateTime createDate;

        /// <summary>
        /// 备注信息
        /// </summary>
        protected string remark;

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
        public long SysNo
        {
            get
            {
                return sysNo;
            }
            protected set
            {
                sysNo = value;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get
            {
                return level;
            }
            protected set
            {
                level = value;
            }
        }

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
        public int Sort
        {
            get
            {
                return sort;
            }
            protected set
            {
                sort = value;
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public RoleStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
            }
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        #endregion

        #region 方法

        #region 功能方法

        #region 设置上级角色

        /// <summary>
        /// 设置上级角色
        /// </summary>
        /// <param name="role">上级角色</param>
        public void SetParentRole(Role parentRole)
        {
            int parentLevel = 0;
            long parentSysNo = 0;
            if (parentRole != null)
            {
                parentLevel = parentRole.Level;
                parentSysNo = parentRole.SysNo;
            }
            if (parentSysNo == sysNo && !IdentityValueIsNone())
            {
                throw new Exception("不能将角色本身设置为自己的上级角色");
            }
            //排序
            IQuery sortQuery = QueryFactory.Create<RoleQuery>(r => r.Parent == parentSysNo);
            sortQuery.AddQueryFields<RoleQuery>(c => c.Sort);
            int maxSortIndex = repository.Max<int>(sortQuery);
            sort = maxSortIndex + 1;
            parent.SetValue(parentRole, true);
            //等级
            int newLevel = parentLevel + 1;
            bool modifyChild = newLevel != level;
            level = newLevel;
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
            if (newSort <= 0)
            {
                throw new Exception("请填写正确的角色排序");
            }
            sort = newSort;
            //其它角色顺延
            IQuery sortQuery = QueryFactory.Create<RoleQuery>(r => r.Parent == (parent.CurrentValue == null ? 0 : parent.CurrentValue.SysNo) && r.Sort >= newSort);
            IModify modifyExpression = ModifyFactory.Create();
            modifyExpression.Add<RoleQuery>(r => r.Sort, 1);
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
            sysNo = GenerateRoleId();
        }

        #endregion

        #endregion

        #region 内部方法

        #region 初始化对象

        /// <summary>
        /// 初始化对象
        /// </summary>
        void Initialization()
        {
            createDate = DateTime.Now;
            status = RoleStatus.正常;
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
            if (level <= 1 || parent.CurrentValue == null)
            {
                return null;
            }
            return repository.Get(QueryFactory.Create<RoleQuery>(r => r.SysNo == parent.CurrentValue.SysNo));
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
            IQuery query = QueryFactory.Create<RoleQuery>(r => r.Parent == SysNo);
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
            return sysNo <= 0;
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
            return SerialNumber.GetSerialNumber(IdentityApplicationHelper.GetIdGroupCode(IdentityGroup.角色));
        }

        #endregion

        #region 创建角色对象

        /// <summary>
        /// 创建一个角色对象
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="name">角色名</param>
        /// <returns>角色对象</returns>
        public static Role CreateRole(long roleId, string name = "")
        {
            var role = new Role()
            {
                SysNo = roleId,
                Name = name
            };
            return role;
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
