using System;
using System.IO;

namespace UnityDataTools.Analyzer;

public class PathHelper
{
    public static string GetRelativePath(string relativeTo, string path)
    {
        if (string.IsNullOrEmpty(relativeTo))
        {
            throw new ArgumentNullException(nameof(relativeTo));
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        Uri relativeToUri = new Uri(AppendDirectorySeparatorChar(relativeTo));
        Uri pathUri = new Uri(AppendDirectorySeparatorChar(path));

        if (relativeToUri.Scheme != pathUri.Scheme) return path;


        Uri relativeUri = relativeToUri.MakeRelativeUri(pathUri);
        string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        if (string.Equals(pathUri.Scheme, Uri.UriSchemeFile, StringComparison.OrdinalIgnoreCase))
        {
            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        return relativePath;
    }

    private static string AppendDirectorySeparatorChar(string path)
    {
        if (path.EndsWith(Path.DirectorySeparatorChar.ToString())) return path;
        if (path.EndsWith(Path.AltDirectorySeparatorChar.ToString())) return path;
        return path + Path.DirectorySeparatorChar;
    }
}