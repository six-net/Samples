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
        /// 用户编号
        /// </summary>
        [EntityField(Description = "用户编号", Role = FieldRole.PrimaryKey)]
        public long Id
        {
            get => GetPropertyValue<long>(nameof(Id));
            set => SetPropertyValue(nameof(Id), value);
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [EntityField(Description = "用户名")]
        public string UserName
        {
            get => GetPropertyValue<string>(nameof(UserName));
            set => SetPropertyValue(nameof(UserName), value);
        }

        /// <summary>
        /// 真实名称
        /// </summary>
        [EntityField(Description = "真实名称")]
        public string RealName
        {
            get => GetPropertyValue<string>(nameof(RealName));
            set => SetPropertyValue(nameof(RealName), value);
        }

        /// <summary>
        /// 密码
        /// </summary>
        [EntityField(Description = "密码")]
        public string Password
        {
            get => GetPropertyValue<string>(nameof(Password));
            set => SetPropertyValue(nameof(Password), value);
        }

        /// <summary>
        /// 类型
        /// </summary>
        [EntityField(Description = "类型")]
        public UserType UserType
        {
            get => GetPropertyValue<UserType>(nameof(UserType));
            set => SetPropertyValue(nameof(UserType), value);
        }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public UserStatus Status
        {
            get => GetPropertyValue<UserStatus>(nameof(Status));
            set => SetPropertyValue(nameof(Status), value);
        }

        /// <summary>
        /// 手机
        /// </summary>
        [EntityField(Description = "手机")]
        public string Mobile
        {
            get => GetPropertyValue<string>(nameof(Mobile));
            set => SetPropertyValue(nameof(Mobile), value);
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EntityField(Description = "邮箱")]
        public string Email
        {
            get => GetPropertyValue<string>(nameof(Email));
            set => SetPropertyValue(nameof(Email), value);
        }

        /// <summary>
        /// QQ
        /// </summary>
        [EntityField(Description = "QQ")]
        public string QQ
        {
            get => GetPropertyValue<string>(nameof(QQ));
            set => SetPropertyValue(nameof(QQ), value);
        }

        /// <summary>
        /// 超级管理员
        /// </summary>
        [EntityField(Description = "超级管理员")]
        public bool SuperUser
        {
            get => GetPropertyValue<bool>(nameof(SuperUser));
            set => SetPropertyValue(nameof(SuperUser), value);
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityField(Description = "添加时间")]
        public DateTime CreateDate
        {
            get => GetPropertyValue<DateTime>(nameof(CreateDate));
            set => SetPropertyValue(nameof(CreateDate), value);
        }
    }
}