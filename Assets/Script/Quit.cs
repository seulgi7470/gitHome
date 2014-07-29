using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	bool mbQuit = false;
	float time = 0;
	public GameObject quitObj;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
	  	{
			quitObj.SetActive(true);
		}
	}

	public void QuitGame()
	{
		Application.Quit();        
    }

	public void ClosePopup()
	{
		quitObj.SetActive(false);
	}
}
