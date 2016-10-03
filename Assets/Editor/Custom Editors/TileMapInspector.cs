using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileMapGenerator))]
public class TileMapInspector : Editor
{

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		if (GUILayout.Button ("Apply"))
		{
			TileMapGenerator tileMapGenerator = (TileMapGenerator)target;
			tileMapGenerator.Generate ();
		}
	}

}

