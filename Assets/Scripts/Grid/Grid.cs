using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{

    public int sizeX, sizeZ;

    private Mesh mesh;

    public Vector3[] vertices;

    public void Awake ()
    {
        Generate();
    }

    void Generate ()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(sizeX + 1) * (sizeZ + 1)];

        for (int i = 0, z = 0; z <= sizeZ; z++)
        {
            for (int x = 0; x <= sizeZ; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);
            }
        }
        mesh.vertices = vertices;

        int[] triangles = new int[sizeX * sizeZ * 6];
        for (int ti = 0, vi = 0, y = 0; y < sizeZ; y++, vi++)
        {
            for (int x = 0; x < sizeX; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + sizeX + 1;
                triangles[ti + 5] = vi + sizeX + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


    }
}
