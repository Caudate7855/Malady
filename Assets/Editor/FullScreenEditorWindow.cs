using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class FullScreenEditorWindow
    {
        private const string FullScreenGameTitle = "FullScreen Game";
        private const string FullScreenSceneTitle = "FullScreen Scene";

        private const string FullScreenGameName = "FullScreen.Game";
        private const string FullScreenSceneName = "FullScreen.Scene";
        private const string CloseOverlayName = "FullScreen.Close";

        private static EditorWindow _contentWindow;
        private static FullScreenCloseOverlayWindow _overlayWindow;
        private static EditorApplication.CallbackFunction _pendingOpenAction;
        private static int _openVersion;
        private static FullScreenMode _mode;

        static FullScreenEditorWindow()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;

            EditorApplication.quitting -= OnEditorQuitting;
            EditorApplication.quitting += OnEditorQuitting;
        }

        [MenuItem("Tools/FullScreen/Game")]
        private static void OpenGame()
        {
            var gameViewType = GetGameViewType();

            if (gameViewType == null)
                throw new Exception("Не удалось найти UnityEditor.GameView");

            var referenceWindow = GetMainGameView();
            OpenWindow(gameViewType, referenceWindow, FullScreenMode.Game);
        }

        [MenuItem("Tools/FullScreen/Scene")]
        private static void OpenScene()
        {
            var referenceWindow = GetSceneReferenceWindow();
            OpenWindow(typeof(SceneView), referenceWindow, FullScreenMode.Scene);
        }

        [MenuItem("Tools/FullScreen/Close")]
        private static void CloseFullScreen()
        {
            CloseAllWindows();
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (_mode == FullScreenMode.Scene)
                    CloseAllWindows();

                return;
            }

            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                if (_mode == FullScreenMode.Game)
                    CloseAllWindows();
            }
        }

        private static void OnBeforeAssemblyReload()
        {
            CloseAllWindows();
        }

        private static void OnEditorQuitting()
        {
            CloseAllWindows();
        }

        private static void OpenWindow(Type windowType, EditorWindow referenceWindow, FullScreenMode mode)
        {
            if (windowType == null)
                throw new Exception("Не удалось найти тип окна.");

            CloseAllWindows();

            _openVersion++;
            _mode = mode;

            var rect = GetFullScreenRect();
            var contentRect = GetContentRect(rect);

            _contentWindow = CreateWindow(windowType, referenceWindow);

            if (!_contentWindow)
                throw new Exception($"Не удалось создать окно типа {windowType.FullName}");

            _contentWindow.name = GetWindowName(mode);
            _contentWindow.titleContent = new GUIContent(GetWindowTitle(mode));
            _contentWindow.minSize = contentRect.size;
            _contentWindow.maxSize = contentRect.size;
            _contentWindow.position = contentRect;

            ConfigureContentWindow(_contentWindow, mode);

            _contentWindow.ShowPopup();
            ApplyWindowState(_contentWindow, contentRect, mode);

            var version = _openVersion;

            _pendingOpenAction = () =>
            {
                _pendingOpenAction = null;

                if (version != _openVersion)
                    return;

                if (!_contentWindow)
                    return;

                ApplyWindowState(_contentWindow, contentRect, mode);
                OpenOverlay(rect, version);

                if (_contentWindow)
                {
                    _contentWindow.Focus();
                    _contentWindow.Repaint();
                }
            };

            EditorApplication.delayCall += _pendingOpenAction;
        }

        private static EditorWindow CreateWindow(Type windowType, EditorWindow referenceWindow)
        {
            if (referenceWindow && windowType.IsAssignableFrom(referenceWindow.GetType()))
            {
                var clonedWindow = UnityEngine.Object.Instantiate(referenceWindow) as EditorWindow;

                if (clonedWindow)
                    return clonedWindow;
            }

            var createdWindow = ScriptableObject.CreateInstance(windowType) as EditorWindow;

            if (!createdWindow)
                throw new Exception($"Не удалось создать окно типа {windowType.FullName}");

            return createdWindow;
        }

        private static void OpenOverlay(Rect fullScreenRect, int version)
        {
            SafeCloseOverlay();

            if (version != _openVersion)
                return;

            if (!_contentWindow)
                return;

            _overlayWindow = ScriptableObject.CreateInstance<FullScreenCloseOverlayWindow>();
            _overlayWindow.name = CloseOverlayName;
            _overlayWindow.titleContent = new GUIContent("Close");
            _overlayWindow.Initialize(CloseAllWindows);

            var width = 90f;
            var height = 36f;
            var margin = 12f;

            var rect = new Rect(
                fullScreenRect.xMax - width - margin,
                fullScreenRect.y + margin,
                width,
                height);

            _overlayWindow.minSize = rect.size;
            _overlayWindow.maxSize = rect.size;
            _overlayWindow.position = rect;
            _overlayWindow.ShowPopup();
            _overlayWindow.position = rect;
            _overlayWindow.Repaint();
        }

        private static void ApplyWindowState(EditorWindow window, Rect rect, FullScreenMode mode)
        {
            if (!window)
                return;

            window.minSize = rect.size;
            window.maxSize = rect.size;
            window.position = rect;
            window.maximized = true;

            ConfigureContentWindow(window, mode);

            window.Repaint();
        }

        private static void ConfigureContentWindow(EditorWindow window, FullScreenMode mode)
        {
            if (!window)
                return;

            if (mode == FullScreenMode.Game)
            {
                SetMemberValue(window, "m_showToolbar", false);
                SetMemberValue(window, "m_Gizmos", false);
                SetMemberValue(window, "m_Stats", false);
                SetMemberValue(window, "drawGizmos", false);
            }
        }

        private static void SetMemberValue(object target, string memberName, object value)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var type = target.GetType();

            var property = type.GetProperty(memberName, flags);

            if (property != null && property.CanWrite)
            {
                property.SetValue(target, value);
                return;
            }

            var field = type.GetField(memberName, flags);

            if (field != null)
                field.SetValue(target, value);
        }

        private static EditorWindow GetSceneReferenceWindow()
        {
            if (SceneView.lastActiveSceneView)
                return SceneView.lastActiveSceneView;

            var windows = Resources.FindObjectsOfTypeAll<SceneView>();

            foreach (var window in windows)
            {
                if (window)
                    return window;
            }

            return null;
        }

        private static EditorWindow GetMainGameView()
        {
            var type = GetGameViewType();

            if (type == null)
                return null;

            var method = type.GetMethod(
                "GetMainGameView",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (method == null)
                return null;

            return method.Invoke(null, null) as EditorWindow;
        }

        private static Type GetGameViewType()
        {
            return typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView");
        }

        private static Rect GetFullScreenRect()
        {
            var mainWindowRect = EditorGUIUtility.GetMainWindowPosition();
            var displayPosition = Screen.mainWindowPosition;
            var displayInfo = Screen.mainWindowDisplayInfo;

            var x = mainWindowRect.x - displayPosition.x;
            var y = mainWindowRect.y - displayPosition.y;

            return new Rect(x, y, displayInfo.width, displayInfo.height);
        }

        private static Rect GetContentRect(Rect fullScreenRect)
        {
            var rect = fullScreenRect;
            rect.yMin -= 17f;
            return rect;
        }

        private static string GetWindowName(FullScreenMode mode)
        {
            if (mode == FullScreenMode.Game)
                return FullScreenGameName;

            if (mode == FullScreenMode.Scene)
                return FullScreenSceneName;

            throw new Exception("Неизвестный режим fullscreen");
        }

        private static string GetWindowTitle(FullScreenMode mode)
        {
            if (mode == FullScreenMode.Game)
                return FullScreenGameTitle;

            if (mode == FullScreenMode.Scene)
                return FullScreenSceneTitle;

            throw new Exception("Неизвестный режим fullscreen");
        }

        private static void CloseAllWindows()
        {
            _openVersion++;
            _mode = FullScreenMode.None;

            CancelPendingOpen();

            SafeCloseOverlay();
            SafeCloseContent();
            CloseLegacyOrphans();
        }

        private static void CancelPendingOpen()
        {
            if (_pendingOpenAction == null)
                return;

            EditorApplication.delayCall -= _pendingOpenAction;
            _pendingOpenAction = null;
        }

        private static void SafeCloseOverlay()
        {
            if (_overlayWindow)
            {
                try
                {
                    _overlayWindow.Close();
                }
                catch
                {
                }
            }

            _overlayWindow = null;

            var windows = Resources.FindObjectsOfTypeAll<FullScreenCloseOverlayWindow>();

            foreach (var window in windows)
            {
                if (!window)
                    continue;

                if (window.name != CloseOverlayName)
                    continue;

                try
                {
                    window.Close();
                }
                catch
                {
                }
            }
        }

        private static void SafeCloseContent()
        {
            if (_contentWindow)
            {
                try
                {
                    _contentWindow.Close();
                }
                catch
                {
                }
            }

            _contentWindow = null;

            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();

            foreach (var window in windows)
            {
                if (!window)
                    continue;

                if (window.name != FullScreenGameName && window.name != FullScreenSceneName)
                    continue;

                try
                {
                    window.Close();
                }
                catch
                {
                }
            }
        }

        private static void CloseLegacyOrphans()
        {
            var windows = Resources.FindObjectsOfTypeAll<EditorWindow>();

            foreach (var window in windows)
            {
                if (!window)
                    continue;

                var title = window.titleContent != null ? window.titleContent.text : string.Empty;

                if (title != FullScreenGameTitle && title != FullScreenSceneTitle && title != "Close")
                    continue;

                try
                {
                    window.Close();
                }
                catch
                {
                }
            }
        }

        private enum FullScreenMode
        {
            None,
            Game,
            Scene
        }

        private class FullScreenCloseOverlayWindow : EditorWindow
        {
            private Action _closeAction;

            public void Initialize(Action closeAction)
            {
                _closeAction = closeAction;
            }

            private void OnGUI()
            {
                var rect = new Rect(0f, 0f, position.width, position.height);

                EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.85f));

                var oldColor = GUI.backgroundColor;
                GUI.backgroundColor = new Color(0.75f, 0.15f, 0.15f, 1f);

                if (GUI.Button(new Rect(4f, 4f, position.width - 8f, position.height - 8f), "Close"))
                    _closeAction?.Invoke();

                GUI.backgroundColor = oldColor;
            }

            private void Update()
            {
                if (!_contentWindow)
                {
                    try
                    {
                        Close();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}