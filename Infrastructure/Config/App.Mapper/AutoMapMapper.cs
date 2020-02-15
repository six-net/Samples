using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.Configuration;
using EZNEW.Framework.ValueType;
using EZNEW.Framework.Extension;
using EZNEW.Framework.ObjectMap;
using EZNEW.Framework.Serialize;
using AutoMapper;
using EZNEW.ModuleConfig.Sys;

namespace App.Mapper
{
    public class AutoMapMapper : IObjectMap
    {
        IMapper Mapper = null;

        /// <summary>
        /// 转换对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="sourceObj">源对象类型</param>
        /// <returns>目标对象类型</returns>
        public T MapTo<T>(object sourceObj)
        {
            return Mapper.Map<T>(sourceObj);
        }

        /// <summary>
        /// /// <summary>
        /// 注册对象映射
        /// </summary>
        /// </summary>
        public void Register(Action<IMapperConfigurationExpression> configuration)
        {
            var mapperConfiguration = new MapperConfiguration(configuration);
            // 开发时启用，发布时删除
            mapperConfiguration.AssertConfigurationIsValid();
            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}
