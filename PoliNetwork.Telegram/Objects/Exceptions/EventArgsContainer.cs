using Newtonsoft.Json;
using PoliNetwork.Telegram.Utils.CallbackUtils;
using SampleNuGet.Utils.CallbackUtils;

namespace SampleNuGet.Objects.Exceptions;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class EventArgsContainer
{
    public CallbackGenericData? CallbackGenericData;
    public CallbackQueryEventArgs? CallbackQueryEventArgs;
    public MessageEventArgs? MessageEventArgs;

    public static EventArgsContainer Get(MessageEventArgs? messageEventArgs)
    {
        return new EventArgsContainer { MessageEventArgs = messageEventArgs };
    }

    public static EventArgsContainer Get(CallbackGenericData callbackGenericData)
    {
        return new EventArgsContainer { CallbackGenericData = callbackGenericData };
    }


    public static EventArgsContainer Get(CallbackQueryEventArgs callbackQueryEventArgs)
    {
        return new EventArgsContainer { CallbackQueryEventArgs = callbackQueryEventArgs };
    }

    public static EventArgsContainer? Get(EventArgsContainer? paramEvent)
    {
        return paramEvent;
    }
}