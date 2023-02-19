using UnityEngine;

public class Sector : MonoBehaviour
{
    public float Radius;
    public float Angle;
    
    public bool IsHit;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = IsHit ? Color.red : Color.green;

        int segments = 100;
        float deltaAngle = Angle / segments;
        Vector3 forward = transform.forward;

        Vector3[] vertices = new Vector3[segments + 2];
        vertices[0] = transform.position;
        for (int i = 1; i < vertices.Length; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, -Angle / 2 + deltaAngle * (i - 1), 0f) * forward * Radius + transform.position;
            vertices[i] = pos;
        }
        for (int i = 1; i < vertices.Length - 1; i++)
        {
            Gizmos.DrawLine(vertices[i], vertices[i + 1]);
        }
        Gizmos.DrawLine(vertices[0], vertices[vertices.Length - 1]);
        Gizmos.DrawLine(vertices[0], vertices[1]);
    }
}