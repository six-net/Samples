using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys
{
    /// <summary>
    /// 修改权限&操作绑定信息
    /// </summary>
    public class ModifyPermissionOperationViewModel
    {
        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<PermissionOperationViewModel> Bindings { get; set; }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<PermissionOperationViewModel> Unbindings { get; set; }
    }
}
