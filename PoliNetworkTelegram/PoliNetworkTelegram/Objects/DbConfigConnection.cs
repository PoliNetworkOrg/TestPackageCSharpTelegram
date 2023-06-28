using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SampleNuGet.Objects;

[PublicAPI]
[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class DbConfigConnection
{
    private readonly DbConfig _dbConfig;
    private QueueThreadSafe? _mySqlConnection;

    public DbConfigConnection(DbConfig? dbConfig)
    {
        dbConfig ??= new DbConfig();
        _dbConfig = dbConfig;
    }

    public MySqlConnectionWithLock GetMySqlConnection()
    {
        if (_mySqlConnection != null)
            return _mySqlConnection.GetFirstAvailable();

        _mySqlConnection = new QueueThreadSafe(_dbConfig.GetConnectionString());
        return _mySqlConnection.GetFirstAvailable();
    }

    public DbConfig GetDbConfig()
    {
        return _dbConfig;
    }

    public void ReleaseConn(MySqlConnectionWithLock connectionWithLock)
    {
        _mySqlConnection?.ReleaseConn(connectionWithLock);
    }

    public string? GetDbName()
    {
        return _dbConfig.Database;
    }
}