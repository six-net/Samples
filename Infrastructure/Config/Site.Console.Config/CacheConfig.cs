using EZNEW.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Console.Config
{
    public static class CacheConfig
    {
        public static void Init()
        {
            CacheManager.Config.GetCacheServerProxy = option =>
            {
                return new List<CacheServer>()
                {
                    new CacheServer()
                    {
                        ServerType=CacheServerType.InMemory
                    }
                };
            };
        }
    }
}
