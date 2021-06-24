using NUnit.Framework;
using System;
using System.Collections.Generic;

public class bitboard
{
    [Test]
    public void test_precompute_5x5()
    {
        // Arrange
        Bitboard.Constants c = new Bitboard.Constants();
        Bitboard.Constants expectedConstants = new Bitboard.Constants();

        // Act
        c = Bitboard.Precompute(5);
        expectedConstants.Bottom = (0b_1UL << 5) - 1;
        expectedConstants.Top = ((0b_1UL << 5) - 1) << (4 * 5);
        expectedConstants.Right = 0x0108421;
        expectedConstants.Left = 0x1084210;
        expectedConstants.Mask = 0x1ffffff;

        // Assert
        Assert.AreEqual(expectedConstants.Bottom, c.Bottom);
        Assert.AreEqual(expectedConstants.Top, c.Top);
        Assert.AreEqual(expectedConstants.Right, c.Right);
        Assert.AreEqual(expectedConstants.Left, c.Left);
        Assert.AreEqual(expectedConstants.Mask, c.Mask);
    }

    [Test]
    public void test_precompute_8x8()
    {
        // Arrange
        Bitboard.Constants c = new Bitboard.Constants();
        Bitboard.Constants expectedConstants = new Bitboard.Constants();

        // Act
        c = Bitboard.Precompute(8);
        expectedConstants.Bottom = (0b_1ul << 8) - 1;
        expectedConstants.Top = ((0b_1ul << 8) - 1) << (7 * 8);
        expectedConstants.Right = 0x101010101010101;
        expectedConstants.Left = 0x8080808080808080;
        expectedConstants.Mask = ~0b0UL;

        // Assert
        Assert.AreEqual(expectedConstants.Bottom, c.Bottom);
        Assert.AreEqual(expectedConstants.Top, c.Top);
        Assert.AreEqual(expectedConstants.Right, c.Right);
        Assert.AreEqual(expectedConstants.Left, c.Left);
        Assert.AreEqual(expectedConstants.Mask, c.Mask);
    }

    [Test]
    public void test_flood()
    {
        // Arrange
        uint size = 5;
        ulong bound = 0x108423c;
        ulong seed = 0x4;
        ulong expectedOut = 0x108421c;

        // Act
        var c = Bitboard.Precompute(size);
        var actualOut = Bitboard.Flood(ref c, bound, seed);

        // Assert
        Assert.AreEqual(expectedOut, actualOut);
    }

    [Test]
    public void test_dimensions()
    {
        // Arrange
        List<Tuple<uint, ulong, uint, uint>> _testCases = new List<Tuple<uint, ulong, uint, uint>>();
        _testCases.Add(Tuple.Create<uint, ulong, uint, uint>(5, 0x108421c, 3, 5));
        _testCases.Add(Tuple.Create<uint, ulong, uint, uint>(5, 0, 0, 0));
        _testCases.Add(Tuple.Create<uint, ulong, uint, uint>(5, 0x843800, 3, 3));
        _testCases.Add(Tuple.Create<uint, ulong, uint, uint>(5, 0x08000, 1, 1));

        for (int i = 0; i < _testCases.Count; i++)
        {
            // Act
            Bitboard.Constants c = Bitboard.Precompute(_testCases[i].Item1);
            Tuple<uint, uint> actual = Bitboard.Dimensions(c, _testCases[i].Item2);
            Tuple<uint, uint> expected = Tuple.Create(_testCases[i].Item3, _testCases[i].Item4);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }

    [Test]
    public void test_trailing_zeros()
    {
        // Arrange
        List<Tuple<ulong, uint>> _testCases = new List<Tuple<ulong, uint>>();
        _testCases.Add(Tuple.Create<ulong, uint>(0x00, 64));
        _testCases.Add(Tuple.Create<ulong, uint>(0x01, 0));
        _testCases.Add(Tuple.Create<ulong, uint>(0x02, 1));
        _testCases.Add(Tuple.Create<ulong, uint>(0x010, 4));


        foreach (Tuple<ulong, uint> tc in _testCases)
        {
            // Act
            uint actual = Bitboard.TrailingZeros(tc.Item1);

            // Assert
            Assert.AreEqual(tc.Item2, actual);
        }
    }
}