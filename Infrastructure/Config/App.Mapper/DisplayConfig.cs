using EZNEW.Mvc.CustomModelDisplayName;
using EZNEW.ViewModel.Sys.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App.Mapper
{
    /// <summary>
    /// 显示配置
    /// </summary>
    public static class DisplayConfig
    {
        public static void Init()
        {
            //配置文件
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Config/Display");
            InitFolderFiles(folderPath);

            //显示配置
            InitDisplay();
        }

        static void InitFolderFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return;
            }
            var files = Directory.GetFiles(folderPath).Where(c => Path.GetExtension(c).Trim('.').ToLower() == "disconfig").ToArray();
            EZNEW.Mvc.CustomModelDisplayName.Config.DisplayConfig.InitFromFiles(files);
            //下级目录
            var childFolders = new DirectoryInfo(folderPath).GetDirectories();
            foreach (var folder in childFolders)
            {
                InitFolderFiles(folder.FullName);
            }
        }

        static void InitDisplay()
        {
            DisplayManager.Add<EditRoleViewModel>(r => r.Name, "名称");
        }
    }
}
