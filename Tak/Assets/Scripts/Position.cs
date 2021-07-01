using System;
using System.Collections.Generic;

public struct Position
{
    Config config;
    
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

    public static Position New(Config cfg)
    {
        Position _position = new Position();

        _position.config = cfg;
        _position.whiteStones = (byte)cfg.Stones;
        _position.whiteCapstones = (byte)cfg.CapStones;
        _position.blackStones = (byte)cfg.Stones;
        _position.blackCapstones = (byte)cfg.CapStones;
        _position.turnCount = 0;

        _position.White = 0ul;
        _position.Black = 0ul;
        _position.StandingStones = 0ul;
        _position.Capstones = 0ul;

        _position = Clone(_position);

        _position.hash = Hash.fnvBasis;

        return _position;
    }

    public static Position Clone(Position src)
    {
        Position _position = new Position();
        int _size = src.config.Size;

        _position = src;        
        _position.Height = new List<byte>(_size * _size);
        _position.Stacks = new List<ulong>(_size * _size);
        _position.analysis.WhiteGroups = new List<ulong>(_size * 2);

        return _position;
    }
}