using UnityEngine;

public class GridChunk : MonoBehaviour
{
    public GridMesh terrain;

    GridCell[] cells;

    public GridMetrics.ChunkTypes chunkTypes;

    public void Awake ()
    {
        cells = new GridCell[GridMetrics.chunkSizeX * GridMetrics.chunkSizeZ];
    }


    public void AddCell (int index, GridCell cell)
    {
        cells[index] = cell;
        cell.chunk = this;
        cell.transform.SetParent(transform, false);
    }

    public void Refresh ()
    {
        enabled = true;
    }

    void LateUpdate ()
    {
        Triangulate();
        enabled = false;
    }

    public void Triangulate ()
    {
        terrain.Clear();

        Vector3[] vertices = new Vector3[(GridMetrics.chunkSizeX + 1) * (GridMetrics.chunkSizeZ + 1)];

        for (int i = 0, z = 0; z <= GridMetrics.chunkSizeZ; z++)
        {
            for (int x = 0; x <= GridMetrics.chunkSizeX; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);                
            }
        }

        int[] triangles = new int[GridMetrics.chunkSizeX * GridMetrics.chunkSizeZ * 6];
        for (int ti = 0, vi = 0, y = 0; y < GridMetrics.chunkSizeZ; y++, vi++)
        {
            for (int x = 0; x < GridMetrics.chunkSizeX; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + GridMetrics.chunkSizeX + 1;
                triangles[ti + 5] = vi + GridMetrics.chunkSizeX + 2;
            }
        }
        terrain.SetMesh(vertices,triangles, GridMetrics.colors[(int)chunkTypes]);

        for(int i = 0; i<cells.Length; i++)
        {
            
        }

        terrain.Apply();
    }
}
