using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

namespace BuildTools
{
    [Serializable]
    public class BuildParams
    {
        public bool IsDev;
        public string Path;

        public BuildParams(string path, bool isDev)
        {
            IsDev = isDev;
            Path = path;
        }
    }
    
    public class BuildRunner : MonoBehaviour
    {
        [MenuItem("Build Tools/Build")]
        public static void Build()
        {
            var buildParams = new BuildParams("../test.apk", true);
            ParseCommandLineArgs(buildParams);

            var buildOptions = BuildOptions.None;
            if (buildParams.IsDev)
            {
                buildOptions |= BuildOptions.Development;
            }
            var report = BuildPipeline.BuildPlayer(
                EditorBuildSettings.scenes, 
                buildParams.Path, 
                BuildTarget.Android,
                buildOptions);
            
            EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
        }

        private static void ParseCommandLineArgs(BuildParams buildParams)
        {
            var args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg == "-config")
                {
                    var path = args[i + 1];
                    var json = File.ReadAllText(path);
                    JsonUtility.FromJsonOverwrite(json, buildParams);
                }
            }
        }
    }
}