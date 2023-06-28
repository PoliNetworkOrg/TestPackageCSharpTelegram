#region

using JetBrains.Annotations;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

#endregion

namespace SampleNuGet.Objects;

[PublicAPI]
[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class MySqlConnectionWithLock
{
    public readonly MySqlConnection Conn;
    public readonly object Lock;

    public MySqlConnectionWithLock(string getConnectionString)
    {
        Conn = new MySqlConnection(getConnectionString);
        Lock = new object();
    }
}