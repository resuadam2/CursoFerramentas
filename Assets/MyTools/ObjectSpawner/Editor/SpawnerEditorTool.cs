using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
[EditorTool("Object Spawner")]
public class ObjectSpawnerEditorTool : EditorTool
{
    [NonSerialized] private GUIContent _iconContent;
    [SerializeField] private Texture2D toolIcon;
    [NonSerialized] private Rect toolWindowRect = new Rect(10, 0, 260f, 0f);
    private Spawner selectedSpawner;
    private float spawnRadius = 2;
    [NonSerialized] private bool mouseDown;
    [NonSerialized] private Vector2 mouseDownPosition;

    private void OnEnable()
    {
        _iconContent = new GUIContent()
        {
            image = toolIcon,
            text = "Spawner",
            tooltip = "Muestra la interfaz de spawner"
        };
    }
    public override GUIContent toolbarIcon => _iconContent;
    public void DrawToolWindow(int id)
    {
        selectedSpawner = EditorGUILayout.ObjectField(selectedSpawner, typeof(Spawner), false) as Spawner;
        spawnRadius = EditorGUILayout.FloatField("Radius", spawnRadius);
    }
    public override void OnToolGUI(EditorWindow window)
    {
        toolWindowRect.y = window.position.height - toolWindowRect.height - 100; // 100 is the height of the scene view toolbar
        toolWindowRect = GUILayout.Window(45, toolWindowRect, DrawToolWindow, "Object Spawner");
        // Event logic
        var ray = HandleUtility.GUIPointToWorldRay(mouseDown ? mouseDownPosition : Event.current.mousePosition);
        bool hitGround = Physics.Raycast(ray, out var result, 100);
        if (hitGround)
        {
            Handles.DrawWireDisc(result.point, Vector3.up, spawnRadius);
        }
        var controlId = EditorGUIUtility.GetControlID(FocusType.Passive);
        switch (Event.current.type)
        {
            case EventType.MouseDown:
                if (Event.current.button == 0 && Event.current.modifiers == EventModifiers.None)
                {
                    GUIUtility.hotControl = controlId;
                    mouseDown = true;
                    mouseDownPosition = Event.current.mousePosition;
                    Event.current.Use();
                }
                break;
            case EventType.MouseDrag:
                if (mouseDown)
                {
                    spawnRadius += EditorGUIUtility.PixelsToPoints(Event.current.delta).x / 100;
                    window.Repaint();
                }
                break;
            case EventType.MouseMove:
                window.Repaint();
                break;
            case EventType.MouseLeaveWindow:
            case EventType.MouseUp:
                if (mouseDown && hitGround && selectedSpawner)
                {
                    selectedSpawner.SpawnObjects(result.point, spawnRadius);
                }
                mouseDown = false;
                break;
        }
    }
}
