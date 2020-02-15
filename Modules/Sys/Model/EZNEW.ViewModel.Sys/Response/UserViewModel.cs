using EZNEW.Application.Identity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EZNEW.ViewModel.Sys.Response
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserViewModel
    {
        #region	属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public long SysNo
        {
            get;
            set;
        }

        /// <summary>
        /// 登录名
        /// </summary>
        [Remote("CheckUserName", "Sys", ErrorMessage = "登陆名已存在", HttpMethod = "post")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public UserType UserType
        {
            get;
            set;
        }

        /// <summary>
        /// 真实名称
        /// </summary>
        public string RealName
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            get;
            set;
        }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status
        {
            get;
            set;
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get; set;
        }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get; set;
        }

        public string QQ
        {
            get; set;
        }

        /// <summary>
        /// 超级用户
        /// </summary>
        public bool SuperUser
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 最新登录时间
        /// </summary>
        public DateTime LastLoginDate
        {
            get;
            set;
        }

        #endregion
    }
}
