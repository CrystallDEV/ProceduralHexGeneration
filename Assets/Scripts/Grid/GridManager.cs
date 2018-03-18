using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{

    public int chunkCountX = 4, chunkCountZ = 3;

    public GridCell cellPrefab;
    public GridChunk chunkPrefab;

    public Texture2D noiseSource;

    public int seed;

    GridChunk[] chunks;
    GridCell[] cells;

    public Color[] colors;

    int cellCountX, cellCountZ;

    void Awake ()
    {
        GridMetrics.noiseSource = noiseSource;
        GridMetrics.InitializeHashGrid(seed);
        GridMetrics.colors = colors;

        cellCountX = chunkCountX * GridMetrics.chunkSizeX;
        cellCountZ = chunkCountZ * GridMetrics.chunkSizeZ;

        CreateChunks();
        CreateCells();
    }

    void OnEnable ()
    {
        if (!GridMetrics.noiseSource)
        {
            GridMetrics.noiseSource = noiseSource;
            GridMetrics.InitializeHashGrid(seed);
            GridMetrics.colors = colors;
        }
    }

    void CreateChunks ()
    {
        chunks = new GridChunk[chunkCountX * chunkCountZ];
        for (int z = 0, i = 0; z < chunkCountZ; z++)
        {
            for (int x = 0; x < chunkCountX; x++)
            {
                GridChunk chunk = chunks[i++] = Instantiate(chunkPrefab);
                chunk.transform.SetParent(transform);
                chunk.transform.position = new Vector3(x * GridMetrics.chunkSizeX, 0, z * GridMetrics.chunkSizeZ);
                chunk.chunkTypes = GridMetrics.GenerateChunkType();
            }
        }
    }

    void CreateCells ()
    {
        cells = new GridCell[cellCountZ * cellCountX];

        for (int z = 0, i = 0; z < cellCountZ; z++)
        {
            for (int x = 0; x < cellCountX; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell (int x, int z, int i)
    {
        Vector3 position;
        position.x = x;
        position.y = 0f;
        position.z = z;

        GridCell cell = cells[i] = Instantiate<GridCell>(cellPrefab);
        cell.transform.localPosition = position;        

        AddCellToChunk(x, z, cell);
        cell.TerrainTypeIndex = (int) cell.chunk.chunkTypes;
    }

    void AddCellToChunk (int x, int z, GridCell cell)
    {
        int chunkX = x / GridMetrics.chunkSizeX;
        int chunkZ = z / GridMetrics.chunkSizeZ;
        GridChunk chunk = chunks[chunkX + chunkZ * chunkCountX];

        int localX = x - chunkX * GridMetrics.chunkSizeX;
        int localZ = z - chunkZ * GridMetrics.chunkSizeZ;
        chunk.AddCell(localX + localZ * GridMetrics.chunkSizeX, cell);
    }
}