using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppConfig;
using EZNEW.Application;
using EZNEW.DataValidation;

namespace Api.Console.Util
{
    public static class ApiConfig
    {
        public static void Init()
        {
            //数据库配置
            DatabaseConfig.Configure();
            //数据验证配置
            ConfigureDataValidation();
        }

        /// <summary>
        /// 配置数据验证
        /// </summary>
        static void ConfigureDataValidation()
        {
            ValidationManager.ConfigureByConfigFile(ApplicationManager.RootPath);
        }
    }
}
