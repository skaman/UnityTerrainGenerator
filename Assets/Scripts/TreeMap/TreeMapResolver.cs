using System;

public class TreeMapResolver
{
    private TreeMapItem[] _items;
    private ItemResolver[] _itemResolvers;

    public void SetItems(TreeMapItem[] items)
    {
        _items = items;
        _itemResolvers = new ItemResolver[items.Length];
        for (var i = 0; i < items.Length; i++)
        {
            var itemResolver = GetItemResolver(_items[i].type);
            itemResolver.SetTreeMapResolver(this);
            itemResolver.SetArguments(_items[i].arguments);
            _itemResolvers[i] = itemResolver;
        }

        for (var i = 0; i < items.Length; i++)
            _itemResolvers[i].Prepare();
    }

    public ItemResolver GetItemResolver(string name)
    {
        var keyIndex = Array.FindIndex(_items, x => x.name == name);
        if (keyIndex == -1)
            throw new Exception($"Resolver {name} not found");

        return _itemResolvers[keyIndex];
    }

    //public float Resolve(string name, float x, float y, float z)
    //{
    //    _mainResolver = GetItemResolver(name);
    //    return itemResolver.Get(x, y, z);
    //}

    private static ItemResolver GetItemResolver(TreeMapItemType itemType)
    {
        switch (itemType)
        {
            case TreeMapItemType.Gradient:
                return new GradientResolver();
            case TreeMapItemType.Select:
                return new SelectResolver();
            case TreeMapItemType.Fractal:
                return new FractalResolver();
            case TreeMapItemType.ScaleOffset:
                return new ScaleOffsetResolver();
            case TreeMapItemType.TranslateDomain:
                return new TranslateDomainResolver();
            case TreeMapItemType.ScaleDomain:
                return new ScaleDomainResolver();
        }

        return null;
    }
}
