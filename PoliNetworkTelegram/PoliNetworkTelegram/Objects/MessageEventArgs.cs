#region

using Newtonsoft.Json;
using Telegram.Bot.Types;

#endregion

namespace SampleNuGet.Objects;

/// <summary>
///     A Telegram Message Wrapper
/// </summary>
[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class MessageEventArgs
{
    public readonly Message Message;
    public bool Edit;

    public MessageEventArgs(Message message, bool edit = false)
    {
        Message = message;
        Edit = edit;
    }
}