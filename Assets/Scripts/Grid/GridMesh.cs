using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour
{

    public bool useCollider, useUVCoordinates, useUV2Coordinates, useColors;

    [NonSerialized]
    List<Vector3> vertices;
    [SerializeField]
    public List<Color> colors;
    [SerializeField]
    List<Vector2> uvs, uv2s;
    [NonSerialized]
    List<int> triangles;


    Mesh gridMesh;
    MeshCollider meshCollider;


    void Awake ()
    {
        GetComponent<MeshFilter>().mesh = gridMesh = new Mesh();
        if (useCollider)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }
        gridMesh.name = "Hex Mesh";
    }

    public void Clear ()
    {
        gridMesh.Clear();
        vertices = ListPool<Vector3>.Get();
        if (useColors)
        {
            colors = ListPool<Color>.Get();
        }
        if (useUVCoordinates)
        {
            uvs = ListPool<Vector2>.Get();
        }
        if (useUV2Coordinates)
        {
            uv2s = ListPool<Vector2>.Get();
        }
        triangles = ListPool<int>.Get();
    }

    public void Apply ()
    {
        gridMesh.SetVertices(vertices);
        ListPool<Vector3>.Add(vertices);
        if (useColors)
        {
            gridMesh.SetColors(colors);
            ListPool<Color>.Add(colors);
        }
        if (useUVCoordinates)
        {
            gridMesh.SetUVs(0, uvs);
            ListPool<Vector2>.Add(uvs);
        }
        if (useUV2Coordinates)
        {
            gridMesh.SetUVs(1, uv2s);
            ListPool<Vector2>.Add(uv2s);
        }
        gridMesh.SetTriangles(triangles, 0);
        ListPool<int>.Add(triangles);
        gridMesh.RecalculateNormals();
        if (useCollider)
        {
            meshCollider.sharedMesh = gridMesh;
        }
    }

    public void SetMesh (Vector3[] verticesList, int[] trianglesList, Color color)
    {
        for (int i = 0; i < verticesList.Length; i++)
        {
            vertices.Add(verticesList[i]);
            AddColor(color);
        }
        for (int t = 0; t < trianglesList.Length; t++)
        {
            triangles.Add(trianglesList[t]);
        }
    }

    public void AddColor (Color color)
    {
        colors.Add(color);
    }
}


