public class ScaleDomainResolver : ItemResolver
{
    private Parameter _source;
    private Parameter _scaleX;
    private Parameter _scaleY;
    private Parameter _scaleZ;

    public override void Prepare()
    {
        _source = ParseParameterFloat("source");
        _scaleX = ParseParameterFloat("scaleX");
        _scaleY = ParseParameterFloat("scaleY");
        _scaleZ = ParseParameterFloat("scaleZ");
    }

    public override float Get(float x, float y, float z)
    {
        return _source.Get(
            x * _scaleX.Get(x, y, z),
            y * _scaleY.Get(x, y, z),
            z * _scaleZ.Get(x, y, z)
        );
    }
}
