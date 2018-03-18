using UnityEngine;

public static class GridMetrics
{

    //size/length of a single cell edge
    public const float cellSize = 1;

    //noise scale for variation of the terrain
    public const float noiseScale = 0.003f;
    //size of a hexgridchunk
    public const int chunkSizeX = 5, chunkSizeZ = 5;
    //cell perturb variables
    public const float cellPerturbStrength = 4f;

    public static Texture2D noiseSource;

    public static Color[] colors;

    //hashgrid for random features
    public const int hashGridSize = 256;
    public const float hashGridScale = 0.25f;
    static GridHash[] hashGrid;

    public static Vector4 SampleNoise (Vector3 position)
    {
        return noiseSource.GetPixelBilinear(
            position.x * noiseScale,
            position.z * noiseScale
        );
    }

    public static Vector3 Perturb (Vector3 position)
    {
        Vector4 sample = SampleNoise(position);
        position.x += (sample.x * 2f - 1f) * cellPerturbStrength;
        position.z += (sample.z * 2f - 1f) * cellPerturbStrength;
        return position;
    }

    public static void InitializeHashGrid (int seed)
    {
        hashGrid = new GridHash[hashGridSize * hashGridSize];
        Random.State currentState = Random.state;
        Random.InitState(seed);
        for (int i = 0; i < hashGrid.Length; i++)
        {
            hashGrid[i] = GridHash.Create();
        }
        Random.state = currentState;
    }

    public enum ChunkTypes
    {
        forest,
        desert,
        sea,
        rocky,
        plane,
        nothing
    }

    public static ChunkTypes GenerateChunkType ()
    {
        int value = (int) Random.Range(0, 4);
        switch (value)
        {
            case 0:
                return ChunkTypes.forest;
            case 1:
                return ChunkTypes.desert;
            case 2:
                return ChunkTypes.sea;
            case 3:
                return ChunkTypes.rocky;
            case 4:
                return ChunkTypes.plane;
        }
        return ChunkTypes.nothing;
    }
}
