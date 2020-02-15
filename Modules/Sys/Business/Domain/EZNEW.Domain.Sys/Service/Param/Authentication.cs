using EZNEW.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Domain.Sys.Service.Param
{
    /// <summary>
    /// 授权验证
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// 用户
        /// </summary>
        public User User
        {
            get;set;
        }

        /// <summary>
        /// 授权操作
        /// </summary>
        public AuthorityOperation Operation
        {
            get;set;
        }
    }
}
