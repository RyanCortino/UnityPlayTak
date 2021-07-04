public struct Piece
{
    public static readonly byte COLOR_MASK = 3 << 6;
    public static readonly byte TYPE_MASK = (1 << 2) - 1;

    public Piece(Color color, Kind kind) : this()
    {
        Value = (byte)((byte)color | (byte)kind);
    }

    public byte Value { get; private set; }

    public Color Color => (Color)(Value & COLOR_MASK);
    public Kind Kind => (Kind)(Value & TYPE_MASK);

    public bool IsRoad() => (Kind == Kind.Flat) || (Kind == Kind.Capstone);
    public string String() => ParseString();

    private string ParseString()
    {
        string _retVal = "";

        switch (Color)
        {
            case Color.White:
                _retVal = "W";
                break;

            case Color.Black:
                _retVal = "B";
                break;
        }

        switch (Kind)
        {
            case Kind.Standing:
                _retVal += "S";
                break;
            case Kind.Capstone:
                _retVal += "C";
                break;
        }

        return _retVal;
    }

    private Color Flip()
    {
        switch (Color)
        {
            case Color.White:
                return Color.Black;
            case Color.Black:
                return Color.White;
            default:
                return Color.NoColor;
        }
    }
}
