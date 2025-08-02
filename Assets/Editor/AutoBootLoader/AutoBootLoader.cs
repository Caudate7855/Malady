using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor.AutoBootLoader
{
    [InitializeOnLoad]
    public static class AutoBootLoader
    {
        private const string BootScenePath = "Assets/Project/Scenes/Boot/BootScene.unity";

        static AutoBootLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }
        
        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (SceneManager.GetActiveScene().path.Equals(BootScenePath)) 
                    return;
                EditorApplication.isPlaying = false;
                EditorSceneManager.OpenScene(BootScenePath);
                EditorApplication.delayCall += () => { EditorApplication.isPlaying = true; };
            }
        }
    }
}