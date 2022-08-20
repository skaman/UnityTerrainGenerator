using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Terrain/TerrainGround")]
public class TerrainGround : ScriptableObject
{
    private readonly TreeMapResolver _mapResolver = new();

    private ItemResolver _itemResolver;

    public TreeMapItem[] rules;
    public string entryPoint;
    public Vector2Int previewSize = new Vector2Int(1024, 256);

    public void Prepare()
    {
        _mapResolver.SetItems(rules);
        _itemResolver = _mapResolver.GetItemResolver(entryPoint);
    }

    public float Get(float x, float y, float z)
    {
        return _itemResolver.Get(x, y, z);
    }

    public Texture2D GetTexture2D(int width, int height)
    {
        Prepare();

        var texture = new Texture2D(width, height);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var value = _itemResolver.Get(
                    x / (float)previewSize.x,
                    y / (float)previewSize.y,
                    0
                );
                texture.SetPixel(x, y, new Color(value, value, value));
            }
        }
        texture.Apply();
        return texture;
    }
}
