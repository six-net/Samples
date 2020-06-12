using System;
using AutoMapper;
using EZNEW.ModuleConfig.Sys;

namespace App.Mapper
{
    public static class MapperFactory
    {
        public static EZNEW.Mapper.IMapper CreateMapper()
        {
            Action<IMapperConfigurationExpression> configuration = ObjectMapConfig;
            ModuleConfig(ref configuration);
            var autoMapper = new AutoMapMapper();
            autoMapper.Register(configuration);
            EZNEW.Mapper.ObjectMapper.Current = autoMapper;
            return autoMapper;
        }

        /// <summary>
        /// 对象转换映射配置
        /// </summary>
        /// <param name="expression"></param>
        static void ObjectMapConfig(IMapperConfigurationExpression expression)
        {
            //TODO
        }

        /// <summary>
        /// 功能模块配置
        /// </summary>
        /// <param name="configuration"></param>
        static void ModuleConfig(ref Action<IMapperConfigurationExpression> configuration)
        {
            #region Sys

            SysModuleConfig.Init(ref configuration);

            #endregion
        }
    }
}
