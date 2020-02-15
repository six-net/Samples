using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 删除操作分组信息
    /// </summary>
    public class DeleteAuthorityOperationGroupCmdDto
    {
        #region 属性

        /// <summary>
        /// 分组编号
        /// </summary>
        public IEnumerable<long> AuthorityOperationGroupIds
        {
            get; set;
        }

        #endregion
    }
}
