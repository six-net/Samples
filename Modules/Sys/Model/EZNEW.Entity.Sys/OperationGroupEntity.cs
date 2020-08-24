using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 操作功能分组
    /// </summary>
    [Serializable]
    [Entity("Sys_OperationGroup", "Sys", "操作功能分组")]
    public class OperationGroupEntity : BaseEntity<OperationGroupEntity>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "编号", PrimaryKey = true)]
        public long Id
        {
            get { return valueDict.GetValue<long>("Id"); }
            set { valueDict.SetValue("Id", value); }
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
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int Sort
        {
            get { return valueDict.GetValue<int>("Sort"); }
            set { valueDict.SetValue("Sort", value); }
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
        /// 等级
        /// </summary>
        [EntityField(Description = "等级")]
        public int Level
        {
            get { return valueDict.GetValue<int>("Level"); }
            set { valueDict.SetValue("Level", value); }
        }

        /// <summary>
        /// 说明
        /// </summary>
        [EntityField(Description = "说明")]
        public string Remark
        {
            get { return valueDict.GetValue<string>("Remark"); }
            set { valueDict.SetValue("Remark", value); }
        }
    }
}