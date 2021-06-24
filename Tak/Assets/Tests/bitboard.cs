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
        int size = 5;
        ulong bound = 0x108423c;
        ulong seed = 0x4;
        ulong expectedOut = 0x108421c;

        var c = Bitboard.Precompute(size);
        var actualOut = Bitboard.Flood(ref c, bound, seed);

        Assert.AreEqual(expectedOut, actualOut);
    }

    [Test]
    public void test_dimensions()
    {
        List<Tuple<int, ulong, int, int>> _testCases = new List<Tuple<int, ulong, int, int>>();
        _testCases.Add(Tuple.Create(5, (ulong)0x108421c, 3, 5));
        _testCases.Add(Tuple.Create(5, 0b_0UL, 0, 0));
        _testCases.Add(Tuple.Create(5, (ulong)0x843800, 3, 3));
        _testCases.Add(Tuple.Create(5, (ulong)0x08000, 1, 1));

        for (int i = 0; i < _testCases.Count; i++)
        {
            Bitboard.Constants c = Bitboard.Precompute(_testCases[i].Item1);
            Tuple<int, int> dimensions = Bitboard.Dimensions(c, _testCases[i].Item2);

            Assert.AreEqual(dimensions, Tuple.Create(_testCases[i].Item3, _testCases[i].Item4));
        }
    }
}