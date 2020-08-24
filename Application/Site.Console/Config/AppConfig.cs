using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Cache;
using App.Database;
using EZNEW.Configuration;
using EZNEW.Mapper.Convention;

namespace Site.Console.Config
{
    public static class AppConfig
    {
        public static void Init()
        {
            //初始化系统配置
            ConfigurationManager.Init();
            //数据库配置
            DatabaseConfig.Configure();
            //Mvc配置
            MvcConfig.Configure();
            //缓存配置
            CacheConfig.Configure();
            //对象映射转换
            ConventionMapper.CreateMapper();
        }
    }
}
