using System.Numerics;
using UnityEngine;

public static class UtilsKit
{
    public static UnityEngine.Vector2 ToVector(Complex complex)
    {
        return new UnityEngine.Vector2((float)complex.Real, (float)complex.Imaginary);
    }
    public static Complex ToComplex(UnityEngine.Vector2 vector2)
    {
        return new Complex(vector2.x, vector2.y);
    }
}