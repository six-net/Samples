using System;
using System.Collections.Generic;
using EZNEW.Development.Entity;
using EZNEWApp.Module.Sys;

namespace EZNEWApp.Domain.Sys.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    [Entity(ObjectName = "Sys_Role", Group = "Sys", Description = "角色")]
    public class Role : ModelRecordEntity<Role>
    {
        #region 属性

        /// <summary>
        /// 角色编号
        /// </summary>
        [EntityField(Description = "角色编号", Role = FieldRole.PrimaryKey)]
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public RoleStatus Status { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [EntityField(Description = "备注信息")]
        public string Remark { get; set; }

        #endregion

        #region 方法

        #endregion
    }
}