using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;

public class StartScene : MonoBehaviour {

	public GameObject OpenUnitPool;

	List<int> mOpenUnitList;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void StartGame() {
		PlayMgr.GetInstance().currentStageNo = 0;
//		PlayMgr.GetInstance().openStageNo = 0;
//		PlayMgr.GetInstance().plum = 0;
//		Debug.Log ("set stage No = " + PlayMgr.GetInstance().stageNo);
//		PlayMgr.GetInstance().SetOpenUnitList(EnumCharacterType.CHARACTER_TYPE_RAT);
		PlayMgr.GetInstance().GetOpenUnitList();
		Application.LoadLevel("selectstage");
	}										

	public void OnPressedStartBtn(GameObject gameObj) {
		gameObj.GetComponentInChildren<UISprite>().spriteName = "btn_start_on";
	}
	
	public void OnReleasedStartBtn(GameObject gameObj) {
		gameObj.GetComponentInChildren<UISprite>().spriteName = "btn_start";
	}
}
