using System;
using System.Collections.Generic;
using EZNEW.Develop.Entity;
using EZNEW.Module.Sys;

namespace EZNEW.Entity.Sys
{
    /// <summary>
    /// 管理用户
    /// </summary>
    [Serializable]
    [Entity("Sys_User", "Sys", "管理用户")]
    public class UserEntity : BaseEntity<UserEntity>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [EntityField(Description = "用户名", CacheOption = EntityFieldCacheOption.CacheKey)]
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
        public string Password
        {
            get { return valueDict.GetValue<string>("Password"); }
            set { valueDict.SetValue("Password", value); }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        [EntityField(Description = "用户编号", PrimaryKey = true)]
        public long Id
        {
            get { return valueDict.GetValue<long>(nameof(Id)); }
            set { valueDict.SetValue(nameof(Id), value); }
        }

        /// <summary>
        /// 类型
        /// </summary>
        [EntityField(Description = "类型")]
        public UserType UserType
        {
            get { return valueDict.GetValue<UserType>("UserType"); }
            set { valueDict.SetValue("UserType", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public UserStatus Status
        {
            get { return valueDict.GetValue<UserStatus>("Status"); }
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
    }
}