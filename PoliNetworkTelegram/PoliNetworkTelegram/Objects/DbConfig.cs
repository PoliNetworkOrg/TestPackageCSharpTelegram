#region

using JetBrains.Annotations;
using Newtonsoft.Json;

#endregion

namespace SampleNuGet.Objects;

[PublicAPI]
[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class DbConfig
{
    public string? Database;
    public string? Host;
    public string? Password;
    public int Port;
    public string? User;


    public string GetConnectionString()
    {
        if (string.IsNullOrEmpty(Password))
            return "server='" + Host + "';user='" + User + "';database='" + Database + "';port=" + Port +
                   ";Allow User Variables=True";

        return "server='" + Host + "';user='" + User + "';database='" + Database + "';port=" + Port + ";password='" +
               Password + "'" + ";Allow User Variables=True";
    }


}