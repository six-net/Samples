using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EZNEW.Develop.Entity;
using EZNEW.Module.Sys;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 操作功能
    /// </summary>
    [Serializable]
    [Entity("Sys_Operation", "Sys", "操作功能")]
    public class OperationEntity : BaseEntity<OperationEntity>
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        [EntityField(Description = "主键编号", PrimaryKey = true)]
        public long Id
        {
            get { return valueDict.GetValue<long>("Id"); }
            set { valueDict.SetValue("Id", value); }
        }

        /// <summary>
        /// 控制器
        /// </summary>
        [EntityField(Description = "控制器", IsRequired = true)]
        public string ControllerCode
        {
            get { return valueDict.GetValue<string>("ControllerCode"); }
            set { valueDict.SetValue("ControllerCode", value); }
        }

        /// <summary>
        /// 操作方法
        /// </summary>
        [EntityField(Description = "操作方法")]
        public string ActionCode
        {
            get { return valueDict.GetValue<string>("ActionCode"); }
            set { valueDict.SetValue("ActionCode", value); }
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
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public int Status
        {
            get { return valueDict.GetValue<int>("Status"); }
            set { valueDict.SetValue("Status", value); }
        }

        /// <summary>
        /// 排序
        /// </summary>
        [EntityField(Description = "排序")]
        public int? Sort
        {
            get { return valueDict.GetValue<int>("Sort"); }
            set { valueDict.SetValue("Sort", value); }
        }

        /// <summary>
        /// 操作分组
        /// </summary>
        [EntityField(Description = "操作分组")]
        public long Group
        {
            get { return valueDict.GetValue<long>("Group"); }
            set { valueDict.SetValue("Group", value); }
        }

        /// <summary>
        /// 访问级别
        /// </summary>
        [EntityField(Description = "访问级别")]
        public OperationAccessLevel AccessLevel
        {
            get { return valueDict.GetValue<OperationAccessLevel>("AccessLevel"); }
            set { valueDict.SetValue("AccessLevel", value); }
        }

        /// <summary>
        /// 方法描述
        /// </summary>
        [EntityField(Description = "方法描述")]
        public string Remark
        {
            get { return valueDict.GetValue<string>("Remark"); }
            set { valueDict.SetValue("Remark", value); }
        }
    }
}