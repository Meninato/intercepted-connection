﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterceptedConnection
{
    /// <summary>
    /// This is just a wrapper around IDbCommand, 
    /// which allows us to log queries
    /// or inspect how Dapper is passing our Parameters
    /// </summary>
    public class InterceptedDbCommand : IDbCommand
    {
        private readonly IDbCommand _cmd;

        public InterceptedDbCommand(IDbCommand command)
        {
            _cmd = command;
        }

        public string CommandText { get => _cmd.CommandText; set => _cmd.CommandText = value; }
        public int CommandTimeout { get => _cmd.CommandTimeout; set => _cmd.CommandTimeout = value; }
        public CommandType CommandType { get => _cmd.CommandType; set => _cmd.CommandType = value; }
        public IDbConnection Connection { get => _cmd.Connection; set => _cmd.Connection = value; }

        public IDataParameterCollection Parameters => _cmd.Parameters;

        public IDbTransaction Transaction { get => _cmd.Transaction; set => _cmd.Transaction = value; }
        public UpdateRowSource UpdatedRowSource { get => _cmd.UpdatedRowSource; set => _cmd.UpdatedRowSource = value; }

        public void Cancel() => _cmd.Cancel();

        public IDbDataParameter CreateParameter() => _cmd.CreateParameter();

        public void Dispose() => _cmd.Dispose();

        public void Prepare() => _cmd.Prepare();

        public int ExecuteNonQuery()
        {
            LogDbCommand.Dump(_cmd);
            return _cmd.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader()
        {
            LogDbCommand.Dump(_cmd);
            return _cmd.ExecuteReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            LogDbCommand.Dump(_cmd);
            return _cmd.ExecuteReader(behavior);
        }

        public object ExecuteScalar()
        {
            LogDbCommand.Dump(_cmd);
            return _cmd.ExecuteScalar();
        }

    }
}
