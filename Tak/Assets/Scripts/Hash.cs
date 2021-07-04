using System;

public static class Hash
{
    public const ulong fnvBasis = 14695981039346656037;
    public const ulong fnvPrime = 1099511628211;

    public static ulong[] basis = new ulong[64];
    
    static Hash()
    {
        UnityEngine.Random.InitState(0x7a3);
        
        for (int i = 0; i < 64; i++)
        {
            basis[i] = Convert.ToUInt64(UnityEngine.Random.Range(0, ulong.MaxValue));
        }
    }

    public static ulong Hash8(ulong _basis, byte _b)
    {
        return (_basis ^ _b) * fnvPrime;
    }

    public static ulong Hash64(ulong _basis, ulong _w)
    {
        ulong h = _basis;

        h = (h ^ (_w & 0xff)) * fnvPrime;
        h = (h ^ ((_w >> 8) & 0xff)) * fnvPrime;
        h = (h ^ ((_w >> 16) & 0xff)) * fnvPrime;
        h = (h ^ (_w >> 24)) * fnvPrime;

        return h;
    }

    public static ulong HashAt(IPosition obj, uint index)
    {
        if (obj.Height[(int)index] <= 1)
            return 0;

        return Hash.Hash64(
                Hash.Hash8(basis[index], obj.Height[(int)index]),
                obj.Stacks[(int)index]
            );
    }
}
