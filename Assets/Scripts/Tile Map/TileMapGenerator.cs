using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TileMapGenerator : MonoBehaviour
{
    public TileMapGeneratorController controller;

	// Use this for initialization
	void Start ()
    {
		Generate ();
	}

	public void Generate ()
	{
		MeshFilter filter = GetComponent<MeshFilter>();
		MeshCollider collider = GetComponent<MeshCollider>();
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		controller.GenerateTileMap (filter, collider, renderer);
	}

	// Update is called once per frame
	void Update ()
    {

	}
}

[System.Serializable]
public class TileMapGeneratorController
{
    private TileMap _tileMap;
    public int sizeX = 100 ;
    public int sizeZ = 50;
	public float tileSize = 1;

    public void GenerateTileMap (MeshFilter filter, MeshCollider collider, MeshRenderer renderer)
    {
		_tileMap = new TileMap(sizeX, sizeZ, tileSize);
        _tileMap.RenderTileMap(filter, collider, renderer);
    }
}

[System.Serializable]
class TileMap
{
    private int _sizeX;
    private int _sizeZ;
	private float _tileSize;

    private int _verticesSizeX;
    private int _verticesSizeZ;

    private Vector3[] _vertices;
    private int[] _triangles;
    private Vector3[] _normals;
    private Vector2[] _uv;

	public TileMap (int sizeX, int sizeZ, float tileSize)
    {
        _sizeX = sizeX;
        _sizeZ = sizeZ;
		_tileSize = tileSize;

        _verticesSizeX = _sizeX + 1;
        _verticesSizeZ = _sizeZ + 1;

        _vertices = CalculateVertices();
        _triangles = CalculateTriangles();
        _normals = CalculateNormals();
        _uv = CalculateUV();

    }

    public void RenderTileMap (MeshFilter filter, MeshCollider collider, MeshRenderer renderer)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = _vertices;
        mesh.triangles = _triangles;
        mesh.normals = _normals;
        mesh.uv = _uv;

        filter.mesh = mesh;
		collider.sharedMesh = mesh;
    }

    private Vector3[] CalculateVertices() {
        Vector3[] vertices = new Vector3[_verticesSizeX * _verticesSizeZ];
        int index;

        for (int z = 0; z < _verticesSizeZ; z++)
        {
            for (int x = 0; x < _verticesSizeX; x++)
            {
                index = z * _verticesSizeX + x;
				vertices[index] = FindCoordinatesForVertax (x * _tileSize, 0.0f, z * _tileSize);
            }
        }

        return vertices;
    }

    private int[] CalculateTriangles()
    {

        int[] triangles = new int[_sizeX * _sizeZ * 2 * 3];
        int index;

        for (int z = 0; z < _sizeZ; z++)
        {
            for (int x =0; x < _sizeX; x++)
            {
                index = (z * _sizeX + x) * 6;

                triangles[index] = FindVerticalOffset(z + 1) + x;
                triangles[index + 1] = FindVerticalOffset(z) + x + 1;
                triangles[index + 2] = FindVerticalOffset(z) + x;

                triangles[index + 3] = FindVerticalOffset(z + 1) + x;
                triangles[index + 4] = FindVerticalOffset(z + 1) + x + 1;
                triangles[index + 5] = FindVerticalOffset(z) + x + 1;
            }
        }

        return triangles;
    }

    private Vector3[] CalculateNormals()
    {
        Vector3[] normals = new Vector3[_verticesSizeX * _verticesSizeZ];
        int index;

        for (int z = 0; z < _verticesSizeZ; z++)
        {
            for (int x = 0; x < _verticesSizeX; x++)
            {
                index = z * _verticesSizeX + x;
                normals[index] = Vector3.up;
            }
        }

        return normals;
    }

    private Vector2[] CalculateUV()
    {
        Vector2[] uv = new Vector2[_verticesSizeX * _verticesSizeZ];
        int index;

        for (int z = 0; z < _verticesSizeZ; z++)
        {
            for (int x = 0; x < _verticesSizeX; x++)
            {
                index = z * _verticesSizeX + x;
                uv[index] = new Vector2( (float)x / _verticesSizeX, (float)z / _verticesSizeZ);
            }
        }

        return uv;
    }

	private Vector3 FindCoordinatesForVertax (float x, float y, float z)
    {
        Vector3 coordinates = new Vector3(x, y, z);
        return coordinates;
    }

    private int FindVerticalOffset(int z)
    {
		return z * _verticesSizeX;
    }
}
