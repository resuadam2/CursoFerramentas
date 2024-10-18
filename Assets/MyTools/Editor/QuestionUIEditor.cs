using UnityEditor;
using UnityEngine;

// Custom editor para QuestionUILoader
[CustomEditor(typeof(QuestionUILoader)), CanEditMultipleObjects]
public class QuestionUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        QuestionUILoader loader = serializedObject.targetObject as QuestionUILoader;
        if (GUILayout.Button("Load Data"))
        {
            loader.LoadAndInflate();
        }
        if (GUILayout.Button("Clear UI"))
        {
            loader.ClearUI();
        }
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Test", GUILayout.Height(40)))
        {

            Debug.Log("TEST");
        }

    }
}