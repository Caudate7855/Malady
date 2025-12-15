using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    public static class OpenSceneMenu
    {
        private const string Root = "Tools/OpenScene/";

        private const string BasePath = "Assets/Project/Scenes/";

        [MenuItem(Root + "Boot")]
        private static void OpenBoot()
        {
            Open("Boot/BootScene.unity");
        }

        [MenuItem(Root + "Church")]
        private static void OpenChurch()
        {
            Open("Church/ChurchScene.unity");
        }

        [MenuItem(Root + "DungeonGeneration")]
        private static void OpenDungeonGeneration()
        {
            Open("DungeonGeneration/DungeonGenerationScene.unity");
        }

        [MenuItem(Root + "Hub")]
        private static void OpenHub()
        {
            Open("Hub/HubScene.unity");
        }

        [MenuItem(Root + "MainMenu")]
        private static void OpenMainMenu()
        {
            Open("MainMenu/MainMenuScene.unity");
        }

        private static void Open(string relativePath)
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;

            EditorSceneManager.OpenScene(
                BasePath + relativePath,
                OpenSceneMode.Single
            );
        }
    }
}