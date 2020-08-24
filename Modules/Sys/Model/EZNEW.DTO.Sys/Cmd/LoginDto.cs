using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
    }
}
