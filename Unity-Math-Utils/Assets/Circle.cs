using UnityEngine;

public class Circle : MonoBehaviour
{
    public float Radius;
    public bool IsHit;
    private void OnDrawGizmos()
    {
        Gizmos.color = IsHit ? Color.red : Color.white;
        
        int segments = 100;
        float deltaAngle = 360f / segments;
        Vector3 forward = transform.forward;

        Vector3[] vertices = new Vector3[segments];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, deltaAngle * i, 0f) * forward * Radius + transform.position;
            vertices[i] = pos;
        }
        for (int i = 0; i < vertices.Length - 1; i++)
        {
            Gizmos.DrawLine(vertices[i], vertices[i + 1]);
        }
        Gizmos.DrawLine(vertices[0], vertices[vertices.Length - 1]);
    }
}
