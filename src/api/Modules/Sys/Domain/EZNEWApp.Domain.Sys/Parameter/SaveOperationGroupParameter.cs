using System;
using System.Collections.Generic;
using System.Text;
using EZNEWApp.Domain.Sys.Model;

namespace EZNEWApp.Domain.Sys.Parameter
{
    /// <summary>
    /// 保存操作分组信息
    /// </summary>
    public class SaveOperationGroupParameter
    {
        /// <summary>
        /// 授权操作分组信息
        /// </summary>
        public OperationGroup OperationGroup { get; set; }
    }
}
