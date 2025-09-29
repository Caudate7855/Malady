using Project.Scripts;
using UnityEditor;
using UnityEngine;

namespace Editor.Markers
{
    [InitializeOnLoad]
    public class CustomHierarchyIcon
    {
        static CustomHierarchyIcon()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj == null) return;

            var marker = obj.GetComponent<PositionMarker>();
            if (marker == null) return;

            // Прямоугольник на месте стандартного кубика
            var iconRect = new Rect(selectionRect.x - 16, selectionRect.y, 16, 16);

            // Эмодзи как строка
            string emoji = marker.Emoji; // <-- просто поле string в компоненте

            var style = new GUIStyle()
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter
            };

            GUI.Label(iconRect, emoji, style);
        }
    }
}