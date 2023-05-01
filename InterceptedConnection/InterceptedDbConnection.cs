using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace InterceptedConnection
{
    /// <summary>
    /// This is just a wrapper around IDbConnection, 
    /// which allows us to build a wrapped IDbCommand for logging/debugging
    /// </summary>
    public class InterceptedDbConnection : IDbConnection
    {
        private readonly IDbConnection _conn;

        public InterceptedDbCommand LastCommand { get; private set; }

        public InterceptedDbConnection(IDbConnection connection)
        {
            _conn = connection;
        }

        public string ConnectionString { get => _conn.ConnectionString; set => _conn.ConnectionString = value; }

        public int ConnectionTimeout => _conn.ConnectionTimeout;

        public string Database => _conn.Database;

        public ConnectionState State => _conn.State;

        public IDbTransaction BeginTransaction() => _conn.BeginTransaction();

        public IDbTransaction BeginTransaction(IsolationLevel il) => _conn.BeginTransaction(il);

        public void ChangeDatabase(string databaseName) => _conn.ChangeDatabase(databaseName);

        public void Close() => _conn.Close();

        public IDbCommand CreateCommand()
        {
            IDbCommand underlyingCommand = _conn.CreateCommand();

            LastCommand = new InterceptedDbCommand(underlyingCommand);
            return LastCommand;
        }

        public void Dispose() => _conn.Dispose();

        public void Open() => _conn.Open();
    }
}
