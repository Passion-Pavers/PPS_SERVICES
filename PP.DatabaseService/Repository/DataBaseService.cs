using Npgsql;
using PP.DatabaseService.Repository.Contract;

namespace PP.DatabaseService.Repository
{
    public class DataBaseService : IDataBaseService
    {
        private readonly IConfiguration _config;
        public DataBaseService(IConfiguration config)
        {
            _config = config;
        }
        public async Task CreateNewDatabase(string databaseName)
        {
            try
            {
                var connectionString = _config.GetConnectionString("ApplicationConnectionString");
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand($"CREATE DATABASE {databaseName}", conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating database: {ex.Message}");
            }
        }

        public async Task CreateDBLink(string sourceConnectionString, string targetDatabaseName, string dblinkName)
        {
            try
            {
                var connectionString = UpdateDatabaseNameInConnectionString(sourceConnectionString, targetDatabaseName.ToLower());

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    // Create the dblink extension if not exists
                    using (var cmd = new NpgsqlCommand("CREATE EXTENSION IF NOT EXISTS dblink", conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }

                    using (var cmd = new NpgsqlCommand($"SELECT dblink_connect_u('{dblinkName}', @connstr)", conn))
                    {
                        cmd.Parameters.AddWithValue("connstr", sourceConnectionString);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating DBLink: {ex.Message}");
            }
        }

        string UpdateDatabaseNameInConnectionString(string connectionString, string newDatabaseName)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Database = newDatabaseName
            };

            return builder.ConnectionString;
        }
        public async Task CloneTables(string sourceConnectionString, string targetDatabaseName, string dblinkName)
        {
            try
            {
                var connectionString = $"Host=localhost;Username=myuser;Password=mypassword;Database={targetDatabaseName}";

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand($"SELECT dblink_exec('{dblinkName}', 'CREATE SCHEMA IF NOT EXISTS public')", conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // Get list of tables to clone
                    var tableList = await GetTableList(sourceConnectionString);

                    foreach (var table in tableList)
                    {
                        using (var cmd = new NpgsqlCommand($"SELECT dblink_exec('{dblinkName}', 'CREATE TABLE IF NOT EXISTS {table} AS TABLE \"{table}\"')", conn))
                        {
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error cloning tables: {ex.Message}");
            }
        }

        public async Task<string[]> GetTableList(string sourceConnectionString)
        {
            try
            {
                var tableList = new List<string>();
                using (var conn = new NpgsqlConnection(sourceConnectionString))
                {
                    await conn.OpenAsync();
                    using (var cmd = new NpgsqlCommand("SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'", conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                tableList.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                return tableList.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting table list: {ex.Message}");
            }
        }
    }
}
