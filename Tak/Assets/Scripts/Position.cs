using System.Collections.Generic;

public struct Position : IPosition
{
    // Properties
    public Config Config { get; set; }
    public List<byte> Height { get; set; }
    public List<ulong> Stacks { get; set; }
    public Analysis Analysis { get; set; }
    public int MoveNumber { get; set; }
    public ulong White { get; set; }
    public ulong Black { get; set; }
    public ulong Standing { get; set; }
    public ulong Capstones { get; set; }
    public byte WhiteStones { get; set; }
    public byte BlackStones { get; set; }
    public byte WhiteCapstones { get; set; }
    public byte BlackCapstones { get; set; }
    public ulong Hash { get; set; }
    
    public int Size => Config.Size;
    
    public Position(Config config)
    {
        Config = config;
        MoveNumber = 0;

        WhiteStones = (byte)Config.Stones;
        WhiteCapstones = (byte)Config.CapStones;
        BlackStones = (byte)Config.Stones;
        BlackCapstones = (byte)Config.CapStones;

        White = 0ul;
        Black = 0ul;
        Standing = 0ul;
        Capstones = 0ul;

        Height = new List<byte>();
        Stacks = new List<ulong>();
        Analysis = new Analysis
        {
            WhiteGroups = new List<ulong>(),
            BlackGroups = new List<ulong>()
        };

        Hash = global::Hash.fnvBasis;

        this = (Position)Allocate.Alloc(this);
    }

    public void Analyze()
    {
        ulong _wr = (White & ~Standing);
        ulong _br = (Black & ~Standing);

        Analysis = new Analysis
        {
            WhiteGroups = Bitboard.FloodGroups(Config.Constants, _wr),
            BlackGroups = Bitboard.FloodGroups(Config.Constants, _br)
        };
    }
}