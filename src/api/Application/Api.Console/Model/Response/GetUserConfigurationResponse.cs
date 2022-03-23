using EZNEW.Model;

namespace Api.Console.Model.Response
{
    /// <summary>
    /// 用户配置
    /// </summary>
    public class GetUserConfigurationResponse
    {
        /// <summary>
        /// 用户状态
        /// </summary>
        public KeyValueCollection<int, string> StatusCollection { get; set; }
    }
}
