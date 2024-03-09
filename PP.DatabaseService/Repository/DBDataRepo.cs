using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using PP.DatabaseService.Data;
using PP.DatabaseService.Models.Dtos;
using PP.DatabaseService.Repository.Contract;
using System.Data;

namespace PP.DatabaseService.Repository
{
    public class DBDataRepo : IDBDataRepo
    {
        private readonly AppDbContext _dbContext;
        private readonly IDbConnection _connection;


        private ResponseDto _response;

        public DBDataRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _connection = _dbContext.Database.GetDbConnection();
            _connection.Open();
            _response = new ResponseDto();
        }

        public ResponseDto CloneDatabase(string existingConnectionString, string newDatabaseName)
        {
            //CreateNewDatabase(newDatabaseName);
            string newConnection = UpdateDatabaseNameInConnectionString(existingConnectionString, newDatabaseName.ToLower());
            CreateTablesInNewDatabase(newDatabaseName.ToLower(),existingConnectionString,newConnection);

            
            _response.Data = new 
            {
                DataBaseName = newDatabaseName
            };

            return _response;
        }

        static string UpdateDatabaseNameInConnectionString(string connectionString, string newDatabaseName)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Database = newDatabaseName
            };

            return builder.ConnectionString;
        }
        private bool CloneDBTables(string ExistingDatabaseConnectionString, string NewDatabaseName)
        {
            var cloneOptions = new DbContextOptionsBuilder<AppDbContext>()
              .UseNpgsql(ExistingDatabaseConnectionString.Replace(_dbContext.Database.GetDbConnection().Database, NewDatabaseName))
              .Options;

            var tables = GetAllTableNames();

            using (var cloneContext = new AppDbContext(cloneOptions))
            {

                //var tables = _dbContext.Model.GetEntityTypes().Select(t => t.GetTableName()).ToList();

                foreach (var table in tables)
                {
                    var createTableSql = $"CREATE TABLE {NewDatabaseName}.{table} AS TABLE public.{table} WITH NO DATA";
                    cloneContext.Database.ExecuteSqlRaw(createTableSql);

                    // Copy data from the source table to the new table
                    var copyDataSql = $"INSERT INTO {NewDatabaseName}.{table} SELECT * FROM public.{table}";
                    cloneContext.Database.ExecuteSqlRaw(copyDataSql);
                }
            }
            return true;
        }

        private DbContextOptions CreateConnectionString(string existingConnectionString, string newDatabaseName)
        {
          return  new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(existingConnectionString.Replace(_dbContext.Database.GetDbConnection().Database, newDatabaseName))
                .Options;
        }

        private void CreateNewDatabase(string newDatabaseName)
        {
         
            try
            {

                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = $"CREATE DATABASE {newDatabaseName}";
                    command.ExecuteNonQuery();
                }
            }
            finally
            {

            }
        }

        private void CreateTablesInNewDatabase(string newDatabaseName, string sourceConnectionString, string targetConnectionString)
        {
            var tables = GetAllTableNames();
            var existingDb = _dbContext.Database.GetDbConnection().Database.ToLower();
            try
            {

                using (var sourceConnection = new NpgsqlConnection(sourceConnectionString))
                using (var targetConnection = new NpgsqlConnection(targetConnectionString))
                {
                    sourceConnection.Open();
                    targetConnection.Open();

                    foreach (var tableName in tables)
                    {
                        CreateTable(targetConnection, tableName, existingDb);

                        // Copy data from source table to target table
                        CopyData(sourceConnection, targetConnection, tableName);
                    }

                    Console.WriteLine("Table cloned successfully!");
                }
            }

            //connection.Open();

            finally
            {
                //connection.Close();
            }  
            
        }

        static void CreateTable(NpgsqlConnection connection, string tableName,string existingDb)
        {
            using (var command = new NpgsqlCommand($"CREATE TABLE {tableName} AS TABLE {existingDb}.public.{tableName} WITH NO DATA", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        static void CopyData(NpgsqlConnection sourceConnection, NpgsqlConnection targetConnection, string tableName)
        {
            using (var copyCommand = new NpgsqlCommand($"INSERT INTO {tableName} SELECT * FROM {tableName}", targetConnection))
            {
                copyCommand.ExecuteNonQuery();
            }
        }


        private List<string> GetAllTableNames()
        {
            var tables = new List<string>();

          
                try
                {
                   // connection.Open();

                    using (var command = _connection.CreateCommand())
                    {
                        command.CommandText = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' AND table_type = 'BASE TABLE'";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tables.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                finally
                {
                   // connection.Close();
                }
            

            return tables;
        }
    }
}
