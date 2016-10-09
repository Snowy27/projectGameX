using UnityEngine;

[System.Serializable]
class TileMap
{
	private int _sizeX;
	private int _sizeZ;
	private float _tileSize;

	private int _verticesSizeX;
	private int _verticesSizeZ;

	private TileMapMesh _tileMapMesh;
	private TileMapTexture _tileMapTexture;


	public TileMap (int sizeX, int sizeZ, float tileSize, int tileResolution, Texture2D tileTexture)
	{
		_sizeX = sizeX;
		_sizeZ = sizeZ;
		_tileSize = tileSize;

		_verticesSizeX = _sizeX + 1;
		_verticesSizeZ = _sizeZ + 1;

		//create tile map mesh and tile map texture
		_tileMapMesh = new TileMapMesh (_sizeX, _sizeZ, _tileSize);
		_tileMapTexture = new TileMapTexture (_verticesSizeX, _verticesSizeZ, tileTexture, tileResolution);
	}

	public void RenderTileMap ( MeshFilter filter, MeshCollider collider, MeshRenderer renderer)
	{
		Mesh mesh = _tileMapMesh.generatedMesh;
		Texture2D texture = _tileMapTexture.generatedTexture;

		//apply tile map mesh and tile map texture to the given filter, collider and renderer
		filter.mesh = mesh;
		collider.sharedMesh = mesh;
		renderer.sharedMaterials [0].mainTexture = texture;
	}
}