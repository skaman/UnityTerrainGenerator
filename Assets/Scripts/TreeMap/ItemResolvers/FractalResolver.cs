using UnityEngine;

public class FractalResolver : ItemResolver
{
    private readonly FastNoiseLite _noise = new();

    public override void Prepare()
    {
        var fractalType = GetParameterString("fractalType", "none");
        var noiseType = GetParameterString("noiseType", "none");
        var octaves = GetParameterInt("octaves");
        var frequency = GetParameterFloat("frequency");

        _noise.SetNoiseType(GetNoiseType(noiseType));
        _noise.SetFractalType(GetFractalType(fractalType));
        _noise.SetFractalOctaves(octaves);
        _noise.SetFrequency(frequency);
    }

    private FastNoiseLite.FractalType GetFractalType(string value)
    {
        switch (value.ToLower())
        {
            case "fbm":
                return FastNoiseLite.FractalType.FBm;
            case "ridged":
                return FastNoiseLite.FractalType.Ridged;
            case "pingpong":
                return FastNoiseLite.FractalType.PingPong;
            case "domainwarpprogressive":
                return FastNoiseLite.FractalType.DomainWarpProgressive;
            case "domainwarpindependent":
                return FastNoiseLite.FractalType.DomainWarpIndependent;
        }

        return FastNoiseLite.FractalType.None;
    }

    private FastNoiseLite.NoiseType GetNoiseType(string value)
    {
        switch (value.ToLower())
        {
            case "opensimplex2":
                return FastNoiseLite.NoiseType.OpenSimplex2;
            case "opensimplex2s":
                return FastNoiseLite.NoiseType.OpenSimplex2S;
            case "cellular":
                return FastNoiseLite.NoiseType.Cellular;
            case "perlin":
                return FastNoiseLite.NoiseType.Perlin;
            case "valuecubic":
                return FastNoiseLite.NoiseType.ValueCubic;
            case "value":
                return FastNoiseLite.NoiseType.Value;
        }

        return FastNoiseLite.NoiseType.OpenSimplex2;
    }

    public override float Get(float x, float y, float z)
    {
        return Mathf.Clamp((_noise.GetNoise(x, y, z) + 1.0f) / 2.0f, 0.0f, 1.0f);
    }
}
