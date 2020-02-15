using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 授权操作删除信息
    /// </summary>
    public class DeleteAuthorityOperationCmdDto
    {
        #region 属性

        /// <summary>
        /// 授权操作编号
        /// </summary>
        public IEnumerable<long> AuthorityOperationIds
        {
            get; set;
        }

        #endregion
    }
}
