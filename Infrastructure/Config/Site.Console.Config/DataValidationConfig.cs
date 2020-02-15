using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EZNEW.DataValidation.Config;

namespace Site.Console.Config
{
    /// <summary>
    /// 数据验证配置
    /// </summary>
    public static class DataValidationConfig
    {
        #region 初始化

        public static void Init()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Config/Validation");
            InitFolderFiles(folderPath);
        }

        static void InitFolderFiles(string folderPath)
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
                InitFolderFiles(folder.FullName);
            }
        }

        #endregion
    }
}
