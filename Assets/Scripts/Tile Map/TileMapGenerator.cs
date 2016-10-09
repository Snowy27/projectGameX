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
    public int sizeX = 100 ;
    public int sizeZ = 50;
	public float tileSize = 1;

	public Texture2D tileTexture;
	public int tileResolution = 64;

	private TileMap _tileMap;

    public void GenerateTileMap (MeshFilter filter, MeshCollider collider, MeshRenderer renderer)
    {
		//generate tile map and render it on given components
		_tileMap = new TileMap(sizeX, sizeZ, tileSize, tileResolution, tileTexture);
        _tileMap.RenderTileMap(filter, collider, renderer);
    }
}

