using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EZNEWApp.Module.Sys;
using EZNEW.Data;
using EZNEW.EntityMigration;
using EZNEW.Configuration;
using EZNEWApp.Domain.Sys.Model;

namespace App.EntityMigration
{
    public class MigrationContext : EntityMigrationContext
    {
        protected override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            _ = DatabaseServer.ServerType switch
            {
                DatabaseServerType.SQLServer => optionsBuilder.UseSqlServer(DatabaseServer.ConnectionString),
                DatabaseServerType.Oracle => optionsBuilder.UseOracle(DatabaseServer.ConnectionString),
                DatabaseServerType.MySQL => optionsBuilder.UseMySQL(DatabaseServer.ConnectionString),
                DatabaseServerType.SQLite => optionsBuilder.UseSqlite(DatabaseServer.ConnectionString),
                DatabaseServerType.PostgreSQL => optionsBuilder.UseNpgsql(DatabaseServer.ConnectionString),
                _ => null
            };
        }

        protected override DatabaseServer GetDatabaseServer()
        {
            return new DatabaseServer()
            {
                ServerType = DatabaseServerType.MySQL,
                ConnectionString = ConfigurationManager.GetConnectionString("DefaultConnection")
            };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //add default data
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = DateTime.Now.Ticks,
                UserName = "admin",
                RealName = "EZNEW.NET",
                Password = "21232f297a57a5a743894a0e4a801fc3",//admin
                UserType = UserType.Management,
                Status = UserStatus.Enable,
                CreationTime = DateTimeOffset.Now,
                UpdateTime=DateTimeOffset.Now,
                Email = "admin@eznew.net",
                Mobile = "13600000000",
                QQ = "123456",
                SuperUser = true
            });
        }
    }
}
