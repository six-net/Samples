using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.ViewModel.Sys.Request
{
    /// <summary>
    /// 修改权限&操作绑定信息
    /// </summary>
    public class ModifyAuthorityBindAuthorityOperationViewModel
    {
        #region 属性

        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<Tuple<EditAuthorityViewModel, EditAuthorityOperationViewModel>> Binds
        {
            get; set;
        }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<Tuple<EditAuthorityViewModel, EditAuthorityOperationViewModel>> UnBinds
        {
            get; set;
        }

        #endregion
    }
}
