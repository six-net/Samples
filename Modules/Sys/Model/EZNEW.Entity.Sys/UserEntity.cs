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
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [EntityField(Description = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        [EntityField(Description = "真实名称")]
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [EntityField(Description = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [EntityField(Description = "类型")]
        public UserType UserType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [EntityField(Description = "状态")]
        public UserStatus Status { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [EntityField(Description = "手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [EntityField(Description = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [EntityField(Description = "QQ")]
        public string QQ { get; set; }

        /// <summary>
        /// 超级管理员
        /// </summary>
        [EntityField(Description = "超级管理员")]
        public bool SuperUser { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityField(Description = "添加时间")]
        public DateTime CreateDate { get; set; }
    }
}