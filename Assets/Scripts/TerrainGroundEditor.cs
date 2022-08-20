#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGround))]
public class TerrainGroundEditor : Editor
{
    private Texture2D _texture;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        DrawDefaultInspector();
        if (EditorGUI.EndChangeCheck() || _texture == null)
        {
            var terrainGround = target as TerrainGround;
            _texture = terrainGround.GetTexture2D(
                terrainGround.previewSize.x,
                terrainGround.previewSize.y
            );
        }

        var previewRatio = _texture.width / _texture.height;

        GUILayout.Label(
            _texture,
            GUILayout.ExpandWidth(true),
            GUILayout.Height(EditorGUIUtility.currentViewWidth / previewRatio)
        );
    }
}
#endif
