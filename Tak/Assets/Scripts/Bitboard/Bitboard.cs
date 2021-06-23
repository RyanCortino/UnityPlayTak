using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Bitboard
{
    public struct Constants
    {
        public uint Size;
        public UInt64 Left, Right, Top, Bottom;
        public UInt64 Edge;
        public UInt64 Mask;
    }

    public static Constants Precompute(uint _size)
    {
        Constants constants = new Constants();

        for (uint i = 0; i < _size; i++)
        {
            constants.Right |= 0b1UL << (int)(i * _size);
        }
        constants.Size = _size;
        constants.Left = constants.Right << (int)(_size - 1);
        constants.Top = ((0b1UL << (int)_size) - 1) << (int)(_size * (_size - 1));
        constants.Bottom = (0b1UL << (int)_size) - 1;
        constants.Mask = (0b1UL << ((int)_size * (int)_size)) - 1;
        constants.Edge = (constants.Left | constants.Right | constants.Bottom | constants.Top);

        return constants;
    }
}