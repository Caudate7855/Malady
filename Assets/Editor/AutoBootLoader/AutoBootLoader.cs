using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor.AutoBootLoader
{
    [InitializeOnLoad]
    public static class AutoBootLoader
    {
        private const string BootScenePath = "Assets/Project/Scenes/Boot/BootScene.unity";
        private const string PrefKey = "AutoBootLoader_Enabled";

        static AutoBootLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (!IsEnabled)
                return;

            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (SceneManager.GetActiveScene().path.Equals(BootScenePath))
                    return;

                EditorApplication.isPlaying = false;
                EditorSceneManager.OpenScene(BootScenePath);
                EditorApplication.delayCall += () => { EditorApplication.isPlaying = true; };
            }
        }

        private static bool IsEnabled
        {
            get { return EditorPrefs.GetBool(PrefKey, true); }
            set { EditorPrefs.SetBool(PrefKey, value); }
        }

        [MenuItem("Tools/Auto Boot Loader/Enabled")]
        private static void Toggle()
        {
            IsEnabled = !IsEnabled;
        }

        [MenuItem("Tools/Auto Boot Loader/Enabled", validate = true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked("Tools/Auto Boot Loader/Enabled", IsEnabled);
            return true;
        }
    }
}