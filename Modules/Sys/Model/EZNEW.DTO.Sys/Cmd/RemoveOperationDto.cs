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
    public class RemoveOperationDto
    {
        /// <summary>
        /// 要删除的授权操作编号
        /// </summary>
        public IEnumerable<long> Ids { get; set; }
    }
}
