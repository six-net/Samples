using System;
using System.Collections.Generic;
using EZNEW.Cache;
using EZNEW.Cache.Redis;
using EZNEW.Data.Cache;

namespace AppConfig.Cache
{
    public static class CacheConfig
    {
        public static void Configure()
        {
            DataCacheManager.Configuration.ConfigureBehavior(context =>
            {
                return new DataCacheBehavior()
                {
                    ExceptionHandling = DataCacheExceptionHandling.Continue
                };
            });
            RedisManager.RegisterServer("redis", "127.0.0.1", 6379, true);
            CacheManager.Configuration.ThrowNoDatabaseException = true;
            CacheManager.Configuration.ConfigureCacheProvider(CacheServerType.Redis, new RedisProvider());
            //配置缓存服务器
            CacheManager.Configuration.ConfigureCacheServer(option =>
            {
                return new List<CacheServer>()
                {
                    new CacheServer("redis",CacheServerType.Redis){ Databases=new List<string>(){ "0" } }
                };
            });
        }
    }
}
