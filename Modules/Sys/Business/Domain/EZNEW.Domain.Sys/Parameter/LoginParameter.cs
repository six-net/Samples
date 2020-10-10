using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Develop.Domain;

namespace EZNEW.Domain.Sys.Parameter
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
    }
}
