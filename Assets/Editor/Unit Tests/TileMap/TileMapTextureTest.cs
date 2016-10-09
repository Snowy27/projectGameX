using UnityEngine;
using NUnit.Framework;

public class TileMapTextureTest {

	[Test]
	public void TextureCreationTest() {
		
		var pixelSize = 2; 
		var tileSize = 3;

		Texture2D tileTexture = new Texture2D (pixelSize, pixelSize);
		Color[] tilePixels = new Color[pixelSize * pixelSize];
		for (int i = 0; i < tilePixels.Length; i++)
		{
			tilePixels [i] = Color.black;
		}
		tileTexture.SetPixels (tilePixels);

		Color[] mapTexturePixels = new Color[pixelSize * pixelSize * tileSize * tileSize];
		for (int i = 0; i < mapTexturePixels.Length; i++)
		{
			mapTexturePixels [i] = Color.black;
		}

		TileMapTexture tileMapTexture = new TileMapTexture(tileSize, tileSize, tileTexture, pixelSize);
		Texture2D mapTexture = tileMapTexture.generatedTexture;

		Assert.AreEqual (6, mapTexture.width);
		Assert.AreEqual (6, mapTexture.height);
		Assert.AreEqual (mapTexture.filterMode, FilterMode.Point);
		Assert.AreEqual (mapTexture.wrapMode, TextureWrapMode.Clamp);
		Assert.AreEqual (mapTexturePixels, mapTexture.GetPixels ());

	}
}
