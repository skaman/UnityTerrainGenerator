using UnityEngine;

public class GradientResolver : ItemResolver
{
    private float _x1;
    private float _x2;
    private float _y1;
    private float _y2;
    private float _z1;
    private float _z2;

    private float _x;
    private float _y;
    private float _z;

    private float _vlen;

    public override void Prepare()
    {
        _x1 = GetParameterFloat("x1");
        _x2 = GetParameterFloat("x2");
        _y1 = GetParameterFloat("y1");
        _y2 = GetParameterFloat("y2");
        _z1 = GetParameterFloat("z1");
        _z2 = GetParameterFloat("z2");

        _x = _x2 - _x1;
        _y = _y2 - _y1;
        _z = _z2 - _z1;

        _vlen = _x * _x + _y * _y + _z * _z;
    }

    public override float Get(float x, float y, float z)
    {
        float dx = x - _x1;
        float dy = y - _y1;
        float dz = z - _z1;
        float dp = dx * _x + dy * _y + dz * _z;
        dp /= _vlen;
        return dp;
    }
}
