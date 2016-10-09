using UnityEngine;
using NUnit.Framework;

public class TileMapMeshTest {
	
	[Test]
	public void VerticesCreationTest() {

		int sizeX = 1;
		int sizeZ = 2;
		int tileSize = 1;

		Vector3[] vertices = new Vector3[6];
		vertices [0] = new Vector3 (0, 0, 0);
		vertices [1] = new Vector3 (1, 0, 0);
		vertices [2] = new Vector3 (0, 0, 1);
		vertices [3] = new Vector3 (1, 0, 1);
		vertices [4] = new Vector3 (0, 0, 2);
		vertices [5] = new Vector3 (1, 0, 2);

		TileMapMesh tileMapMesh = new TileMapMesh (sizeX, sizeZ, tileSize);
		Mesh mapMesh = tileMapMesh.generatedMesh;

		Assert.AreEqual (vertices, mapMesh.vertices);

	}

	[Test]
	public void TrianglesCreationTest() {

		int sizeX = 1;
		int sizeZ = 2;
		int tileSize = 1;

		int[] triangles = new int[12];
		triangles [0] = 2;
		triangles [1] = 1;
		triangles [2] = 0;
		triangles [3] = 2;
		triangles [4] = 3;
		triangles [5] = 1;
		triangles [6] = 4;
		triangles [7] = 3;
		triangles [8] = 2;
		triangles [9] = 4;
		triangles [10] = 5;
		triangles [11] = 3;

		TileMapMesh tileMapMesh = new TileMapMesh (sizeX, sizeZ, tileSize);
		Mesh mapMesh = tileMapMesh.generatedMesh;

		Assert.AreEqual (triangles, mapMesh.triangles);

	}

	[Test]
	public void UvsCreationTest() {

		int sizeX = 1;
		int sizeZ = 2;
		int tileSize = 1;

		Vector2[] uvs = new Vector2[6];
		uvs [0] = new Vector2 (0.0f, 0.0f);
		uvs [1] = new Vector2 (0.5f, 0.0f);
		uvs [2] = new Vector2 (0.0f, (float)1/3);
		uvs [3] = new Vector2 (0.5f, (float)1/3);
		uvs [4] = new Vector2 (0.0f, (float)2/3);
		uvs [5] = new Vector2 (0.5f, (float)2/3);

		TileMapMesh tileMapMesh = new TileMapMesh (sizeX, sizeZ, tileSize);
		Mesh mapMesh = tileMapMesh.generatedMesh;

		Assert.AreEqual (uvs, mapMesh.uv);

	}

	[Test]
	public void NormalsCreationTest() {

		int sizeX = 1;
		int sizeZ = 2;
		int tileSize = 1;

		Vector3[] normals = new Vector3[6];
		for (int i = 0; i < normals.Length; i++)
		{
			normals [i] = Vector3.up;
		}

		TileMapMesh tileMapMesh = new TileMapMesh (sizeX, sizeZ, tileSize);
		Mesh mapMesh = tileMapMesh.generatedMesh;

		Assert.AreEqual (normals, mapMesh.normals);

	}
}
