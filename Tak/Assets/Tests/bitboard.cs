using NUnit.Framework;
using System;
using System.Collections.Generic;

public class bitboard
{
    [Test]
    public void test_precompute_5x5()
    {
        // Arrange
        Constants _actual = new Constants();
        Constants _expected = new Constants();

        // Act
        _actual = Bitboard.Precompute(5);

        _expected.Bottom = (1ul << 5) - 1;
        _expected.Top = ((1ul << 5) - 1) << (4 * 5);
        _expected.Right = 0x0108421;
        _expected.Left = 0x1084210;
        _expected.Mask = 0x1ffffff;

        // Assert
        Assert.AreEqual(_expected.Bottom, _actual.Bottom);
        Assert.AreEqual(_expected.Top, _actual.Top);
        Assert.AreEqual(_expected.Right, _actual.Right);
        Assert.AreEqual(_expected.Left, _actual.Left);
        Assert.AreEqual(_expected.Mask, _actual.Mask);
    }

    [Test]
    public void test_precompute_8x8()
    {
        // Arrange
        Constants _actual = new Constants();
        Constants _expected = new Constants();

        // Act
        _actual = Bitboard.Precompute(8);

        _expected.Bottom = (1ul << 8) - 1;
        _expected.Top = ((1ul << 8) - 1) << (7 * 8);
        _expected.Right = 0x101010101010101;
        _expected.Left = 0x8080808080808080;
        _expected.Mask = ~0ul;

        // Assert
        Assert.AreEqual(_expected.Bottom, _actual.Bottom);
        Assert.AreEqual(_expected.Top, _actual.Top);
        Assert.AreEqual(_expected.Right, _actual.Right);
        Assert.AreEqual(_expected.Left, _actual.Left);
        Assert.AreEqual(_expected.Mask, _actual.Mask);
    }

    [Test]
    public void test_flood()
    {
        // Arrange
        Constants _constants = new Constants();

        uint _size = 5u;
        ulong _bound = 0x108423c;
        ulong _seed = 0x4;
        ulong _expected = 0x108421c;        

        // Act
        _constants = Bitboard.Precompute(_size);
        ulong _actual = Bitboard.Flood(_constants, _bound, _seed);

        // Assert
        Assert.AreEqual(_expected, _actual);
    }

    [Test]
    public void test_dimensions()
    {
        // Arrange
        Constants _constants = new Constants();

        List<Tuple<uint, ulong, uint, uint>> _testCases = new List<Tuple<uint, ulong, uint, uint>>
        {
            Tuple.Create(5u, 0x108421cul, 3u, 5u),
            Tuple.Create(5u, 0ul, 0u, 0u),
            Tuple.Create(5u, 0x843800ul, 3u, 3u),
            Tuple.Create(5u, 0x08000ul, 1u, 1u)
        };

        Tuple<uint, uint> _actual = Tuple.Create(0u, 0u);
        Tuple<uint, uint> _expected = Tuple.Create(0u, 0u);

        for (int i = 0; i < _testCases.Count; i++)
        {
            // Act
            _constants = Bitboard.Precompute(_testCases[i].Item1);
            _actual = Bitboard.Dimensions(_constants, _testCases[i].Item2);
            _expected = Tuple.Create(_testCases[i].Item3, _testCases[i].Item4);

            // Assert
            Assert.AreEqual(_expected, _actual);
        }
    }

    [Test]
    public void test_bit_coordinates()
    {
        // Arrange
        Tuple<uint, uint> _expected = Tuple.Create<uint, uint>(0, 0);
        Tuple<uint, uint> _actual = Tuple.Create<uint, uint>(0, 0);
        Constants _c = new Constants();
        ulong _bit = 0ul;

        // _testCases: BoardSize, X, Y
        List<Tuple<uint, uint, uint>> _testCases = new List<Tuple<uint, uint, uint>>
        {
            Tuple.Create(5u, 1u, 1u),
            Tuple.Create(3u, 1u, 1u),
            Tuple.Create(3u, 2u, 2u),
            Tuple.Create(5u, 3u, 1u),
            Tuple.Create(5u, 0u, 1u)
        };

        // Act        
        foreach (Tuple<uint, uint, uint> tc in _testCases)
        {
            _c = Bitboard.Precompute(tc.Item1);
            _bit = 1UL << Convert.ToInt32(_c.Size * tc.Item3 + tc.Item2);

            _actual = Bitboard.BitCoordinates(_c, _bit);
            _expected = Tuple.Create(tc.Item2, tc.Item3);

            // Assert
            Assert.AreEqual(_expected.Item1, _actual.Item1);
            Assert.AreEqual(_expected.Item2, _actual.Item2);
        }
    }

    [Test]
    public void test_trailing_zeros()
    {
        // Arrange
        List <Tuple<ulong, uint>> _testCases = new List<Tuple<ulong, uint>>
        {
            Tuple.Create(0x00UL, 64U),
            Tuple.Create(0x01UL, 0U),
            Tuple.Create(0x02UL, 1U),
            Tuple.Create(0x010UL, 4U)
        };
        uint _actual = 0u;
        uint _expected = 0u;

        foreach (Tuple<ulong, uint> tc in _testCases)
        {
            // Act
            _actual = Bitboard.TrailingZeros(tc.Item1);
            _expected = tc.Item2;

            // Assert
            Assert.AreEqual(_expected, _actual);
        }
    }
}
