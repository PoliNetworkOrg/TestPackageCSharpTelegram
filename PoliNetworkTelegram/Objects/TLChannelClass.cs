#region

using TeleSharp.TL;

#endregion

namespace SampleNuGet.Objects;

public class TlChannelClass
{
    public readonly TLChannel Channel;

    public TlChannelClass(TLChannel channel)
    {
        Channel = channel;
    }
}