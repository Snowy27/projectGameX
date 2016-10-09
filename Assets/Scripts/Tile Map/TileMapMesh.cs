using UnityEngine;

public class TileMapMesh
{
	public Mesh generatedMesh
	{
		get
		{
			return _mesh;
		}
	}

	private Vector3[] _vertices;
	private int[] _triangles;
	private Vector3[] _normals;
	private Vector2[] _uv;

	private Mesh _mesh;

	public TileMapMesh (int sizeX, int sizeZ, float tileSize)
	{
		int verticesSizeX = sizeX + 1,
		verticesSizeZ = sizeZ + 1;

		//calculate all properties for the mesh
		_vertices = CalculateVertices(verticesSizeX, verticesSizeZ, tileSize);
		_triangles = CalculateTriangles(sizeX, sizeZ);
		_normals = CalculateNormals(verticesSizeX, verticesSizeZ);
		_uv = CalculateUV(verticesSizeX, verticesSizeZ);

		//create and set up the mesh
		_mesh = new Mesh ();

		_mesh.vertices = _vertices;
		_mesh.triangles = _triangles;
		_mesh.normals = _normals;
		_mesh.uv = _uv;
	}

	private Vector3[] CalculateVertices(int sizeX, int sizeZ, float tileSize) {
		Vector3[] vertices = new Vector3[sizeX * sizeZ];
		int index;

		for (int z = 0; z < sizeZ; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{	
				//set coordinates for current vertax
				index = z * sizeX + x;
				vertices[index] = FindCoordinatesForVertax (x * tileSize, 0.0f, z * tileSize);
			}
		}

		return vertices;
	}

	private int[] CalculateTriangles(int sizeX, int sizeZ)
	{

		int[] triangles = new int[sizeX * sizeZ * 2 * 3];
		int index;

		for (int z = 0; z < sizeZ; z++)
		{
			for (int x =0; x < sizeX; x++)
			{
				index = (z * sizeX + x) * 6;

				//first triangle for current tile
				triangles[index] = FindVerticalOffset(z + 1, sizeX + 1) + x;
				triangles[index + 1] = FindVerticalOffset(z, sizeX + 1) + x + 1;
				triangles[index + 2] = FindVerticalOffset(z, sizeX + 1) + x;

				//second triangle for current tile
				triangles[index + 3] = FindVerticalOffset(z + 1, sizeX + 1) + x;
				triangles[index + 4] = FindVerticalOffset(z + 1, sizeX + 1) + x + 1;
				triangles[index + 5] = FindVerticalOffset(z, sizeX + 1) + x + 1;
			}
		}

		return triangles;
	}

	private Vector3[] CalculateNormals(int sizeX, int sizeZ)
	{
		Vector3[] normals = new Vector3[sizeX * sizeZ];
		int index;

		for (int z = 0; z < sizeZ; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				//normal for current tile
				index = z * sizeX + x;
				normals[index] = Vector3.up;
			}
		}

		return normals;
	}

	private Vector2[] CalculateUV(int sizeX, int sizeZ)
	{
		Vector2[] uv = new Vector2[sizeX * sizeZ];
		int index;

		for (int z = 0; z < sizeZ; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				//texture placement for current vertax
				index = z * sizeX + x;
				uv[index] = new Vector2( (float)x / sizeX, (float)z / sizeZ);
			}
		}

		return uv;
	}

	private Vector3 FindCoordinatesForVertax (float x, float y, float z)
	{
		Vector3 coordinates = new Vector3(x, y, z);
		return coordinates;
	}

	private int FindVerticalOffset(int z, int sizeX)
	{
		return z * sizeX;
	}
}
