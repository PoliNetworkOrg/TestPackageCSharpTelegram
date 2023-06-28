#region

using System.Data;
using JetBrains.Annotations;
using MySql.Data.MySqlClient;
using SampleNuGet.Objects;
using SampleNuGet.Utils.Logger;

#endregion

namespace SampleNuGet.Utils.DatabaseUtils;

[PublicAPI]
public static class Database
{
    public static int Execute(string? query, DbConfigConnection? dbConfigConnection,
        Dictionary<string, object?>? args = null)
    {
        LoggerClass.WriteLine(query, LogSeverityLevel.DATABASE_QUERY); //todo metti gli args

        return ExecuteSlave(query, dbConfigConnection, args);
    }

    public static int ExecuteUnlogged(string? query, DbConfigConnection? dbConfigConnection,
        Dictionary<string, object?>? args = null)
    {
        return ExecuteSlave(query, dbConfigConnection, args);
    }

    private static int ExecuteSlave(string? query, DbConfigConnection? dbConfigConnection,
        Dictionary<string, object?>? args = null)
    {
        if (dbConfigConnection == null)
            return 0;
        var connectionWithLock = dbConfigConnection.GetMySqlConnection();
        var connection = connectionWithLock.Conn;
        int numberOfRowsAffected;
        lock (connectionWithLock.Lock)
        {
            var cmd = new MySqlCommand(query, connection);


            OpenConnection(connection);

            if (args != null)
                foreach (var (key, value) in args)
                    cmd.Parameters.AddWithValue(key, value);

            numberOfRowsAffected = cmd.ExecuteNonQuery();
        }

        dbConfigConnection.ReleaseConn(connectionWithLock);
        return numberOfRowsAffected;
    }

    public static DataTable? ExecuteSelect(string? query, DbConfigConnection? dbConfigConnection,
        Dictionary<string, object?>? args = null)
    {
        LoggerClass.WriteLine(query, LogSeverityLevel.DATABASE_QUERY); //todo metti gli args

        return ExecuteSelectSlave(query, dbConfigConnection, args);
    }


    public static DataTable? ExecuteSelectUnlogged(string? query, DbConfigConnection? dbConfigConnection,
        Dictionary<string, object?>? args = null)
    {
        return ExecuteSelectSlave(query, dbConfigConnection, args);
    }

    private static DataTable? ExecuteSelectSlave(string? query, DbConfigConnection? dbConfigConnection,
        Dictionary<string, object?>? args = null)
    {
        if (dbConfigConnection == null) return null;
        var connectionWithLock = dbConfigConnection.GetMySqlConnection();
        var connection = connectionWithLock.Conn;
        var ret = new DataSet();
        lock (connectionWithLock.Lock)
        {
            var cmd = new MySqlCommand(query, connection);

            if (args != null)
                foreach (var (key, value) in args)
                    cmd.Parameters.AddWithValue(key, value);

            OpenConnection(connection);

            var adapter = new MySqlDataAdapter
            {
                SelectCommand = cmd
            };


            adapter.Fill(ret);

            adapter.Dispose();
        }


        dbConfigConnection.ReleaseConn(connectionWithLock);
        return ret.Tables[0];
    }

    public static void OpenConnection(IDbConnection connection)
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();
    }

    public static object? GetFirstValueFromDataTable(DataTable? dt)
    {
        if (dt == null)
            return null;

        try
        {
            return dt.Rows[0].ItemArray[0];
        }
        catch
        {
            return null;
        }
    }

    public static long? GetIntFromColumn(DataRow dr, string columnName)
    {
        var o = dr[columnName];
        if (o is null or DBNull)
            return null;

        try
        {
            return Convert.ToInt64(o);
        }
        catch
        {
            return null;
        }
    }
}