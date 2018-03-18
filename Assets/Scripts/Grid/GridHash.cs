using UnityEngine;

public struct GridHash
{

    public float a, b, c, d, e;

    public static GridHash Create ()
    {
        GridHash hash;
        hash.a = Random.value * 0.999f;
        hash.b = Random.value * 0.999f;
        hash.c = Random.value * 0.999f;
        hash.d = Random.value * 0.999f;
        hash.e = Random.value * 0.999f;
        return hash;
    }

}
