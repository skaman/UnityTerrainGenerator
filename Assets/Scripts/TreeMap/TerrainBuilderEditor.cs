#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainBuilder))]
public class TerrainBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var terrainBuilder = target as TerrainBuilder;
        if (GUILayout.Button("Build"))
        {
            terrainBuilder.Build();
        }
    }
}
#endif
