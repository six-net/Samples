using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 登录响应信息
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 获取或设置登录Token
        /// </summary>
        public string Token { get; set; }
    }
}
