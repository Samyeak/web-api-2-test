using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class DatabaseManager
    {
        private DatabaseManager(){ }

        private static DbConnectionStringBuilder connectionBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = "localhost",
            Username = "postgres",
            Password = "badministrator",
            Database = "Products"
        };

        public static string ConnectionString => connectionBuilder.ConnectionString;

    }
    public class EfContext : DbContext
    {
        public EfContext() : this(DatabaseManager.ConnectionString)
        {
        }
        public EfContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfContext, Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }


        #region DbSets
        public DbSet<Product> Products { get; set; }
        #endregion
    }
    public abstract class BaseRepository: IDisposable
    {
        protected IDbConnection connection;
        protected EfContext Context;
        public BaseRepository()
        {
            connection = new NpgsqlConnection(DatabaseManager.ConnectionString);
            //Context = new EfContext(DatabaseManager.ConnectionString);
        }



        public IDbConnection GetConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        public IDbCommand GetCommand()
        {
            using (var connection = GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    return command;
                }
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (Context != null)
                {
                    Context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}