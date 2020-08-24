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
    public class RemoveOperationGroupDto
    {
        /// <summary>
        /// 要删除的操作分组编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
