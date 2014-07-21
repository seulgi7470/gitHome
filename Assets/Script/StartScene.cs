using UnityEngine;
using System.Collections;
using CommonData;

public class StartScene : MonoBehaviour {

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
		PlayMgr.GetInstance().SetOpenUnitList(EnumCharacterType.CHARACTER_TYPE_RAT);
//		Debug.Log ("set stage No = " + PlayMgr.GetInstance().stageNo);
		Application.LoadLevel("selectstage");
	}

	public void OnPressedStartBtn(GameObject gameObj) {
		gameObj.GetComponentInChildren<UISprite>().spriteName = "btn_start_on";
	}
	
	public void OnReleasedStartBtn(GameObject gameObj) {
		gameObj.GetComponentInChildren<UISprite>().spriteName = "btn_start";
	}
}
