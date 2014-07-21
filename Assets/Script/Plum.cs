using UnityEngine;
using System.Collections;

public class Plum : MonoBehaviour {

	public UILabel plumText;
	int mPlum;
	int mTempPlum;
	// Use this for initialization
	void Start () {
		RefreshPlum();
		ClearTempPlum ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RefreshPlum()
	{
		mPlum = PlayMgr.GetInstance().plum;
		plumText.text = mPlum.ToString("N0");
	}

	public void ClearTempPlum()
	{
		mTempPlum = mPlum;
	}

	public void SaveTempPlumInGame(int plum)
	{
		mTempPlum += plum;
		plumText.text = mTempPlum.ToString("N0");
	}

	public void SavePlum()
	{
		mPlum = mTempPlum;
		PlayMgr.GetInstance().plum = mPlum;
	}
}
