using AutoMapper;
using EZNEW.Code;
using EZNEW.Module.Sys;
using System;
using System.Collections.Generic;

namespace EZNEW.ModuleConfig.Sys
{
    public static class SysModuleConfig
    {
        /// <summary>
        /// 初始化模块配置
        /// </summary>
        /// <param name="autoMapperConfiguration">Automapper configuration</param>
        public static void Init(ref Action<IMapperConfigurationExpression> autoMapperConfiguration)
        {
            //对象转换
            if (autoMapperConfiguration == null)
            {
                autoMapperConfiguration = ObjectMapConfig.Configure;
            }
            else
            {
                autoMapperConfiguration += ObjectMapConfig.Configure;
            }
            //仓储配置
            RepositoryConfig.Init();
            //标识符生成配置
            SysManager.ConfigureIdentityKey();
            //领域事件配置
            DomainEventConfig.Init();
            //数据库配置
            DatabaseConfig.Init();
        }
    }
}
