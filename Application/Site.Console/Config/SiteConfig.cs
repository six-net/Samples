using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppConfig.Cache;
using AppConfig.Database;
using EZNEW.Configuration;
using EZNEW.Mapper.Convention;

namespace Site.Console.Config
{
    public static class SiteConfig
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
