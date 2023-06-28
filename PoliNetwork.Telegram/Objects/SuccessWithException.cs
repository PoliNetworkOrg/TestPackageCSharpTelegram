#region

#endregion

using PoliNetworkBot_CSharp.Code.Objects;

namespace SampleNuGet.Objects;

public class SuccessWithException
{
    private readonly List<Exception?>? _ex;
    private readonly bool _success;

    public SuccessWithException(bool v)
    {
        _success = v;
    }

    public SuccessWithException(bool v, Exception? e2)
    {
        _success = v;
        _ex = new List<Exception?> { e2 };
    }

    public SuccessWithException(bool v, List<Exception?>? e2)
    {
        _success = v;
        _ex = e2;
    }

    public bool IsSuccess()
    {
        return _success;
    }

    internal List<Exception?>? GetExceptions()
    {
        return _ex;
    }

    public bool ContainsExceptions()
    {
        return _ex is { Count: > 0 };
    }

    public ExceptionNumbered? GetFirstException()
    {
        if (!ContainsExceptions()) return null;
        if (_ex == null) return null;
        var ex2 = _ex[0];
        return new ExceptionNumbered(ex2);
    }
}