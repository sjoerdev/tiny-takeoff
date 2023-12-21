using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class EndlessTerrain : MonoBehaviour 
{
	public const float maxViewDst = 512;
	public Transform viewer;

	public static Vector2 viewerPosition;
	public int chunkSize = 32;
	int chunksVisibleInViewDst;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    public float scale;
    public int octaves;
	public int amplitude;
	public int lod = 2;
	public GameObject chunkPrefab;

	void Start()
	{
		chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
	}

	void Update() 
	{
		viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
		UpdateVisibleChunks();
	}

	public GameObject GenerateChunk(Vector2 offset)
	{
		var chunk = Instantiate(chunkPrefab);
		float[,] heightMap = Noise.GenerateHeightMap(chunkSize, chunkSize, scale, octaves, amplitude, offset);
		var meshData = MeshGenerator.GenerateTerrainMesh(heightMap, lod);
		var mesh = meshData.CreateMesh();
		chunk.GetComponent<MeshFilter>().sharedMesh = mesh;
		chunk.GetComponent<MeshCollider>().sharedMesh = mesh;
		return chunk;
	}
		
	void UpdateVisibleChunks() 
	{
		for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
		{
			terrainChunksVisibleLastUpdate[i].SetVisible(false);
		}
		terrainChunksVisibleLastUpdate.Clear();
			
		int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

		for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++) 
		{
			for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++) 
			{
				Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainChunkDictionary.ContainsKey(viewedChunkCoord)) 
				{
					terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
					if (terrainChunkDictionary[viewedChunkCoord].IsVisible()) 
					{
						terrainChunksVisibleLastUpdate.Add(terrainChunkDictionary[viewedChunkCoord]);
					}
				} else 
				{
					terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform, gameObject, lod));
				}
			}
		}
	}

	public class TerrainChunk 
	{
		GameObject chunk;
		Vector2 position;
		Bounds bounds;

		public TerrainChunk(Vector2 coord, int size, Transform parent, GameObject endlessTerrainObject, int lod) 
		{
			position = coord * (size - lod);
			bounds = new Bounds(position,Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x, 0, position.y);

			chunk = endlessTerrainObject.GetComponent<EndlessTerrain>().GenerateChunk(position);
			chunk.transform.position = positionV3;
			chunk.transform.parent = parent;

			SetVisible(false);
		}

		public void UpdateTerrainChunk() 
		{
			float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance (viewerPosition));
			bool visible = viewerDstFromNearestEdge <= maxViewDst;
			SetVisible (visible);
		}

		public void SetVisible(bool visible) 
		{
			chunk.SetActive(visible);
		}

		public bool IsVisible() 
		{
			return chunk.activeSelf;
		}

	}
}
