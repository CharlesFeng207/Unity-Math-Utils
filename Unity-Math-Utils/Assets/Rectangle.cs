using UnityEngine;

public class Rectangle : MonoBehaviour
{
    public bool IsHit;

    public float Width;
    public float Height;

    private void OnDrawGizmos()
    {
        Gizmos.color = IsHit ? Color.red : Color.green;

        Vector3[] vertices = new Vector3[4];    
        vertices[0] = transform.position + transform.right * Width / 2 + transform.forward * Height / 2;
        vertices[1] = transform.position - transform.right * Width / 2 + transform.forward * Height / 2;
        vertices[2] = transform.position - transform.right * Width / 2 - transform.forward * Height / 2;
        vertices[3] = transform.position + transform.right * Width / 2 - transform.forward * Height / 2;

        for (int i = 0; i < vertices.Length - 1; i++)
        {
            Gizmos.DrawLine(vertices[i], vertices[i + 1]);
        }

        Gizmos.DrawLine(vertices[0], vertices[vertices.Length - 1]);
    }
}