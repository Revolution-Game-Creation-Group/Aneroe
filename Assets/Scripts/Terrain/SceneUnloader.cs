﻿using UnityEngine;
using System.Collections;

public class SceneUnloader : MonoBehaviour
{
	public string sceneName;
	SceneController sc;

	// Use this for initialization
	void Start ()
	{
		sc = FindObjectOfType<SceneController> ();
	}
	

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Removing Level");
		sc.removeScene (sceneName);
	}
}
