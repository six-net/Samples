using App.DBConfig;
using App.Mapper;

namespace Site.Console.Config
{
    public static class AppConfig
    {
        public static void Init()
        {
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
