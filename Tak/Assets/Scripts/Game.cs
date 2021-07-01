using System.Collections.Generic;

public class Game
{
    public static readonly List<int> defaultStones = new List<int>
    {
        0, 0, 0, 10, 15, 21, 30, 40, 50
    };

    public static readonly List<int> defaultCapstones = new List<int>
    {
        0, 0, 0, 0, 0, 1, 1, 2, 2
    };

    private Position New(Config game)
    {
        if (game.Stones == 0)
            game.Stones = defaultStones[game.Size];
        if (game.CapStones == 0)
            game.CapStones = defaultCapstones[game.Size];

        game.Constants = Bitboard.Precompute(System.Convert.ToUInt32(game.Size));

        Position _position = Position.New(game);

        return _position;
    }
}
