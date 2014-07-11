using UnityEngine;
using System.Collections;

public class Plum : MonoBehaviour {

	public TextMesh plumText;
	int mPlum;
	// Use this for initialization
	void Start () {
		RefreshPlum();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RefreshPlum()
	{
		mPlum = PlayMgr.GetInstance().plum;
		plumText.text = mPlum.ToString("N0");
	}

}
