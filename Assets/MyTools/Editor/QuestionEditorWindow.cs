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
    
    float m_Value = 50;
    // onGUI is called every frame in the Editor
    private void OnGUI()
    {
        // This is the Label for the Slider
        GUI.Label(new Rect(0, 0, 100, 30), "Rectangle Width");
        //This is the Slider that changes the size of the Rectangle drawn
        m_Value = GUI.HorizontalSlider(new Rect(100, 0, 100, 30), m_Value, 1.0f,250.0f);
        //The rectangle is drawn in the Editor (when MyScript is attached) with the width depending on the value of the Slider
        EditorGUI.DrawRect(new Rect(50, 50, m_Value, 70), Color.green);
    }
}
