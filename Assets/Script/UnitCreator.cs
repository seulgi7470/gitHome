using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CommonData;
public class UnitCreator: MonoBehaviour {
	public GameObject unit;
	public UIFilledSprite mDelayWidget;

	int mButtonIndex;
	int mUnitPrice;
	float mUnitDelay;
	float mDelay;
	bool mbWaitingUnit = false;

	// Use this for initialization
	void Start () {
		mDelayWidget.fillAmount = 1.0f;
	}

	void CreateUnit(GameObject obj)
	{

		if (mbWaitingUnit) {
			return;
		}

		mButtonIndex = gameObject.GetComponent<UnitButton>().buttonIndex;
		
		EnumCharacterType charType = PlayMgr.GetInstance().GetSelectedUnitAt(mButtonIndex);

		UnitData unitData =  DataMgr.GetInstance().GetUnitData(charType);
		mUnitPrice = unitData.price;
		mUnitDelay = unitData.unitDelay;

		int sproutPrice = PlayMgr.GetInstance().sproutValue;

		if(sproutPrice >= mUnitPrice)
		{
			PlayMgr.GetInstance().sproutValue -= mUnitPrice;
			GameObject.FindWithTag("GM").SendMessage("CreateUnit", charType);
			mbWaitingUnit = true;
		}

	}
	// Update is called once per frame
	void Update () {
		if (mbWaitingUnit) {
			GameObject child = gameObject.transform.FindChild("Select_BG").gameObject;
			child.SetActive(true);
			mDelay++;
			mDelayWidget.fillAmount = mDelay / mUnitDelay;
			if (mDelay > mUnitDelay)
			{
				mbWaitingUnit = false;
				mDelay = 1;
			}
		}
		else
		{
			GameObject child = gameObject.transform.FindChild("Select_BG").gameObject;
			child.SetActive(false);
		}
	}
}