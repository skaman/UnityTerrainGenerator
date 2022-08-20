using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public abstract class ItemResolver
{
    public struct Parameter
    {
        public bool isConstant;
        public float value;
        public ItemResolver resolver;

        public float Get(float x, float y, float z)
        {
            if (isConstant)
                return value;

            return resolver.Get(x, y, z);
        }
    }

    protected TreeMapResolver treeMapResolver;
    protected Dictionary<string, string> arguments;

    public void SetTreeMapResolver(TreeMapResolver treeMapResolver)
    {
        this.treeMapResolver = treeMapResolver;
    }

    public void SetArguments(TreeMapItemArg[] args)
    {
        arguments = args.ToDictionary(x => x.name, x => x.value);
    }

    public Parameter ParseParameterFloat(string name)
    {
        if (!arguments.ContainsKey(name))
            throw new Exception($"Missing parameter {name}");

        if (
            float.TryParse(
                arguments[name],
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var value
            )
        )
            return new Parameter { isConstant = true, value = value };

        var resolver = treeMapResolver.GetItemResolver(arguments[name]);
        return new Parameter { isConstant = false, resolver = resolver };
    }

    protected float GetParameterFloat(string name, float defaultValue = 0.0f)
    {
        if (
            arguments.ContainsKey(name)
            && float.TryParse(
                arguments[name],
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var value
            )
        )
            return value;

        return defaultValue;
    }

    protected int GetParameterInt(string name, int defaultValue = 0)
    {
        if (
            arguments.ContainsKey(name)
            && int.TryParse(
                arguments[name],
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var value
            )
        )
            return value;

        return defaultValue;
    }

    protected string GetParameterString(string name, string defaultValue = null)
    {
        if (arguments.ContainsKey(name))
            return arguments[name];

        return defaultValue;
    }

    public abstract void Prepare();

    public abstract float Get(float x, float y, float z);
}
