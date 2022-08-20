#if UNITY_EDITOR
using UnityEngine;

public class TerrainBuilder : MonoBehaviour
{
    public TerrainGround terrainGround;

    public void Build()
    {
        terrainGround.Prepare();

        var terrain = GetComponent<Terrain>();
        var heightmapWidth = terrain.terrainData.heightmapResolution;
        var heightmapHeight = terrain.terrainData.heightmapResolution;

        Debug.Log($"Heighmap width = {heightmapWidth}, height = {heightmapHeight}");

        var heightmap = terrain.terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
        for (var y = 0; y < heightmapHeight; y++)
        {
            var scaledY = y / (float)heightmapHeight;
            for (var x = 0; x < heightmapWidth; x++)
            {
                var scaledX = x / (float)heightmapWidth;
                for (var z = 257.0f; z >= 0; z--)
                {
                    var scaledZ = z / 257.0f;
                    var value = terrainGround.Get(scaledX, scaledZ, scaledY);
                    if (value == 0.0f)
                    {
                        heightmap[y, x] = scaledZ;
                        break;
                    }
                }
            }
        }

        terrain.terrainData.SetHeights(0, 0, heightmap);
    }
}
#endif
