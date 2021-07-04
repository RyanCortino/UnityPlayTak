using System;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    public static readonly List<int> DEFAULT_STONES = new List<int> { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
    public static readonly List<int> DEFAULT_CAPSTONES = new List<int> { 0, 0, 0, 0, 0, 1, 1, 2, 2 };

    public Color ToMove => Position.MoveNumber % 2 == 0 ? Color.White : Color.Black;
    
    protected IPosition Position;
    
    /// <summary>
    /// Initializes a new game from raw Config.
    /// </summary>
    /// <param name="cfg"></param>
    /// <returns></returns>
    private void NewGame(Config cfg)
    {
        if (cfg.Stones == 0)
            cfg.Stones = DEFAULT_STONES[cfg.Size];
        if (cfg.CapStones == 0)
            cfg.CapStones = DEFAULT_CAPSTONES[cfg.Size];

        cfg.Constants = Bitboard.Precompute((uint)cfg.Size);

        Position =  new Position(cfg);
    }

    public IPosition Clone => Allocate.Alloc(Position);

    /// <summary>
    /// Initializes a new game with the specified squares and currrent turn count.
    /// </summary>
    /// <param name="cfg"></param>
    /// <param name="board"></param>
    /// <param name="turnCount"></param>
    /// <returns></returns>
    private Position LoadGame(Config cfg, Square[][] board, int turnCount)
    {
        Square _square = new Square();

        IPosition _p = new Position(cfg);
        _p.MoveNumber = turnCount;

        int _size = _p.Size;

        for (int y = 0; y < _size; y++)
        {
            for (int x = 0; x < _size; x++)
            {
                _square = board[x][y];

                if (_square.Pieces.Count == 0)
                    continue;

                uint i = (uint)(x + y * _size);

                switch (_square.Pieces[0].Color)
                {
                    case Color.White:
                        _p.White |= (1ul << (int)i);
                        break;
                    case Color.Black:
                        _p.Black |= 1ul << (int)i;
                        break;
                }

                switch (_square.Pieces[0].Kind)
                {
                    case Kind.Standing:
                        _p.Standing |= (1ul << (int)i);
                        break;
                    case Kind.Capstone:
                        _p.Capstones |= (1ul << (int)i);
                        break;
                }

                for (int j = 0; j < _square.Pieces.Count; j++)
                {
                    foreach (Piece piece in _square.Pieces)
                    {
                        if (piece.Value == new Piece(Color.White, Kind.Capstone).Value)
                            _p.WhiteCapstones--;

                        if (piece.Value == new Piece(Color.Black, Kind.Capstone).Value)
                            _p.BlackCapstones--;

                        if (piece.Value == new Piece(Color.White, Kind.Flat).Value ||
                            piece.Value == new Piece(Color.White, Kind.Standing).Value)
                            _p.WhiteStones--;

                        if (piece.Value == new Piece(Color.Black, Kind.Flat).Value ||
                            piece.Value == new Piece(Color.Black, Kind.Standing).Value)
                            _p.BlackStones--;

                        if (j == 0)
                            continue;

                        if (piece.Color == Color.Black)
                            _p.Stacks[(int)i] |= 1ul << (j - 1);
                    }

                    _p.Height[(int)i] = (byte)_square.Pieces.Count;

                    _p.Hash = Hash.HashAt(_p, i);
                }
            }

        }

        _p.Analyze();

        return (Position)_p;
    }

    private Square? FindSquare((int, int) coordinates)
    {
        int _x = coordinates.Item1;
        int _y = coordinates.Item2;

        uint _bitPosition = (uint)(_x + _y * Position.Size);

        if (((Position.White | Position.Black) & (1ul << (int)_bitPosition)) == 0)
            return null;

        Square _square = new Square();
        _square.Pieces.Capacity = Position.Height[(int)_bitPosition];
        _square.Pieces.Add(FindTopmostPiece(coordinates));

        for (byte i = 1; i < Position.Height[(int)_bitPosition]; i++)
        {
            if ((Position.Stacks[(int)_bitPosition] & (1ul << (i - 1))) != 0)
                _square.Pieces.Add(new Piece(Color.Black, Kind.Flat));
            else
                _square.Pieces.Add(new Piece(Color.White, Kind.Flat));
        }

        return _square;
    }

    private Piece FindTopmostPiece((int, int) coordinates)
    {
        int _x = coordinates.Item1;
        int _y = coordinates.Item2;
        Color _c = new Color();
        Kind _k = new Kind();

        uint _bitPosition = (uint)(_x + _y * Position.Size);

        if ((Position.White & (1ul << (int)_bitPosition)) != 0)
            _c = Color.White;
        else if ((Position.Black & (1ul << (int)_bitPosition)) != 0)
            _c = Color.Black;
        else
            _c = Color.NoColor;

        if ((Position.Standing & (1ul << (int)_bitPosition)) != 0)
            _k = Kind.Standing;
        else if ((Position.Capstones & (1ul << (int)_bitPosition)) != 0)
            _k = Kind.Capstone;
        else
            _k = Kind.Flat;

        return new Piece(_c, _k);
    }

}