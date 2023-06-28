using JetBrains.Annotations;

namespace PoliNetwork.Telegram.Utils;

/// <summary>
///     Dummy class
/// </summary>
[PublicAPI]
public static class TestUtils
{
    /// <summary>
    ///     Sum
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>a+b</returns>
    public static int Sum(int a, int b)
    {
        return a + b;
    }
}