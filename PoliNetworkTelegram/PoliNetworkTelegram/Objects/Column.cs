using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SampleNuGet.Objects;

[PublicAPI]
[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class Column
{
    public readonly Type DataType;
    public readonly string Name;

    public Column(string name, Type dataType)
    {
        Name = name;
        DataType = dataType;
    }
}