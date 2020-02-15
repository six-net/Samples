using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EZNEW.DataValidation.Config;
using EZNEW.Develop.DataValidation;
using EZNEW.Domain.Sys.Model;

namespace App.Mapper
{
    /// <summary>
    /// 数据验证配置
    /// </summary>
    public static class DataValidationConfig
    {
        #region 初始化

        public static void Init()
        {
            //文件配置
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Config/Validation");
            InitConfigValidation(folderPath);
            //代码配置
            //InitValidation();
        }

        static void InitConfigValidation(string folderPath)
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
                InitConfigValidation(folder.FullName);
            }
        }

        static void InitValidation()
        {
            ValidationManager.StringLength<Role>(5, 1, new ValidationField<Role>()
            {
                FieldExpression = r => r.Name,
                ErrorMessage = "角色名称最大不能超过5个字符"
            }, new ValidationField<Role>()
            {
                FieldExpression = r => r.Remark,
                ErrorMessage = "角色备注最大不能超过5个字符"
            });
            //ValidationManager.Equal<Role>(100, new ValidationField<Role>()
            //{
            //    FieldExpression = r => r.SortIndex,
            //    ErrorMessage = "角色排序必须等于100"
            //});

            //ValidationManager.Equal<Role>(r=>r.Level, new ValidationField<Role>()
            //{
            //    FieldExpression = r => r.SortIndex,
            //    ErrorMessage = "角色排序必须等于角色等级"
            //});

            //ValidationManager.In<Role>(new List<dynamic>() { 2, 3, 4 }, new ValidationField<Role>()
            //{
            //    FieldExpression = r => r.Level,
            //    ErrorMessage = "只能添加2，3，4级角色"
            //});

            ValidationManager.Range<Role>(DateTime.Parse("2015-1-1"),DateTime.Parse("2016-12-31"),fields:new ValidationField<Role>()
            {
                FieldExpression=r=>r.CreateDate,
                ErrorMessage="当前时间内不能添加角色"
            });
        }

        #endregion
    }
}
