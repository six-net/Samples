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
    /// 修改权限和操作功能绑定
    /// </summary>
    public class ModifyOperationPermissionParameter : IDomainParameter
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<OperationPermission> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<OperationPermission> Unbindings { get; set; }
    }
}
