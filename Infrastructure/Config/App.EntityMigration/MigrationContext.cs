using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using EZNEW.Data;
using EZNEW.Develop.Command;
using EZNEW.EntityMigration;
using Microsoft.EntityFrameworkCore;
using EZNEW.Fault;
using System.Linq;

namespace App.EntityMigration
{
    public class MigrationContext : EntityMigrationContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DatabaseType = DatabaseServerType.SQLServer;
            base.OnModelCreating(modelBuilder);
        }
    }
}
