using Dotnet.Data.Providers;
using Dotnet.EfTest.Tests.Entities;
using Dotnet.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Text;

namespace Dotnet.EfTest
{
    public  class EfDbContext: DbContext, IDbContext
    {

        static EfDbContext()
        {
            Database.SetInitializer<EfDbContext>(null);
        }

        public EfDbContext()
            : base("Name=test")
        {
            // 关闭语义可空判断
            Configuration.UseDatabaseNullSemantics = true;

            //与变量的值为null比较
            //ef判断为null的时候，不能用变量比较：https://stackoverflow.com/questions/682429/how-can-i-query-for-null-values-in-entity-framework?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
            (this as IObjectContextAdapter).ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior = true;
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public EfDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public System.Data.Entity.DbSet<Product> Applications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());

        }

        public DbProviderFactory GetDbFactory()
        {
            return null;
        }
        public  string ConnectionString { get; }

        public DBType DBType { get; set; }
    }
}
