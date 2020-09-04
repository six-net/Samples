using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EZNEW.Application;
using EZNEW.DataValidation;
using EZNEW.Web.Mvc.Display;

namespace Site.Console.Util
{
    /// <summary>
    /// Mvc配置
    /// </summary>
    public static class MvcConfig
    {
        /// <summary>
        /// 初始化Mvc配置
        /// </summary>
        public static void Configure()
        {
            //数据验证
            ConfigureDataValidation();
            //页面显示
            ConfigureDisplay();
        }

        /// <summary>
        /// 配置数据验证
        /// </summary>
        static void ConfigureDataValidation()
        {
            ValidationManager.ConfigureByConfigFile(ApplicationManager.ApplicationExecutableDirectory);
        }

        /// <summary>
        /// 配置视图显示
        /// </summary>
        static void ConfigureDisplay()
        {
            string folderPath = Path.Combine(ApplicationManager.ApplicationExecutableDirectory);
            DisplayManager.ConfigureByConfigFile(folderPath);
        }
    }
}
