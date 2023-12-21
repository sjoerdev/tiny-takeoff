using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, int step)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        MeshData meshData = new MeshData(width / step, height / step); // Adjusted mesh size based on step
        int vertexIndex = 0;

        for (int y = 0; y < width; y += step)
        {
            for (int x = 0; x < height; x += step)
            {
                meshData.vertices[vertexIndex] = new Vector3(y, heightMap[y, x], x);

                if (x < width - step && y < height - step)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + meshData.Width + 1, vertexIndex + meshData.Width);
                    meshData.AddTriangle(vertexIndex + meshData.Width + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;

    int triangleIndex;

    public int Width { get; private set; }

    public MeshData(int meshWidth, int meshHeight)
    {
        Width = meshWidth;
        vertices = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
