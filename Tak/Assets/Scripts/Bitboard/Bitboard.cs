using System;
using System.Collections.Generic;

public static class Bitboard
{
    public static Constants Precompute(uint size)
    {
        Constants _constants = new Constants();

        for (uint i = 0; i < size; i++)
        {
            _constants.Right |= 1ul << (int)(i * size);
        }

        _constants.Size = size;

        _constants.Left = _constants.Right << (int)(size - 1);

        _constants.Top = ((1ul << (int)size) - 1) << (int)(size * (size - 1));

        _constants.Bottom = (1ul << (int)size) - 1;

        _constants.Edge = (_constants.Left | _constants.Right | _constants.Bottom | _constants.Top);

        _constants.Mask
            = ((int)size < 8)   // Due to overflow restrictions we have to test for a single edge case.
            ? (1ul << ((int)size * (int)size)) - 1
            : ~0ul;

        return _constants;
    }

    public static ulong Flood(Constants constants, ulong within, ulong seed)
    {
        ulong _next = 0ul;

        while (true)
        {
            _next = Grow(constants, within, seed);

            if (_next == seed)
                return _next;

            seed = _next;
        }
    }

    public static ulong Grow(Constants constants, ulong within, ulong seed)
    {
        ulong _next = seed;

        _next |= (seed << 1) & ~constants.Right;
        _next |= (seed >> 1) & ~constants.Left;
        _next |= (seed >> (int)constants.Size);
        _next |= (seed << (int)constants.Size);

        return _next & within;
    }

    public static List<ulong> FloodGroups(Constants constants, ulong bits)
    {
        List<ulong> _outVal = new List<ulong>();
        ulong _seen = 0ul;
        ulong _g = 0ul;
        ulong _next = 0ul;
        ulong _bit = 0ul;

        while (bits != 0)
        {
            _next = bits & (bits - 1);
            _bit = (bits & ~_next);

            if ((_seen & _bit) == 0)
            {
                _g = Flood(constants, bits, _bit);

                if (_g != _bit)
                    _outVal.Add(_g);

                _seen |= _g;
            }

            bits = _next;
        }

        return _outVal;
    }

    public static Tuple<uint, uint> Dimensions(Constants constants, ulong bits)
    {
        uint _width = 0u;
        uint _height = 0u;
        ulong _bitMask = 0ul;

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

    public static Tuple<uint, uint> BitCoordinates(Constants constants, ulong bits)
    {
        if ((bits == 0) || ((bits & (bits - 1)) != 0))
        {

            return Tuple.Create(0u, 0u);
        }

        uint n = TrailingZeros(bits);
        uint _y = n / constants.Size;
        uint _x = n % constants.Size; // returns remainder as: n - (n / size) * size 

        return Tuple.Create(_x, _y);
    }

    public static int Popcount(ulong x)
    {
        if (x == 0)
            return 0;

        x -= (x >> 1) & 0x5555555555555555;
        x = (x >> 2) & 0x3333333333333333 + x & 0x3333333333333333;
        x += x >> 4;
        x &= 0x0f0f0f0f0f0f0f0f;
        x *= 0x0101010101010101;

        return Convert.ToInt32(x >> 56);
    }

    public static uint TrailingZeros(ulong x)
    {
        for (int i = 0; i < 64; i++)
        {
            if ((x & (1ul << i)) != 0)
                return (uint)i;
        }
        return 64;
    }
}