using System;
using EZNEW.Develop.Entity;
using EZNEW.Framework.Extension;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    [Entity("Sys_Role", "Sys", "角色")]
    public class RoleEntity : BaseEntity<RoleEntity>
    {
        #region	字段

        /// <summary>
        /// 角色编号
        /// </summary>
        [EntityField(Description = "角色编号", PrimaryKey = true)]
        public long SysNo
        {
            get { return valueDict.GetValue<long>("SysNo"); }
            set { valueDict.SetValue("SysNo", value); }
        }

        /// <summary>
        /// 名称
        /// </summary>
        [EntityField(Description = "名称")]
        public string Name
        {
            get { return valueDict.GetValue<string>("Name"); }
            set { valueDict.SetValue("Name", value); }
        }

        /// <summary>
        /// 等级
        /// </summary>
        [EntityField(Description = "等级")]
        public int Level
        {
            get { return valueDict.GetValue<int>("Level"); }
            set { valueDict.SetValue("Level", value); }
        }

        /// <summary>
        /// 上级
        /// </summary>
        [EntityField(Description = "上级")]
        public long Parent
        {
            get { return valueDict.GetValue<long>("Parent"); }
            set { valueDict.SetValue("Parent", value); }
        }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort
        {
            get { return valueDict.GetValue<int>("Sort"); }
            set { valueDict.SetValue("Sort", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public int Status
        {
            get { return valueDict.GetValue<int>("Status"); }
            set { valueDict.SetValue("Status", value); }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityField(Description = "添加时间")]
        public DateTime CreateDate
        {
            get { return valueDict.GetValue<DateTime>("CreateDate"); }
            set { valueDict.SetValue("CreateDate", value); }
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        [EntityField(Description = "备注信息")]
        public string Remark
        {
            get { return valueDict.GetValue<string>("Remark"); }
            set { valueDict.SetValue("Remark", value); }
        }

        #endregion
    }
}