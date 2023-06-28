#region

using JetBrains.Annotations;

#pragma warning disable CS0162

#endregion

namespace SampleNuGet.Utils.Logger;

[PublicAPI]
public static class LoggerClass
{
    public static void WriteLine(string? query, LogSeverityLevel oLogSeverityLevel)
    {
        Console.WriteLine(oLogSeverityLevel + " " + query);
    }
}