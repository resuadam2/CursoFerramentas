using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestionLoader))]
public class QuestionLoaderEditor : Editor
{
    SerializedProperty url;
    int numberOfQuestions = 1;
    Color matColor = Color.white;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Load Questions"))
        {
            url = serializedObject.FindProperty("urlToObtainQuestions");
            Debug.Log("LOADING: " + url.stringValue);
            QuestionLoader loader = serializedObject.targetObject as QuestionLoader;
            loader.LoadQuestionDataInUI();
        }

        numberOfQuestions = EditorGUILayout.IntSlider("Number of questions", numberOfQuestions, 1, 50);
        matColor = EditorGUILayout.ColorField("Color", matColor);
    }
}
