using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SqlHelper.Code
{
    /// <summary>
    /// Provide methods for creating database and related tables in Sql Server.
    /// </summary>
    public class SqlDataManager
    {
        /// <summary>
        /// Get or set full path to data directory
        /// </summary>
        public static string DataDirectory { get; set; }

        /// <summary>
        /// Check if user provided connection string is valid or not.
        /// </summary>
        /// <param name="connectionString">End user provided connection string</param>
        /// <param name="errorMsg">Error message if any exception occur</param>
        /// <returns></returns>
        public static bool IsConStringValid(string connectionString, out string errorMsg)
        {
            var errorMessage = string.Empty;
            var isValid = true;

            try
            {
                // ReSharper disable once UnusedVariable
                var con = new SqlConnectionStringBuilder(connectionString);
            }
            catch (Exception exc)
            {
                errorMessage = exc.Message;
                isValid = false;
            }

            errorMsg = errorMessage;
            return isValid;
        }

        /// <summary>
        /// Create database on Sql Server as per connection string and script file path
        /// </summary>
        /// <param name="connectionString">End user provided connection string</param>
        /// <param name="dbScript">The database script content</param>
        /// <param name="bgDbWorker">BackgroundWorker to report progress</param>
        public static void CreateDatabase(string connectionString, string dbScript, BackgroundWorker bgDbWorker)
        {
            // 1 Get database create query
            bgDbWorker.ReportProgress(0, "Initializing query for creating database");
            var database = GetDatabaseName(connectionString);
            var query = GetDatabaseCreateQuery(database);

            // 2. Get new connection string
            var newConString = connectionString.Replace(database, "master");

            // 3. Create database
            var message = string.Format("Executing query for creating database '{0}'", database);
            bgDbWorker.ReportProgress(0, message);
            using (var sqlCon = new SqlConnection(newConString))
            using (var sqlCmd = new SqlCommand(query, sqlCon))
            {
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
            }

            // 4. Wait for seconds so that database be ready
            message = string.Format("Awaiting 5 seconds for database '{0}' to be ready...", database);
            bgDbWorker.ReportProgress(0, message);
            System.Threading.Thread.Sleep(5000);

            // Link - http://stackoverflow.com/questions/9297098/cant-immediately-connect-to-newly-created-sql-server-database?rq=1
            //query = string.Format("SELECT DATABASEPROPERTYEX('{0}', 'Collation')", database);
            //using (var sqlCon = new SqlConnection(newConString))
            //using (var sqlCmd = new SqlCommand(query, sqlCon))
            //{
            //    sqlCon.Open();
            //    object result;

            //    // Execute until result is null. If we get some value, then it means database is ready
            //    do
            //    {
            //        result = sqlCmd.ExecuteScalar(); 
            //    }
            //    while (result.Equals(DBNull.Value));
            //}

            // 5. Run script to create tables in same database
            message = string.Format("Executing query for creating tables in database '{0}'", database);
            bgDbWorker.ReportProgress(0, message);
            using (var sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                using (var trans = sqlCon.BeginTransaction())
                using (var cmd = new SqlCommand(dbScript, sqlCon, trans))
                {
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
            }

            bgDbWorker.ReportProgress(0, "Database and tables created successfully!");
        }

        /// <summary>
        /// Check if database already exists on Sql Server.
        /// </summary>
        /// <param name="connectionString">User entered connection string</param>
        /// <returns>Return true if database exists else false</returns>
        public static bool IsDatabaseExists(string connectionString)
        {
            bool dbExist;

            try
            {
                var database = GetDatabaseName(connectionString);
                var query = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", database);

                using (var sqlCon = new SqlConnection(connectionString))
                {
                    using (var sqlCmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCon.Open();
                        var databaseID = (int)sqlCmd.ExecuteScalar();
                        sqlCon.Close();

                        dbExist = (databaseID > 0);
                    }
                }
            }
            catch (Exception) //TODO: Add error handling to Elmah from common library
            {
                dbExist = false;
            }

            return dbExist;
        }

        /// <summary>
        /// Get query for creating database
        /// </summary>
        private static string GetDatabaseCreateQuery(string database)
        {
            // 1. Set different variable and paths
            var mdfDataName = string.Format("{0}_Data", database);
            var ldfLogName = string.Format("{0}_Log", database);
            var dbMdfPath = string.Format("{0}_data.mdf", Path.Combine(DataDirectory, database));
            var dbLdfPath = string.Format("{0}_log.ldf", Path.Combine(DataDirectory, database));

            // 2. Prepare query with attention 
            //var dbQuery = new StringBuilder("CREATE DATABASE ").Append(database);
            var dbQuery = new StringBuilder("CREATE DATABASE ").Append(database).Append(" ON PRIMARY ");
            dbQuery.AppendFormat("(NAME = {0}, ", mdfDataName);
            dbQuery.AppendFormat("FILENAME = '{0}') ", dbMdfPath);
            //dbQuery.Append("SIZE = 3MB, MAXSIZE = 10MB, FILEGROWTH = 10%) ");
            dbQuery.AppendFormat("LOG ON (NAME = {0}, ", ldfLogName);
            dbQuery.AppendFormat("FILENAME = '{0}')", dbLdfPath);
            //dbQuery.Append("SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%)");

            return dbQuery.ToString();
        }

        /// <summary>
        /// Get databae name as per connection string.
        /// </summary>
        /// <param name="connectionString">End user provided connection string</param>
        private static string GetDatabaseName(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder
            {
                ConnectionString = connectionString
            };
            return builder.InitialCatalog;
        }
    }
}
