using JetBrains.Annotations;

namespace SampleNuGet.Utils.Logger;

[PublicAPI]
internal class MessageQueue<T, TS>
{
    public readonly string Text;
    public KeyValuePair<T, TS?> Key;

    public MessageQueue(KeyValuePair<T, TS?> key, string text)
    {
        Key = key;
        Text = text;
    }
}