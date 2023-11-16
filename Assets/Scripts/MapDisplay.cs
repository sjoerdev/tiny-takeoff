using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public void DrawMesh(MeshData meshData)
    {
        var data = meshData.CreateMesh();
        meshFilter.sharedMesh = data;
        meshCollider.sharedMesh = data;
    }
}
