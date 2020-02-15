using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Service.Param
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class UserLogin
    {
        #region 属性


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;set;
        }

        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Pwd
        {
            get;set;
        }

        #endregion
    }
}
