using System.Collections.Generic;

public static class Allocate
{
    public static IPosition Position(int size)
    {
        IPosition p =
            new Position { Config = new Config { Size = size } };

        return Alloc(p);
    }

    public static void CopyPosition(ref IPosition dstPosition, IPosition srcPosition)
    {
        List<byte> _h = dstPosition.Height;
        List<ulong> _s = dstPosition.Stacks;
        Analysis _a = dstPosition.Analysis;

        dstPosition = srcPosition;

        dstPosition.Height = _h;
        dstPosition.Stacks = _s;
        dstPosition.Analysis = _a;

        dstPosition.Height = srcPosition.Height;
        dstPosition.Stacks = srcPosition.Stacks;
    }

    public static IPosition Alloc(IPosition position)
    {
        IPosition _position = position;
        int _size = _position.Config.Size;

        _position.Height = new List<byte>(_size * _size);
        _position.Stacks = new List<ulong>(_size * _size);
        _position.Analysis = new Analysis
        {
            WhiteGroups = new List<ulong>(_size * 2),
            BlackGroups = new List<ulong>(_size * 2)
        };
        _position.Height = position.Height;
        _position.Stacks = position.Stacks;

        return _position;
    }
}
