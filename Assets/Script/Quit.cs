using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	bool mbQuit = false;
	float time = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
	  	{
			if(mbQuit)
			{
				mbQuit = false;
				Application.Quit();
			}
			else
			{
				mbQuit = true;
				time += Time.deltaTime;
				if(time > 2.0f)
				{
					time = 0;
					mbQuit = false;
				}
			}

		}
	}
}
