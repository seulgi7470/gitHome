using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;

public class StartScene : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			StartGame ();
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void StartGame() {
		Application.LoadLevel("selectstage");
	}				
}		