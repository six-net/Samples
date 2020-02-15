using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.ModuleConfig.Sys
{
    public static class SysModuleConfig
    {
        public static void Init(ref Action<IMapperConfigurationExpression> configuration)
        {
            //对象转换
            if (configuration == null)
            {
                configuration = new Action<IMapperConfigurationExpression>(SysModuleObjectMapper.Config);
            }
            else
            {
                configuration += SysModuleObjectMapper.Config;
            }
            //资源仓储配置
            SysModuleRepositoryConfig.Init();
            //对象标识符生成配置
            SysIdentityKeyConfig.Config();
            //领域事件配置
            DomainEventConfig.Init();
        }
    }
}
