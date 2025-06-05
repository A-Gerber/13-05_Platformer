using UnityEngine;

public static class Vector2Extensions
{
    public static float SqrDistance(this Vector2 start, Vector2 end)
    {
        return (end - start).sqrMagnitude;
    }
}