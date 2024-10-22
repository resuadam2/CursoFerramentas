using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    [MenuItem("Window/LevelWindow")] // Add a menu item named "LevelWindow" to the Window menu
    private static void ShowWindow()
    {
        var window = GetWindow<LevelEditorWindow>();
        window.titleContent = new GUIContent("LevelWindow");
        window.Show();
    }
    private void OnGUI()
    {
        DrawToolbar();
        DrawSceneList();
    }

    private void DrawToolbar()
    {
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar)) // Create a new horizontal scope with a toolbar style
        {
            GUILayout.Label("Scenes In Build");
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Build Settings", EditorStyles.toolbarButton))
            {
                EditorApplication.ExecuteMenuItem("File/Build Profiles");
            }
        }
    }

    private Vector2 scrollPosition;
    private void DrawSceneList()
    {
        //Drawing the scroll view
        using (var scrollViewScope = new
       EditorGUILayout.ScrollViewScope(scrollPosition))
        {
            scrollPosition = scrollViewScope.scrollPosition;
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];
                DrawSceneListItem(i, scene);
            }
        }
    }

    private void DrawSceneListItem(int i, EditorBuildSettingsScene scene)
    {
        {
            var sceneName = Path.GetFileNameWithoutExtension(scene.path);
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label(i.ToString(), GUILayout.Width(20));
                GUILayout.Label(new GUIContent(sceneName, scene.path));
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Load"))
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        EditorSceneManager.OpenScene(scene.path);
                }
                if (GUILayout.Button("Load Additively"))
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        EditorSceneManager.OpenScene(scene.path,
                       OpenSceneMode.Additive);
                }
                if (GUILayout.Button("..."))
                {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Locate"), false, () =>
                    {
                        var sceneAsset =
                       AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
                        EditorGUIUtility.PingObject(sceneAsset);
                    });
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Item 2"), false, () => { });
                    menu.AddItem(new GUIContent("Item 3/More item here"), false, () => { });
                    menu.AddItem(new GUIContent("Item 3/More item here 2"), false, () => { });
                    menu.AddDisabledItem(new GUIContent("Item 4"));
                    menu.AddItem(new GUIContent("Item Selected"), true, () => { });
                    menu.ShowAsContext();
                }
            }
        }
    }
}
