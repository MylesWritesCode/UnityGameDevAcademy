﻿using System.Collections.Generic;
using UnityEngine;

public enum TextureType {
	air, grass, rock
}

public class Chunk : MonoBehaviour {

	private List<Vector3> newVertices = new List<Vector3>();
	private List<int> newTriangles = new List<int>();
	private List<Vector2> newUV = new List<Vector2>();
	private Mesh mesh;
	private MeshCollider chunkCollider;
	private float textureWidth = 0.083f;
	private int faceCount;

	// Textures
	private Vector2 grassTop = new Vector2(1, 11);
	private Vector2 grassSide = new Vector2(0, 10);
	private Vector2 rock = new Vector2(7, 8);

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		chunkCollider = GetComponent<MeshCollider>();
		CubeTop(0, 0, 0, (byte) TextureType.rock.GetHashCode());
		UpdateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CubeTop(int x, int y, int z, byte block) {
		newVertices.Add(new Vector3(x, y, z + 1));
		newVertices.Add(new Vector3(x + 1, y, z + 1));
		newVertices.Add(new Vector3(x + 1, y, z));
		newVertices.Add(new Vector3(x, y, z));

		newTriangles.Add(faceCount * 4);     // Vertex 1
		newTriangles.Add(faceCount * 4 + 1); // Vertex 2
		newTriangles.Add(faceCount * 4 + 2); // Vertex 3
		newTriangles.Add(faceCount * 4);     // Vertex 1
		newTriangles.Add(faceCount * 4 + 2); // Vertex 3
		newTriangles.Add(faceCount * 4 + 3); // Vertex 4

		Vector2 texturePos = rock;

		newUV.Add(new Vector2(textureWidth * texturePos.x + textureWidth, textureWidth * texturePos.y));
		newUV.Add(new Vector2(textureWidth * texturePos.x + textureWidth, textureWidth * texturePos.y + textureWidth));
		newUV.Add(new Vector2(textureWidth * texturePos.x, textureWidth * texturePos.y + textureWidth));
		newUV.Add(new Vector2(textureWidth * texturePos.x, textureWidth * texturePos.y));
	}

	void UpdateMesh() {
		mesh.Clear();
		mesh.vertices = newVertices.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.triangles = newTriangles.ToArray();
		// mesh.Optimize(); //Obsolete, apparently didn't do anything in Unity for a while.
		mesh.RecalculateNormals();

		chunkCollider.sharedMesh = null;
		chunkCollider.sharedMesh = mesh;
		newVertices.Clear();
		newUV.Clear();
		newTriangles.Clear();
		faceCount = 0;
	}
}
