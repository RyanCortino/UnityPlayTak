using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Bitboard
{
    public struct Constants
    {
        public int Size;
        public ulong Left, Right, Top, Bottom;
        public ulong Edge;
        public ulong Mask;
    }

    public static Constants Precompute(int size)
    {
        Constants _constants = new Constants();

        for (uint i = 0; i < size; i++)
        {
            _constants.Right |= 0b1UL << (int)(i * size);
        }

        _constants.Size = size;

        _constants.Left = _constants.Right << (size - 1);

        _constants.Top = ((0b1UL << size) - 1) << (size * (size - 1));

        _constants.Bottom = (0b1UL << size) - 1;

        _constants.Edge = (_constants.Left | _constants.Right | _constants.Bottom | _constants.Top);

        _constants.Mask
            = (size < 8)   // Due to overflow restriction's we have to test for a single edge case.
            ? (0b1UL << (size * size)) - 1
            : ~0b0UL;

        return _constants;
    }

    public static ulong Flood(ref Constants constants, ulong within, ulong seed)
    {
        ulong _next = new ulong();

        while (true)
        {
            _next = Grow(ref constants, within, seed);

            if (_next == seed)
                return _next;
            
            seed = _next;
        }
    }

    public static ulong Grow(ref Constants constants, ulong within, ulong seed)
    {
        ulong _next = seed;

        _next |= (seed << 1) & ~constants.Right;
        _next |= (seed >> 1) & ~constants.Left;
        _next |= (seed >> (int)constants.Size);
        _next |= (seed << (int)constants.Size);

        return _next & within;
    }

    public static Tuple<int, int> Dimensions(Constants constants, ulong bits)
    {
        int _width = 0;
        int _height = 0;
        ulong _bitMask = 0;

        if (bits == 0)
            return Tuple.Create(_width, _height);

        _bitMask = constants.Left;
        while ((bits & _bitMask) == 0)
        {
            _bitMask >>= 1;
        }
        while ((_bitMask != 0) && ((bits & _bitMask) != 0))
        {
            _bitMask >>= 1;
            _width++;
        }
        
        _bitMask = constants.Top;
        while ((bits & _bitMask) == 0)
        {
            _bitMask >>= (int)constants.Size;
        }
        while ((_bitMask != 0) && ((bits & _bitMask) != 0))
        { 
            _bitMask >>= (int)constants.Size;
            _height++;
        }
        
        return Tuple.Create(_width, _height);
    }
}