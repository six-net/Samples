using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Development.Domain;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class LoginParameter : IDomainParameter
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }
    }
}
