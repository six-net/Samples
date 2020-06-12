using App.DBConfig;
using App.Mapper;
using EZNEW.Logging;

namespace Site.Console.Config
{
    public static class AppConfig
    {
        public static void Init()
        {
            //开启框架跟踪消息输出，建议只在测试环境启用
            TraceLogSwitchManager.EnableFrameworkTrace();

            //对象转换映射
            MapperFactory.CreateMapper();

            //数据库配置
            DbConfig.Init();

            //mvc配置
            MvcConfig.Config();

            //缓存配置
            CacheConfig.Init();
        }
    }
}
