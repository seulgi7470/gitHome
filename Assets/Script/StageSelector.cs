using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StageSelector : MonoBehaviour {
	public int index;
	int stageNo;
	// Use this for initialization
	void Start () {
		stageNo = PlayMgr.GetInstance().openStageNo;
	
		if(index <= stageNo)
		{
			gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_stage";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectStage(GameObject gameObj) {
		if(index > stageNo)
		{
			return;
		}
		PlayMgr.GetInstance().currentStageNo = gameObj.GetComponent<StageSelector>().index;
	//	Debug.Log ("asd " + PlayMgr.GetInstance().currentStageNo ); 
		Application.LoadLevel("savetheplum");
	}
}
