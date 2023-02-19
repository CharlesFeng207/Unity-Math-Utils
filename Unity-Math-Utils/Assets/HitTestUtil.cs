using UnityEngine;

public static class HitTestUtil
{
    public static float Ray(Vector2 targetPos, float targetRadius, Vector2 rayPos, Vector2 rayDir, float width)
    {
        var targetDir = targetPos - rayPos;
        if (targetDir == Vector2.zero)
            return 0.5f;
        
        var d2r = DisPoint2Ray(targetDir, rayDir);
        var min = width - targetRadius;
        var max = width + targetRadius;
        var angleFactor = 1 - InverseLerp(min, max, d2r);
        
        var dir = Vector3.Cross(rayDir.To3D(), Vector3.up).To2D();
        d2r = DisPoint2Ray(targetDir, dir) * Mathf.Sin(Dot(rayDir, targetDir));
        min = -targetRadius;
        max = Magnitude(rayDir) + targetRadius;
        var disFactor = 1 - InverseLerp(min, max, d2r); 

        // var disFactor = Dot(rayDir, targetPos - rayPos) > 0
        //     ? Circle(targetPos, targetRadius, rayPos, Magnitude(rayDir))
        //     : 0f;

        return disFactor * angleFactor;
    }

    public static float Circle(Vector2 targetPos, float targetRadius, Vector2 circlePos, float circleRadius)
    {
        var d = Distance(targetPos, circlePos);

        var min = circleRadius - targetRadius;
        var max = circleRadius + targetRadius;

        return 1 - InverseLerp(min, max, d);
    }

    public static float Sector(Vector2 targetPos, float targetRadius, Vector2 sectorPos, Vector2 dir, float angle)
    {
        var d = Distance(targetPos, sectorPos);

        var angleFactor = 0f;

        if (d < targetRadius)
        {
            angleFactor = 1f;
        }
        else
        {
            var offsetAngle = Mathf.Asin(targetRadius / d) * Mathf.Rad2Deg;
            var targetAngle = Angle(targetPos - sectorPos, dir);
            var min = angle * 0.5f - offsetAngle;
            var max = angle * 0.5f + offsetAngle;

            angleFactor = 1 - InverseLerp(min, max, targetAngle);
        }

        return angleFactor * Circle(targetPos, targetRadius, sectorPos, Magnitude(dir));
    }

    /// <summary>
    ///   <para>Calculates the linear parameter t that produces the interpolant value within the range [a, b].</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="value"></param>
    public static float InverseLerp(float a, float b, float value)
    {
        if ((double) a != (double) b)
            return Mathf.Clamp01((float) (((double) value - (double) a) / ((double) b - (double) a)));
        return 0.0f;
    }

    /// <summary>
    ///   <para>Linearly interpolates between a and b by t.</para>
    /// </summary>
    /// <param name="a">The start value.</param>
    /// <param name="b">The end value.</param>
    /// <param name="t">The interpolation value between the two floats.</param>
    /// <returns>
    ///   <para>The interpolated float result between the two float values.</para>
    /// </returns>
    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * Mathf.Clamp01(t);
    }

    /// <summary>
    ///   <para>Returns the distance between a and b.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public static float Distance(Vector2 a, Vector2 b)
    {
        return Magnitude(a - b);
    }

    /// <summary>
    ///   <para>Returns the length of this vector (Read Only).</para>
    /// </summary>
    public static float Magnitude(Vector2 vector2)
    {
        return Mathf.Sqrt((float) ((double) vector2.x * (double) vector2.x + (double) vector2.y * (double) vector2.y));
    }

    /// <summary>
    ///   <para>Returns the unsigned angle in degrees between from and to.</para>
    /// </summary>
    /// <param name="from">The vector from which the angular difference is measured.</param>
    /// <param name="to">The vector to which the angular difference is measured.</param>
    public static float Angle(Vector2 from, Vector2 to)
    {
        
        return Mathf.Acos(Mathf.Clamp(Dot(Normalize(from), Normalize(to)), -1f, 1f)) * Mathf.Rad2Deg;
    }

    /// <summary>
    ///   <para>Dot Product of two vectors.</para>
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    public static float Dot(Vector2 lhs, Vector2 rhs)
    {
        return (float) ((double) lhs.x * (double) rhs.x + (double) lhs.y * (double) rhs.y);
    }

    /// <summary>
    ///   <para>Makes this vector have a magnitude of 1.</para>
    /// </summary>
    public static Vector2 Normalize(Vector2 v)
    {
        var magnitude = Magnitude(v);
        return v / magnitude;
    }

    public static float DisPoint2Ray(Vector2 targetDir, Vector2 dir)
    {
        var a = Angle(targetDir, dir);
        var r = Mathf.Sin(a * Mathf.Deg2Rad) * Magnitude(targetDir);
        return r;
    }
}