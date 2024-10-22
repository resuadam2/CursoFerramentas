using UnityEngine;
using UnityEditor;

public class QuestionEditorWindow : EditorWindow
{
    [MenuItem("Window/QuestionListConfigurator")] // Add a menu item named "QuestionListConfigurator" to the Window menu
    private static void ShowWindow()
    {
        var window = GetWindow<QuestionEditorWindow>();
        window.titleContent = new GUIContent("QuestionListConfigurator");
        window.Show();
    }

    private Vector2 scrollPosition;
    // OnGUI se llama cada vez que se redibuja la ventana
    private void OnGUI()
    {
        DrawToolbar();
        DrawQuestionListElements();
    }

    private void DrawToolbar()
    {
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            GUILayout.Label("Questions", EditorStyles.toolbarTextField);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("NewQuestion UI", EditorStyles.toolbarButton))
            {
                LoadAndInstantiatePrefabsFromFolder();
            }
        }
    }

    private void LoadAndInstantiatePrefabsFromFolder()
    {
        string prefabPath = "Assets/MyTools/Prefabs/Question.prefab"; // Esto está hardcoded, mejor hacerlo configurable
        Transform goParent = GameObject.FindWithTag("QuestionParent").transform; // Esto también está hardcoded, mejor hacerlo configurable

        if (AssetDatabase.AssetPathExists(prefabPath))
        {
            Debug.Log("El prefab existe en la ruta: " + prefabPath);
            GameObject o = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            PrefabUtility.InstantiatePrefab(o, goParent);
        }
        else
        {
            Debug.LogWarning("El prefab no existe en la ruta: " + prefabPath);
        }
    }

    private void DrawQuestionListElements()
    {
        
        {
            QuestionUILoader[] allQuestions = GameObject.FindObjectsOfType<QuestionUILoader>();
            using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
            {
                scrollPosition = scrollViewScope.scrollPosition;
                for (int i = 0; i < allQuestions.Length; i++)
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Load data"))
                    {
                        allQuestions[i].LoadAndInflate();
                    }
                    if (allQuestions[i].isDataLoaded)
                    {
                        GUILayout.Label(allQuestions[i].GetQuestionString());
                    }
                    if (GUILayout.Button("Clear UI"))
                    {
                        allQuestions[i].ClearUI();
                    }
                    if (GUILayout.Button("Delete Question"))
                    {
                        DestroyImmediate(allQuestions[i].gameObject);
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }
        
    }
}
