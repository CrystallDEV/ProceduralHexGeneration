using UnityEngine;

public class GridCell : MonoBehaviour {

    public GridChunk chunk;

    int terrainTypeIndex;

    public int TerrainTypeIndex
    {
        get
        {
            return terrainTypeIndex;
        }
        set
        {
            if (terrainTypeIndex != value)
            {
                terrainTypeIndex = value;
                Refresh();
            }
        }
    }

    public Color Color
    {
        get
        {
            return GridMetrics.colors[terrainTypeIndex];
        }
    }

    void Refresh ()
    {
        if (chunk)
        {
            chunk.Refresh();
        }
    }
}
