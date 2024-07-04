using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Project.Scripts.RemoteBuilder
{
    public class RemoteBuilder : MonoBehaviour
    {
        [MenuItem("Build/Build windows")]
        public static void PerformWindowsBuild()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = new[] { "Assets/Scenes/MainScene.unity" };
            buildPlayerOptions.locationPathName = "build/Windows/Malady.app";
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.None;

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"Build succeeded: {summary.totalSize} bytes." );
            }
            else if(summary.result == BuildResult.Failed)
            {
                Debug.Log("Build failed.");
            }
        }
    }
}