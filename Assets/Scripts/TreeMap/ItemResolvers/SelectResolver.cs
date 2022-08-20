public class SelectResolver : ItemResolver
{
    private float _low;
    private float _high;
    private float _threshold;
    private ItemResolver _control;

    public override void Prepare()
    {
        var control = GetParameterString("control");
        _low = GetParameterFloat("low");
        _high = GetParameterFloat("high");
        _threshold = GetParameterFloat("threshold");
        _control = treeMapResolver.GetItemResolver(control);
    }

    public override float Get(float x, float y, float z)
    {
        var value = _control.Get(x, y, z);
        return value < _threshold ? _low : _high;
    }
}
