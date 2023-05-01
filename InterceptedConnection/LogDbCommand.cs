using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterceptedConnection
{
    public static class LogDbCommand 
    {
        public static ILogger Logger;

        public static void Dump(IDbCommand dbCommand)
        {
            if(Logger != null)
            {
                Logger.Debug("SQL COMMAND: {commandText}", dbCommand.CommandText);
                for (int i = 0; i < dbCommand.Parameters.Count; i++)
                {
                    IDataParameter dbParam = dbCommand.Parameters[i] as IDataParameter;
                    if (dbParam != null)
                    {
                        Logger.Debug("SQL PARAMETER Name: {name}, Value: {value} ", dbParam.ParameterName, dbParam.Value);
                    }
                }
            }
        }
    }
}
