using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Develop.Domain;
using EZNEW.Domain.Sys.Model;

namespace EZNEW.Domain.Sys.Parameter
{
    /// <summary>
    /// 修改权限和操作功能绑定
    /// </summary>
    public class ModifyPermissionOperation : IDomainParameter
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<PermissionOperation> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<PermissionOperation> Unbindings { get; set; }
    }
}
