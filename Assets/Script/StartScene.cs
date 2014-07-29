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
		if( Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			StartGame ();
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void StartGame() {
		PlayMgr.GetInstance().currentStageNo = 0;
//		PlayMgr.GetInstance().openStageNo = 0;
//		PlayMgr.GetInstance().plum = 0;
//		PlayMgr.GetInstance().ClearArrGoldPlum();
//		PlayMgr.GetInstance().SetOpenUnitList(EnumCharacterType.CHARACTER_TYPE_RAT);
		PlayMgr.GetInstance().GetOpenUnitList();
		Application.LoadLevel("selectstage");
	}										

}
