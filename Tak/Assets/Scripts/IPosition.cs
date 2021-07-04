using System;
using System.Collections.Generic;

public interface IPosition
{
    Config Config { get; set; }
    List<byte> Height { get; set; }
    List<ulong> Stacks { get; set; }
    Analysis Analysis { get; set; }
    ulong White { get; set; }
    ulong Black { get; set; }
    ulong Standing { get; set; }
    ulong Capstones { get; set; }

    int Size { get; }
    int MoveNumber { get; set; }
    byte WhiteStones { get; set; }
    byte BlackStones { get; set; }
    byte WhiteCapstones { get; set; }
    byte BlackCapstones { get; set; }

    ulong Hash { get; set; }

    void Analyze();
}