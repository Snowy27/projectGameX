using UnityEngine;

public class TileMapTexture
{
	public Texture2D generatedTexture
	{
		get
		{
			return _mapTexture;
		}
	}

	private int _textureWidth;
	private int _textureHeight;
	private int _tileResolution;
	private Texture2D _tileTexture;
	private Color[] _tilePixels;

	private Texture2D _mapTexture;

	public TileMapTexture (int sizeX, int sizeZ, Texture2D tileTexture, int tileResolution)
	{
		_tileResolution = tileResolution;
		_textureWidth = sizeX * _tileResolution;
		_textureHeight = sizeZ * _tileResolution;
		_tileTexture = tileTexture;

		_tilePixels = CalculateTilePixels ();

		_mapTexture = CreateMapTexture (sizeX, sizeZ);
	}

	private Color[] CalculateTilePixels ()
	{	
		//return pixels for tile
		return _tileTexture.GetPixels (0, 0, _tileResolution, _tileResolution);
	}

	private Texture2D CreateMapTexture (int sizeX, int sizeZ)
	{

		Texture2D mapTexture = new Texture2D (_textureWidth, _textureHeight);

		for (int z = 0; z < sizeZ; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				//set pixels for current tile
				mapTexture.SetPixels (x * _tileResolution, z * _tileResolution, _tileResolution, _tileResolution, _tilePixels);
			}
		}

		mapTexture.filterMode = FilterMode.Point;
		mapTexture.wrapMode = TextureWrapMode.Clamp;
		mapTexture.Apply ();

		return mapTexture;
	}
}