using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSensor : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1f;
    public Material meshMaterial;

    private Mesh mesh;
    private GameObject coneFOV;

    private void Start()
    {
        coneFOV = new GameObject();
        
        mesh = CreateMesh();

        coneFOV.name = "FOV";

        coneFOV.AddComponent<MeshFilter>().mesh = mesh;
        coneFOV.GetComponent<MeshFilter>().mesh.name = "FOV mesh";

        coneFOV.AddComponent<MeshRenderer>().material = meshMaterial;

        coneFOV.AddComponent<MeshCollider>().convex = true;
        coneFOV.GetComponent<MeshCollider>().isTrigger = true;

        coneFOV.AddComponent<SensorBehaviour>();

        coneFOV.transform.SetParent(gameObject.transform);

        gameObject.GetComponent<PatrollerController>().FOVsensor = coneFOV;
    }

    private Mesh CreateMesh()
    {
        Mesh _mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 4;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        _mesh.vertices = vertices;
        _mesh.triangles = triangles;

        _mesh.RecalculateNormals();

        return _mesh;
    }

    // private void OnDrawGizmos()
    // {
    //     if (mesh)
    //     {
    //         Gizmos.color = meshColor;
    //         Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
    //     }
    // }
}
