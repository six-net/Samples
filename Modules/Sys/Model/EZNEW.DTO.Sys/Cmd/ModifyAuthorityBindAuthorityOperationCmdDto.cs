using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 修改操作绑定的权限
    /// </summary>
    public class ModifyAuthorityBindAuthorityOperationCmdDto
    {
        #region 属性

        /// <summary>
        /// 绑定信息
        /// </summary>
        public IEnumerable<Tuple<AuthorityCmdDto, AuthorityOperationCmdDto>> Binds
        {
            get;set;
        }

        /// <summary>
        /// 解绑信息
        /// </summary>
        public IEnumerable<Tuple<AuthorityCmdDto, AuthorityOperationCmdDto>> UnBinds
        {
            get; set;
        }

        #endregion
    }
}
