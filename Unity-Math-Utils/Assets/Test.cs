using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Test : MonoBehaviour
{
    public Circle Circle;
    public Rectangle Rectangle;
    public Sector Sector;

    // Update is called once per frame
    void Update()
    {
        // Sector hit test.
        var circlePos = Circle.transform.position.To2D();
        var circleRadius = Circle.Radius;
        var sectorPos = Sector.transform.position.To2D();
        var sectorDir = Sector.transform.forward.To2D();
        var sectorTheta = Sector.Angle / 2f * Mathf.Deg2Rad;
        var sectorHit =
            MathUtils.IsSectorDiskIntersect(sectorPos, sectorDir, sectorTheta, Sector.Radius, circlePos, circleRadius);
        Sector.IsHit = sectorHit;

        // OBB hit test.
        var obbPos = Rectangle.transform.position.To2D();
        var obbSize = new Vector2(Rectangle.Width, Rectangle.Height);
        
        // var isObbHit = MathUtils.IsAabbDiskIntersect(obbPos, obbSize, circlePos, circleRadius);
        var isObbHit =
            MathUtils.IsObbDiskIntersect(obbPos, obbSize, Rectangle.transform.right.To2D(), circlePos, circleRadius);

        Rectangle.IsHit = isObbHit;
        
        Circle.IsHit = sectorHit || isObbHit;
    }
}