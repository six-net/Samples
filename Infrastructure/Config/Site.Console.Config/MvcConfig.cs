using App.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Console.Config
{
    /// <summary>
    /// mvc config
    /// </summary>
    public static class MvcConfig
    {
        public static void Config()
        {
            //数据验证
            DataValidationConfig.Init();
            //显示验证
            DisplayConfig.Init();
        }
    }
}
