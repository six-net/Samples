using System;
using EZNEW.Module.Sys;

namespace EZNEW.ViewModel.Sys
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName => Status.GetEnumDisplayName();

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 超级用户
        /// </summary>
        public bool SuperUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最新登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }
    }
}
