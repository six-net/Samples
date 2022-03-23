using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Development.Domain;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 检查操作授权
    /// </summary>
    public class CheckAuthorizationParameter : IDomainParameter
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 授权操作
        /// </summary>
        public Operation Operation { get; set; }
    }
}
