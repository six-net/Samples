using System.Collections.Generic;
using EZNEW.Model;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 获取菜单配置响应参数
    /// </summary>
    public class GetMenuConfigurationResponse
    {
        /// <summary>
        /// 菜单状态
        /// </summary>

        public KeyValueCollection<int,string> StatusCollection { get; set; }


        /// <summary>
        /// 菜单用途
        /// </summary>
        public KeyValueCollection<int, string> UsageCollection { get; set; }
    }
}
