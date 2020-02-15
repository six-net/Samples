using System;
using EZNEW.Develop.Entity;
using EZNEW.Framework.Extension;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 管理用户
    /// </summary>
    [Serializable]
    [Entity("Sys_User", "Sys", "管理用户")]
    public class UserEntity : BaseEntity<UserEntity>
    {
        #region	字段

        /// <summary>
        /// 用户编号
        /// </summary>
        [EntityField(Description = "用户编号", PrimaryKey = true)]
        public long SysNo
        {
            get { return valueDict.GetValue<long>("SysNo"); }
            set { valueDict.SetValue("SysNo", value); }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [EntityField(Description = "用户名")]
        public string UserName
        {
            get { return valueDict.GetValue<string>("UserName"); }
            set { valueDict.SetValue("UserName", value); }
        }

        /// <summary>
        /// 真实名称
        /// </summary>
        [EntityField(Description = "真实名称")]
        public string RealName
        {
            get { return valueDict.GetValue<string>("RealName"); }
            set { valueDict.SetValue("RealName", value); }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [EntityField(Description = "密码")]
        public string Pwd
        {
            get { return valueDict.GetValue<string>("Pwd"); }
            set { valueDict.SetValue("Pwd", value); }
        }

        /// <summary>
        /// 类型
        /// </summary>
        [EntityField(Description = "类型")]
        public int UserType
        {
            get { return valueDict.GetValue<int>("UserType"); }
            set { valueDict.SetValue("UserType", value); }
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
        /// 手机
        /// </summary>
        [EntityField(Description = "手机")]
        public string Mobile
        {
            get { return valueDict.GetValue<string>("Mobile"); }
            set { valueDict.SetValue("Mobile", value); }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EntityField(Description = "邮箱")]
        public string Email
        {
            get { return valueDict.GetValue<string>("Email"); }
            set { valueDict.SetValue("Email", value); }
        }

        /// <summary>
        /// QQ
        /// </summary>
        [EntityField(Description = "QQ")]
        public string QQ
        {
            get { return valueDict.GetValue<string>("QQ"); }
            set { valueDict.SetValue("QQ", value); }
        }

        /// <summary>
        /// 超级管理员
        /// </summary>
        [EntityField(Description = "超级管理员")]
        public bool SuperUser
        {
            get { return valueDict.GetValue<bool>("SuperUser"); }
            set { valueDict.SetValue("SuperUser", value); }
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
        /// 最近登录时间
        /// </summary>
        [EntityField(Description = "最近登录时间")]
        public DateTime LastLoginDate
        {
            get { return valueDict.GetValue<DateTime>("LastLoginDate"); }
            set { valueDict.SetValue("LastLoginDate", value); }
        }

        #endregion
    }
}