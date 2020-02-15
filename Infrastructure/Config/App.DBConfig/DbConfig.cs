using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZNEW.Develop.Command;
using Microsoft.Extensions.Configuration;
using EZNEW.Framework.IoC;
using EZNEW.Data.SqlServer;
using EZNEW.Data;
using EZNEW.Data.Config;
using EZNEW.Develop.CQuery;

namespace App.DBConfig
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbConfig
    {
        public static void Init()
        {
            DataBaseEngineConfig();//数据库执行器
            DataManager.GetDBServerAsync = GetServerInfoAsync;//获取数据连接信息方法
        }

        /// <summary>
        /// 数据库执行器配置
        /// </summary>
        static void DataBaseEngineConfig()
        {
            DataManager.RegisterDBEngine(ServerType.SQLServer, new SqlServerEngine());
        }

        /// <summary>
        /// 获取数据库服务器
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        static async Task<List<ServerInfo>> GetServerInfoAsync(ICommand command)
        {
            List<ServerInfo> servers = new List<ServerInfo>();
            servers.Add(new ServerInfo()
            {
                ServerType = ServerType.SQLServer,
                ConnectionString = ContainerManager.Resolve<IConfiguration>().GetConnectionString("DefaultConnection")
            });
            return await Task.FromResult(servers);
        }
    }
}
