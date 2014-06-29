using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StageSelector : MonoBehaviour {

	int stageNo;
	// Use this for initialization
	void Start () {
		stageNo = PlayMgr.GetInstance().openStageNo;

		for(int i=1; i<= stageNo + 1; i++)
		{
			if(gameObject.ToString().Substring(6).Contains(i.ToString()))
			{
				gameObject.GetComponent<UISprite>().spriteName = "btn_stage";
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelectStage() {
//		Debug.Log(System.Int32.Parse(gameObject.ToString().Substring(6,1)));
		PlayMgr.GetInstance ().currentStageNo = System.Int32.Parse(gameObject.ToString().Substring(6,1)) - 1;
		Application.LoadLevel("savetheplum");
	}
}
