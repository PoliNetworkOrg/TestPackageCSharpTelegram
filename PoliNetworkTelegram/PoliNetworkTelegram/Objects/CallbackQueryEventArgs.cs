#region

using Telegram.Bot.Types;

#endregion

namespace SampleNuGet.Objects;

public class CallbackQueryEventArgs
{
    public readonly CallbackQuery? CallbackQuery;

    public CallbackQueryEventArgs(CallbackQuery? callbackQuery)
    {
        CallbackQuery = callbackQuery;
    }
}