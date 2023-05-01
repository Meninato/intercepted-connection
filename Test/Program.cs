using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using InterceptedConnection;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Debug()
            .CreateLogger();

            using (DatabaseContext context = new DatabaseContext())
            {
                IDbConnection dbConnection = context.Database.GetDbConnection();
                //IDbConnection interceptedDbCon = new InterceptedDbConnection(dbConnection, Log.Logger);

                IDbConnection interceptedDbCon = new InterceptedDbConnection(dbConnection);
                LogDbCommand.Logger = Log.Logger;

                string sql = "SELECT Id, Name FROM Users WHERE Name LIKE @Name";
                string param = "Lul";
                var results = interceptedDbCon.Query<User>(sql, new { Name = $"%{param}%"});
            }

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
