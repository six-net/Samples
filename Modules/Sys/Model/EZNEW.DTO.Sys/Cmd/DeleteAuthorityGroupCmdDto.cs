using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.DTO.Sys.Cmd
{
    /// <summary>
    /// 删除角色分组信息
    /// </summary>
    public class DeleteAuthorityGroupCmdDto
    {
        #region 属性

        /// <summary>
        /// 分组编号
        /// </summary>
        public IEnumerable<long> AuthorityGroupIds
        {
            get;set;
        }

        #endregion
    }
}
