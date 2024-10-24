using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

[EditorTool("Vertex Visualizer Tool", typeof(MeshFilter))]
public class VertexVisualizerTool : EditorTool
{
    [SerializeField] private Texture2D toolIcon;
    private GUIContent _iconContent;
    private void OnEnable()
    {
        _iconContent = new GUIContent()
        {
            image = toolIcon,
            text = "Vertex Visualizer",
            tooltip = "Visualiza los vértices del mesh seleccionado"
        };
    }
    public override GUIContent toolbarIcon => _iconContent;

    public override void OnToolGUI(EditorWindow window)
    {
        if (window is SceneView sceneView)
        {
            MeshFilter meshFilter =
           Selection.activeGameObject?.GetComponent<MeshFilter>();
            if (meshFilter == null) return;
            Mesh mesh = meshFilter.sharedMesh;
            if (mesh == null) return;
            Vector3[] vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 worldPos = meshFilter.transform.TransformPoint(vertices[i]);
                Handles.color = Color.red;
                Handles.DrawSolidDisc(worldPos, Vector3.up, 0.01f);
                Handles.Label(worldPos, $"V{i}");
            }
        }
    }
}
