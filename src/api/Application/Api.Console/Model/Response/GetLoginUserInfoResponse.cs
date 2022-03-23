using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    public class GetLoginUserInfoResponse
    {
        /// <summary>
        /// 用户角色
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public List<string> Permissions { get; set; }

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
