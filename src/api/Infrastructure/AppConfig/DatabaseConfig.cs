using System;
using System.Collections.Generic;
using System.Text;
using EZNEW.Configuration;
using EZNEW.Data;
using EZNEW.Data.MySQL;

namespace AppConfig
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public static class DatabaseConfig
    {
        public static void Configure()
        {
            //配置数据库执行器
            ConfigureDatabaseProvider();
            //配置数据库服务器
            ConfigureDatabaseServer();
        }

        /// <summary>
        /// 配置数据库执行器
        /// </summary>
        static void ConfigureDatabaseProvider()
        {
            DataManager.ConfigureDatabaseProvider(DatabaseServerType.MySQL, new MySqlProvider());
        }

        /// <summary>
        /// 配置数据库服务器
        /// </summary>
        static void ConfigureDatabaseServer()
        {
            DataManager.ConfigureDatabaseServer(command =>
            {
                return new List<DatabaseServer>
                {
                    new DatabaseServer()
                    {
                        ServerType = DatabaseServerType.MySQL,
                        ConnectionString = ConfigurationManager.GetConnectionString("DefaultConnection")
                    }
                };
            });

        }
    }
}
