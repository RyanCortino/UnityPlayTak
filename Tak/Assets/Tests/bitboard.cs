using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
        expectedConstants.Bottom = (UInt64)(1 << 5) - 1;
        expectedConstants.Top = (UInt64)((1 << 5) - 1) << (4 * 5);
        expectedConstants.Right = (UInt64)0x0108421;
        expectedConstants.Left = (UInt64)0x1084210;
        expectedConstants.Mask = (UInt64)0x1ffffff;

        // Assert
        Assert.AreEqual(c.Bottom, expectedConstants.Bottom);
        Assert.AreEqual(c.Top, expectedConstants.Top);
        Assert.AreEqual(c.Right, expectedConstants.Right);
        Assert.AreEqual(c.Left, expectedConstants.Left);
        Assert.AreEqual(c.Mask, expectedConstants.Mask);
    }

    [Test]
    public void test_precompute_8x8()
    {
        // Arrange
        Bitboard.Constants c = new Bitboard.Constants();
        Bitboard.Constants expectedConstants = new Bitboard.Constants();

        // Act
        c = Bitboard.Precompute(8);
        expectedConstants.Bottom = (UInt64)(1 << 8) - 1;
        expectedConstants.Top = (UInt64)((1 << 8) - 1) << (7 * 8);
        expectedConstants.Right = (UInt64)0x101010101010101;
        expectedConstants.Left = (UInt64)0x8080808080808080;
        expectedConstants.Mask ^= (UInt64)0;

        // Assert
        Assert.AreEqual(c.Bottom, expectedConstants.Bottom);
        Assert.AreEqual(c.Top, expectedConstants.Top);
        Assert.AreEqual(c.Right, expectedConstants.Right);
        Assert.AreEqual(c.Left, expectedConstants.Left);
        Assert.AreEqual(c.Mask, expectedConstants.Mask);
    }
}