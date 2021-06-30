using System;
using System.Collections.Generic;

public struct Position
{
    public Config config { get; private set; }
    
    byte whiteStones;
    byte whiteCapstones;
    byte blackStones;
    byte blackCapstones;

    int turnCount;

    ulong White, Black, StandingStones, Capstones;
    List<byte> Height;
    List<ulong> Stacks;

    Analysis analysis;

    ulong hash;

    public Position Preallocate(int size)
    {
        var _position = new Position();

        _position.config = new Config { Size = size };

        return Allocate(ref _position);
    }

    private Position Allocate(ref Position position)
    {
        Position p = new Position();

        switch (position.config.Size)
        {
            case 3:
                p = new Position();
                p.Height = PreAllocateList.Repeated(new byte(), 3 * 3);
                p.Stacks = PreAllocateList.Repeated(new ulong(), 3 * 3);
                p.analysis.WhiteGroups = PreAllocateList.Repeated(new ulong(), 6);
                p.Height = position.Height;
                p.Stacks = position.Stacks;

                return p;
            case 4:
                p = new Position();
                p.Height = PreAllocateList.Repeated(new byte(), 4 * 4);
                p.Stacks = PreAllocateList.Repeated(new ulong(), 4 * 4);
                p.analysis.WhiteGroups = PreAllocateList.Repeated(new ulong(), 8);
                p.Height = position.Height;
                p.Stacks = position.Stacks;

                return p;
            case 5:
                p = new Position();
                p.Height = PreAllocateList.Repeated(new byte(), 5 * 5);
                p.Stacks = PreAllocateList.Repeated(new ulong(), 5 * 5);
                p.analysis.WhiteGroups = PreAllocateList.Repeated(new ulong(), 10);
                p.Height = position.Height;
                p.Stacks = position.Stacks;

                return p;
            case 6:
                p = new Position();
                p.Height = PreAllocateList.Repeated(new byte(), 6 * 6);
                p.Stacks = PreAllocateList.Repeated(new ulong(), 6 * 6);
                p.analysis.WhiteGroups = PreAllocateList.Repeated(new ulong(), 12);
                p.Height = position.Height;
                p.Stacks = position.Stacks;

                return p;
            case 7:
                p = new Position();
                p.Height = PreAllocateList.Repeated(new byte(), 7 * 7);
                p.Stacks = PreAllocateList.Repeated(new ulong(), 7 * 7);
                p.analysis.WhiteGroups = PreAllocateList.Repeated(new ulong(), 14);
                p.Height = position.Height;
                p.Stacks = position.Stacks;

                return p;
            case 8:
                p = new Position();
                p.Height = PreAllocateList.Repeated(new byte(), 8 * 8);
                p.Stacks = PreAllocateList.Repeated(new ulong(), 8 * 8);
                p.analysis.WhiteGroups = PreAllocateList.Repeated(new ulong(), 16);
                p.Height = position.Height;
                p.Stacks = position.Stacks;

                return p;
            default:
                throw new NotImplementedException();
        }
    }
}