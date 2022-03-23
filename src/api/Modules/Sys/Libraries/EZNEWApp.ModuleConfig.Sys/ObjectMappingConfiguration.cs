using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EZNEW.Mapper;

namespace EZNEWApp.ModuleConfig.Sys
{
    /// <summary>
    /// 对象映射配置
    /// </summary>
    public static class ObjectMappingConfiguration
    {
        public static void Configure()
        {
            ObjectMapper.ConfigureMap(cfg =>
            {
            });
        }
    }
}
