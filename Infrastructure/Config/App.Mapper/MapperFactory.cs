using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EZNEW.Framework.ObjectMap;

namespace App.Mapper
{
    public static class MapperFactory
    {
        public static IObjectMap CreateMapper(Action<IMapperConfigurationExpression> configuration)
        {
            if (configuration == null)
            {
                return null;
            }
            var autoMapper = new AutoMapMapper();
            autoMapper.Register(configuration);
            return autoMapper;
        }
    }
}
