using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public class MyTransform2D
{
    public float myx;
    public float myy;
}
[CustomPropertyDrawer(typeof(MyTransform2D))]
public class MyTransform : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent
   label)
    {
        // Usamos BeginProperty para manejar el control de arrastrar y otras interacciones
        EditorGUI.BeginProperty(position, label, property);
        // Calculamos las posiciones de los campos
        var xRect = new Rect(position.x, position.y, 100, position.height);
        var yRect = new Rect(position.x + 105, position.y, 50, position.height);
        SerializedProperty spX = property.FindPropertyRelative("myx");
        SerializedProperty spY = property.FindPropertyRelative("myy");
        // Dibujamos los campos de la clase serializable
        EditorGUI.PropertyField(xRect, spX, GUIContent.none);
        EditorGUI.PropertyField(yRect, spY, GUIContent.none);
        // Terminamos la propiedad
        EditorGUI.EndProperty();
    }
}