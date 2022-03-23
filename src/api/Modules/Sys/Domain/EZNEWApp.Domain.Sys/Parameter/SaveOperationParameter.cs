using EZNEWApp.Domain.Sys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEWApp.Domain.Sys.Parameter
{
    public class SaveOperationParameter
    {
        /// <summary>
        /// 获取或设置要保存的操作信息
        /// </summary>
        public Operation Operation { get; set; }
    }
}
