using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using EZNEW.Data.SqlServer;
using EZNEW.Data;
using EZNEW.DependencyInjection;

namespace App.Database
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DatabaseConfig
    {
        public static void Configure()
        {
            //配置数据库执行器
            ConfigureDatabaseEngine();
            //配置数据库服务器
            ConfigureDatabaseServer();
        }

        /// <summary>
        /// 配置数据库执行器
        /// </summary>
        static void ConfigureDatabaseEngine()
        {
            DataManager.ConfigureDatabaseEngine(DatabaseServerType.SQLServer, new SqlServerEngine());
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
                        ServerType = DatabaseServerType.SQLServer,
                        ConnectionString = ContainerManager.Resolve<IConfiguration>().GetConnectionString("DefaultConnection")
                    }
                };
            });

        }
    }
}
