using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StageSelector : MonoBehaviour {
	public int index;
	public UILabel stageText;
	
	int stageNo;
	bool mbOpenStage;
	int[] mArrGoldPlum;

	// Use this for initialization
	void Start () {
		stageNo = PlayMgr.GetInstance().openStageNo;
	
		if(index <= stageNo)
		{
			gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_stage";
			if(index+1 / 10 == 0)
			{
				stageText.text = "0";
				stageText.text += (index + 1).ToString("N0");
			}
			else
				stageText.text = (index + 1).ToString("N0");
		}
		mArrGoldPlum = PlayMgr.GetInstance().GetArrGoldPlum();
		for(int i = 0; i <= mArrGoldPlum[index]; i++)
		{
			if(i == 0)
				continue;
			Debug.Log (mArrGoldPlum[index]);
			gameObject.transform.FindChild("GoldPlum"+i).gameObject.SetActive(true);
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
		Application.LoadLevel("savetheplum");
	}
}
