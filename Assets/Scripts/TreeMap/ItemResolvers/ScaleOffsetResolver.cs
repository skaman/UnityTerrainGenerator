public class ScaleOffsetResolver : ItemResolver
{
    private Parameter _source;
    private Parameter _scale;
    private Parameter _offset;

    public override void Prepare()
    {
        _source = ParseParameterFloat("source");
        _scale = ParseParameterFloat("scale");
        _offset = ParseParameterFloat("offset");
    }

    public override float Get(float x, float y, float z)
    {
        return (_source.Get(x, y, z) * _scale.Get(x, y, z)) + _offset.Get(x, y, z);
    }
}
