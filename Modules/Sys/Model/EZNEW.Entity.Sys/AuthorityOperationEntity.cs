using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 授权操作
    /// </summary>
    [Serializable]
    [Entity("Sys_AuthorityOperation", "Sys", "授权操作")]
    public class AuthorityOperationEntity : BaseEntity<AuthorityOperationEntity>
    {
        #region	字段

        /// <summary>
        /// 主键编号
        /// </summary>
        [EntityField(Description = "主键编号", PrimaryKey = true)]
        public long SysNo
        {
            get { return valueDict.GetValue<long>("SysNo"); }
            set { valueDict.SetValue("SysNo", value); }
        }

        /// <summary>
        /// 控制器
        /// </summary>
        [EntityField(Description = "控制器")]
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
        public int Sort
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
        /// 授权类型
        /// </summary>
        [EntityField(Description = "授权类型")]
        public int AuthorizeType
        {
            get { return valueDict.GetValue<int>("AuthorizeType"); }
            set { valueDict.SetValue("AuthorizeType", value); }
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

        #endregion
    }
}