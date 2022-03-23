using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Model.Request
{
    /// <summary>
    /// 定义登录信息
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
    }
}
