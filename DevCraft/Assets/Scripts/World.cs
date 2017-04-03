using UnityEngine;
using Noise;

public class World : MonoBehaviour {

	[SerializeField] GameObject chunk;
	[SerializeField] int worldX = 16;
	[SerializeField] int worldY = 16;
	[SerializeField] int worldZ = 16;
	[SerializeField] int chunkSize = 16;

	private byte[,,] worldData;
	private Chunk[,,] chunks;

	// Use this for initialization
	void Start () {
		worldData = new byte[worldX, worldY, worldZ];

		// For each x.
		for (int x = 0; x < worldX; x++) {
			// Then each z in the x.
			for (int z = 0; z < worldZ; z++) {
				int rock = PerlinNoise(x, 0, z, 10f, 3f, 1.2f);
				rock += PerlinNoise(x, 200, z, 20f, 8f, 0f) + 10;
				int grass = PerlinNoise(x, 100, z, 50f, 30f, 0f) + 1;
				// For each y in the z in an x.
				for (int y = 0; y < worldY; y++) {
					if (y <= rock) {
						worldData[x, y, z] = (byte) TextureType.grass.GetHashCode();
					}
					else if (y <= grass) {
						worldData[x, y, z] = (byte) TextureType.rock.GetHashCode();
					}
				}
			}
		}

		chunks = new Chunk[ Mathf.FloorToInt(worldX / chunkSize), 
												Mathf.FloorToInt(worldY / chunkSize), 
												Mathf.FloorToInt(worldZ / chunkSize) ];
		
		for (int x = 0; x < chunks.GetLength(0); x++) {
			for (int y = 0; y < chunks.GetLength(1); y++) {
				for (int z = 0; z < chunks.GetLength(2); z++) {
					GameObject newChunk = 
							Instantiate(
								chunk,
								new Vector3(x * chunkSize, y * chunkSize, z * chunkSize),
								new Quaternion(0, 0, 0, 0)) as GameObject;

					chunks[x, y, z] = newChunk.GetComponent("Chunk") as Chunk;
					chunks[x, y, z].WorldGO = gameObject;
					chunks[x, y, z].ChunkSize = chunkSize;
					chunks[x, y, z].ChunkX = x * chunkSize;
					chunks[x, y, z].ChunkY = y * chunkSize;
					chunks[x, y, z].ChunkZ = z * chunkSize;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int PerlinNoise(int x, int y, int z, float scale, float height, float power) {
		// From Noise.cs
		float perlinValue = Noise.Noise.GetNoise((double) x / scale, (double) y / scale, (double) z / scale);
		perlinValue *= height;

		if (power != 0) {
			perlinValue = Mathf.Pow(perlinValue, power);
		}

		return (int) perlinValue;
	}

	public byte Block (int x, int y, int z) {
		if (x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0) {
			return (byte) TextureType.rock.GetHashCode();
		}
		return worldData[x, y, z];
	}
}
