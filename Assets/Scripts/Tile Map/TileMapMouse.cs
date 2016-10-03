using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapMouse : MonoBehaviour {

	public Transform selectionCubeTransform;

	private TileMapGenerator _tileMapGenerator;
	private Vector3 _currentTile;

	void Start ()
	{
		_tileMapGenerator = GetComponent <TileMapGenerator> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hitInfo;
		Collider collider = GetComponent <Collider> ();

		if (collider.Raycast(ray, out hitInfo, Mathf.Infinity))
		{
			int x = Mathf.FloorToInt (hitInfo.point.x / _tileMapGenerator.controller.tileSize);
			int z = Mathf.FloorToInt (hitInfo.point.z / _tileMapGenerator.controller.tileSize);

			_currentTile.x = x;
			_currentTile.z = z;

			selectionCubeTransform.transform.position = _currentTile;
		}
	}
}