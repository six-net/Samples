using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EZNEW.DataValidation.Config;
using EZNEW.Mvc.CustomModelDisplayName.Config;

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
            DataValidationConfig();
            //显示验证
            DisplayNameConfig();
        }

        #region 数据验证配置

        /// <summary>
        /// 数据验证配置
        /// </summary>
        static void DataValidationConfig()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dvconfig", new EnumerationOptions()
            {
                RecurseSubdirectories = true
            });
            ValidationConfig.InitFromConfigFile(files);
        }

        #endregion

        #region 属性显示配置

        /// <summary>
        /// 属性显示配置
        /// </summary>
        static void DisplayNameConfig()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.disconfig", new EnumerationOptions()
            {
                RecurseSubdirectories = true
            });
            DisplayConfig.InitFromFiles(files);
        }

        #endregion
    }
}
