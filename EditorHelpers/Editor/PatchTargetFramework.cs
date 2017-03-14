// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using System.IO;
using System.Text.RegularExpressions;

namespace LeopotamGroup.EditorHelpers.UnityEditors {
    static class PatchTargetFramework {
        const string NewTarget = "4.6";

        [PostSolutionGeneration]
        static void Process () {
#if DEV_FW46
            foreach (var file in Directory.GetFiles (Directory.GetCurrentDirectory (), "*.csproj")) {
                try {
                    var content = File.ReadAllText (file);
                    var newContent = Regex.Replace (content, "(\\<TargetFrameworkVersion\\>v)(.+)(\\</TargetFrameworkVersion\\>)", match => {
                        if (string.CompareOrdinal (match.Groups[2].Value, NewTarget) < 0) {
                            return match.Groups[1].Value + NewTarget + match.Groups[3].Value;
                        }
                        return match.Value;
                    });
                    if (string.CompareOrdinal (content, newContent) != 0) {
                        File.WriteAllText (file, newContent);
                    }
                } catch { }
            }
#endif
        }
    }
}