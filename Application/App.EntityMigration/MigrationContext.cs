using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EZNEW.Entity.Sys;
using EZNEW.Module.Sys;
using EZNEW.Data;
using EZNEW.EntityMigration;
using Site.Console.Util;

namespace App.EntityMigration
{
    public class MigrationContext : EntityMigrationContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //configure database
            Site.Console.Program.CreateHostBuilder(Array.Empty<string>()).Build();
            SiteConfig.Init();
            DatabaseServer = DataManager.GetDatabaseServers(new MigrationCommand() { ObjectName = EntityMigrationManager.MigrationCommandObjectName })?.FirstOrDefault();
            DbContextOptionsBuilder contextOptionsBuilder = DatabaseServer.ServerType switch
            {
                DatabaseServerType.SQLServer => optionsBuilder.UseSqlServer(DatabaseServer.ConnectionString),
                DatabaseServerType.Oracle => optionsBuilder.UseOracle(DatabaseServer.ConnectionString),
                DatabaseServerType.MySQL => optionsBuilder.UseMySQL(DatabaseServer.ConnectionString),
                DatabaseServerType.SQLite => optionsBuilder.UseSqlite(DatabaseServer.ConnectionString),
                DatabaseServerType.PostgreSQL => optionsBuilder.UseNpgsql(DatabaseServer.ConnectionString),
                _ => null
            };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //add default data
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity()
            {
                Id = DateTime.Now.Ticks,
                UserName = "admin",
                RealName = "EZNEW.NET",
                Password = "21232f297a57a5a743894a0e4a801fc3",//admin
                UserType = UserType.Management,
                Status = UserStatus.Enable,
                CreateDate = DateTime.Now,
                Email = "admin@eznew.net",
                Mobile = "13600000000",
                QQ = "123456",
                SuperUser = true
            });
        }
    }
}
