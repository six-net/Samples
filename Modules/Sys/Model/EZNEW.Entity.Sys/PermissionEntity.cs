using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;
using EZNEW.Module.Sys;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    [Entity("Sys_Permission", "Sys", "权限")]
    public class PermissionEntity : BaseEntity<PermissionEntity>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [EntityField(Description = "权限编码", PrimaryKey = true)]
        public long Id
        {
            get { return valueDict.GetValue<long>("Id"); }
            set { valueDict.SetValue("Id", value); }
        }

        /// <summary>
        /// 权限编码
        /// </summary>
        [EntityField(Description = "权限编码")]
        public string Code
        {
            get { return valueDict.GetValue<string>("Code"); }
            set { valueDict.SetValue("Code", value); }
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
        /// 权限类型
        /// </summary>
        [EntityField(Description = "权限类型")]
        public PermissionType Type
        {
            get { return valueDict.GetValue<PermissionType>("Type"); }
            set { valueDict.SetValue("Type", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public PermissionStatus Status
        {
            get { return valueDict.GetValue<PermissionStatus>("Status"); }
            set { valueDict.SetValue("Status", value); }
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
        /// 权限分组
        /// </summary>
        [EntityField(Description = "权限分组")]
        public long Group
        {
            get { return valueDict.GetValue<long>("Group"); }
            set { valueDict.SetValue("Group", value); }
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

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityField(Description = "添加时间")]
        public DateTime CreateDate
        {
            get { return valueDict.GetValue<DateTime>("CreateDate"); }
            set { valueDict.SetValue("CreateDate", value); }
        }
    }
}