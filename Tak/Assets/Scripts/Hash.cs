using System;

public class Hash
{
    public const ulong fnvBasis = 14695981039346656037;
    public const ulong fnvPrime = 1099511628211;

    private ulong[] basis = new ulong[64];
    
    private void Initialize()
    {
        UnityEngine.Random.InitState(0x7a3);
        
        for (int i = 0; i < 64; i++)
        {
            basis[i] = Convert.ToUInt64(UnityEngine.Random.Range(0, ulong.MaxValue));      // test for negative values, panic if found
        }
    }    

    private ulong Hash8(ulong _basis, byte _b)
    {
        return (_basis ^ _b) * fnvPrime;
    }

    private ulong Hash64(ulong _basis, ulong _w)
    {
        ulong h = _basis;

        h = (h ^ (_w & 0xff)) * fnvPrime;
        h = (h ^ ((_w >> 8) & 0xff)) * fnvPrime;
        h = (h ^ ((_w >> 16) & 0xff)) * fnvPrime;
        h = (h ^ (_w >> 24)) * fnvPrime;

        return h;
    }
}
