using JetBrains.Annotations;

namespace SampleNuGet.Utils;

/// <summary>
///     Class util for files
/// </summary>
[PublicAPI]
public static class FileUtil
{
    /// <summary>
    ///     Find file, searching subdirectories but can also search in upper directories
    /// </summary>
    /// <param name="searchPattern">Search pattern (all "*.*")</param>
    /// <param name="startingPath">Starting path</param>
    /// <param name="howManyFoldersUp">How many folders we can go up to search (default 10)</param>
    /// <returns>File path of found file, null if no file found</returns>
    public static string? FindFile(string searchPattern, string startingPath = "./", int howManyFoldersUp = 10)
    {
        if (string.IsNullOrEmpty(startingPath))
            return null;

        //input string must end with "/"
        if (!startingPath.EndsWith("/"))
            startingPath += "/";

        while (howManyFoldersUp >= 0)
        {
            var files = FindFilesAlsoInSubdirectories(startingPath, searchPattern);
            if (files is { Length: > 0 } && !string.IsNullOrEmpty(files[0]))
                return files[0];

            //go one folder up, but if we are still at the same folder, we are stuck, return null
            var oldPath = Path.Join(startingPath, ".");
            startingPath += "../";
            var newPath = Path.Join(startingPath, ".");
            if (newPath == oldPath)
                return null;

            howManyFoldersUp--;
        }

        return null;
    }

    /// <summary>
    ///     Finds files, also in subdirectories
    /// </summary>
    /// <param name="startingPath">Starting path</param>
    /// <param name="searchPattern">Search pattern (all "*.*")</param>
    /// <returns>An array of files, can be empty</returns>
    /// <example>func("./","*.*")</example>
    private static string[] FindFilesAlsoInSubdirectories(string startingPath, string searchPattern)
    {
        return Directory.GetFiles(startingPath, searchPattern, SearchOption.AllDirectories);
    }


    public static bool TryDelete(string? path)
    {
        if (string.IsNullOrEmpty(path)) return false;
        try
        {
            File.Delete(path);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void TryBulkDelete(IEnumerable<string?> paths)
    {
        foreach (var path in paths) TryDelete(path);
    }
}