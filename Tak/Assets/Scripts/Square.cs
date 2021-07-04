using System.Collections.Generic;

public struct Square
{
    public Square(List<Piece> pieces) : this()
    {
        Pieces = pieces;
    }

    public List<Piece> Pieces;
}
