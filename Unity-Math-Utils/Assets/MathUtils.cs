using UnityEngine;

public static class MathUtils
{
    // 扇形与圆盘相交测试
    // a 扇形圆心
    // u 扇形方向（单位矢量）
    // theta 扇形扫掠半角 (注意是弧度)
    // l 扇形边长
    // c 圆盘圆心
    // r 圆盘半径
    public static bool IsSectorDiskIntersect(
        Vector2 a, Vector2 u, float theta, float l,
        Vector2 c, float r)
    {
        // 1. 如果扇形圆心和圆盘圆心的方向能分离，两形状不相交
        Vector2 d = c - a;
        float rsum = l + r;
        if (d.sqrMagnitude > rsum * rsum)
            return false;

        // 2. 计算出扇形局部空间的 p
        float px = Vector2.Dot(d, u);
        float py = Mathf.Abs(Vector2.Dot(d, new Vector2(-u.y, u.x)));

        // 3. 如果 p_x > ||p|| cos theta，两形状相交
        if (px > d.magnitude * Mathf.Cos(theta))
            return true;

        // 4. 求左边线段与圆盘是否相交
        Vector2 q = l * new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        Vector2 p = new Vector2(px, py);
        float sqrDis = SegmentPointSqrDistance(Vector2.zero, q, p); 
        return sqrDis <= r * r;
    }

    // 计算线段与点的最短平方距离
    // x0 线段起点
    // u  线段方向至末端点
    // x  任意点
    public static float SegmentPointSqrDistance(Vector2 x0, Vector2 u, Vector2 x)
    {
        float t = Vector2.Dot(x - x0, u) / u.sqrMagnitude;
        return (x - (x0 + Mathf.Clamp(t, 0, 1) * u)).sqrMagnitude;
    }
    
    // c OBB的中心
    // s OBB的尺寸
    // right OBB的right方向
    // p 圆盘的圆心
    // r 圆盘的半径
    public static bool IsObbDiskIntersect(Vector2 c, Vector2 s, Vector2 right, Vector2 p, float r)
    {
        // 计算出Obb局部空间的 p
        Vector2 d = p - c;
        float px = Vector2.Dot(d, right);
        float py = Vector2.Dot(d, new Vector2(-right.y, right.x));

        return IsAabbDiskIntersect(Vector2.zero, s, new Vector2(px, py), r);
    }

    // c AABB的中心
    // s AABB的尺寸
    // p 圆盘的圆心
    // r 圆盘的半径
    public static bool IsAabbDiskIntersect(Vector2 c, Vector2 s, Vector2 p, float r)
    {
        // h AABB的半长度
        Vector2 h = s * 0.5f;
        Vector2 v = Vector2.Max(p - c, c - p); // = Abs(p - c);
        Vector2 u = Vector2.Max(v - h, Vector3.zero);
        return u.sqrMagnitude <= r * r;
    }
    
    public static Vector2 To2D(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public static Vector3 To3D(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }
}