public class TranslateDomainResolver : ItemResolver
{
    private Parameter _source;
    private Parameter _translateX;
    private Parameter _translateY;
    private Parameter _translateZ;

    public override void Prepare()
    {
        _source = ParseParameterFloat("source");
        _translateX = ParseParameterFloat("translateX");
        _translateY = ParseParameterFloat("translateY");
        _translateZ = ParseParameterFloat("translateZ");
    }

    public override float Get(float x, float y, float z)
    {
        return _source.Get(
            x + _translateX.Get(x, y, z),
            y + _translateY.Get(x, y, z),
            z + _translateZ.Get(x, y, z)
        );
    }
}
