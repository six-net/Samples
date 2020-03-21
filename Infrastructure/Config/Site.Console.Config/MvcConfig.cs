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
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Config/Validation");
            InitDataValidation(folderPath);
        }

        /// <summary>
        /// 初始化数据验证
        /// </summary>
        /// <param name="folderPath">文件路径</param>
        static void InitDataValidation(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return;
            }
            var files = Directory.GetFiles(folderPath).Where(c => Path.GetExtension(c).Trim('.').ToLower() == "dvconfig").ToArray();
            ValidationConfig.InitFromConfigFile(files);
            //下级目录
            var childFolders = new DirectoryInfo(folderPath).GetDirectories();
            foreach (var folder in childFolders)
            {
                InitDataValidation(folder.FullName);
            }
        }

        #endregion

        #region 属性显示配置

        /// <summary>
        /// 属性显示配置
        /// </summary>
        static void DisplayNameConfig()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Config/Display");
            InitDisplayConfig(folderPath);
        }

        /// <summary>
        /// 初始化属性显示配置
        /// </summary>
        /// <param name="folderPath">文件路径</param>
        static void InitDisplayConfig(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return;
            }
            var files = Directory.GetFiles(folderPath).Where(c => Path.GetExtension(c).Trim('.').ToLower() == "disconfig").ToArray();
            DisplayConfig.InitFromFiles(files);
            //下级目录
            var childFolders = new DirectoryInfo(folderPath).GetDirectories();
            foreach (var folder in childFolders)
            {
                InitDisplayConfig(folder.FullName);
            }
        }

        #endregion
    }
}
