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
using EZNEW.Response;

namespace EZNEW.Domain.Sys.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : AggregationRoot<Role>
    {
        #region 构造方法

        /// <summary>
        /// 初始化一个角色
        /// </summary>
        internal Role()
        {
            repository = this.Instance<IRoleRepository>();
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

        #region 更新对象时触发

        /// <summary>
        /// 更新对象时触发
        /// </summary>
        /// <param name="newData">新的对象值</param>
        /// <returns>返回更新后的对象</returns>
        protected override Role OnUpdating(Role newData)
        {
            if (newData != null)
            {
                Name = newData.Name;
                Status = newData.Status;
                Remark = newData.Remark ?? string.Empty;
            }
            return this;
        }

        #endregion

        #region 添加对象时触发

        /// <summary>
        /// 添加对象时触发
        /// </summary>
        /// <returns>返回要保存的对象值</returns>
        protected override Role OnAdding()
        {
            var saveData = base.OnAdding();
            saveData.CreateDate = DateTime.Now;
            return saveData;
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

        #region 对象标识值是否无效

        /// <summary>
        /// 对象标识值是否无效
        /// </summary>
        /// <returns></returns>
        public override bool IdentityValueIsNone()
        {
            return Id < 1;
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
