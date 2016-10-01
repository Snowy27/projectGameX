using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	protected static T instance;

	//Return the instance or create


	public static T Instance 
	{
		get
		{
			if (instance == null)
			{
				instance = (T) FindObjectOfType(typeof(T));

				if (instance == null)
				{
					Debug.LogError ("Instance of " + typeof(T) +
						" is needed un the scene, but there is none. ");
				}
			}

			return instance;
		}
	}
}
