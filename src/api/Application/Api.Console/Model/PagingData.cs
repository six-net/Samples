using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Console.Model
{
    public class PagingData<T>
    {
        /// <summary>
        /// 获取或设置数据列表
        /// </summary>
        public IEnumerable<T> List { get; set; }

        /// <summary>
        /// 获取或设置数据总量
        /// </summary>
        public long Total { get; set; }
    }
}
