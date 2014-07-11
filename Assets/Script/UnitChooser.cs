using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;

public class UnitChooser: MonoBehaviour {
	public int index = 0;
	public EnumAliasType aliasType;
	public EnumCharacterType openCharacterType;
	public EnumCharacterType characterType;
	public GameObject unitInfo; 
	List<int> mOpenUnitList;
	List<int> mUnitList;
	bool mbOpenedSelected = false;
	bool mbSelected = false;
	bool mbOpened = false;

	// Use this for initialization
	void Start () {
		//mUnitList = PlayMgr.GetInstance().GetUnitList();
		RefreshOpenUnitList();

	}
	
	// Update is called once per frame
	void Update () {

	
	}
		
	void RefreshOpenUnitList()
	{
		mOpenUnitList = PlayMgr.GetInstance().GetOpenUnitList();
		if(mOpenUnitList.Count > 0)
		{
			//			Debug.Log (gameObject + "  " + mOpenUnitList[index]);
			openCharacterType = (EnumCharacterType)mOpenUnitList[index];
			switch(openCharacterType)
			{
			case EnumCharacterType.CHARACTER_TYPE_NONE:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_lock";
				break;
			case EnumCharacterType.CHARACTER_TYPE_RAT:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_rat";
				mbOpened = true;
				break;
			case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_elephant";
				mbOpened = true;
				break;
			case EnumCharacterType.CHARACTER_TYPE_HORSE:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_horse";
				mbOpened = true;
				break;
			}
		}
	}

	void RefreshUnitList()
	{
		mUnitList = PlayMgr.GetInstance().GetUnitList();
		if(mUnitList.Count > 0 && !mbOpened)
		{
			if(mbSelected)
			{
				gameObject.transform.FindChild("UnitInfo").gameObject.SetActive(false);
				mbSelected = false;
			}
			else{
				characterType =(EnumCharacterType)mUnitList[index];
				switch(characterType)
				{
				case EnumCharacterType.CHARACTER_TYPE_NONE:
					break;
				case EnumCharacterType.CHARACTER_TYPE_RAT:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "쥐";
					mbSelected = true;
					break;
				case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "코끼리";
					mbSelected = true;
					break;
				case EnumCharacterType.CHARACTER_TYPE_HORSE:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "말";
					mbSelected = true;
					break;
				}
			}
		}
		else if(mbOpened)
		{
			unitInfo.SetActive(false);
			mbSelected = false;
		}
	}

	void SelectUnit()
	{
		if(mbOpened)
		{
			if(!mbOpenedSelected) // --------------- setActive 아아아아아아악
			{
				PlayMgr.GetInstance ().AddSelectedUnit(openCharacterType);
				gameObject.transform.FindChild("Img_Selected").gameObject.SetActive(true);
				Debug.Log ("11" + openCharacterType);
				switch(openCharacterType)
				{
				case EnumCharacterType.CHARACTER_TYPE_NONE:
					break;
				case EnumCharacterType.CHARACTER_TYPE_RAT:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "쥐";
					break;
				case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "코끼리";
					break;
				case EnumCharacterType.CHARACTER_TYPE_HORSE:
					unitInfo.SetActive(true);
					Debug.Log ("22" + unitInfo.activeSelf);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "말";
			//		Debug.Log ("33");
					break;
				}
		//		Debug.Log ("44");
		//		gameObject.transform.FindChild("UnitInfo").FindChild("BuyBtn").gameObject.SetActive(false);
		//		Debug.Log ("55");
		//		gameObject.transform.FindChild("UnitInfo").FindChild("UnitPlumText").gameObject.SetActive(false);
				mbOpenedSelected = true;
			}
			else
			{
				PlayMgr.GetInstance().RemoveSelectedUnit(openCharacterType);
				gameObject.transform.FindChild("Img_Selected").gameObject.SetActive(false);
				unitInfo.SetActive(false);

				mbOpenedSelected = false;
			}
		}
		RefreshUnitList();

	}

	public void BuyUnit(GameObject gameObj)
	{
		UnitData unitdata = DataMgr.GetInstance().GetUnitData(characterType);
		if(PlayMgr.GetInstance().plum >= unitdata.plum)
		{
			PlayMgr.GetInstance().plum -= unitdata.plum;
			PlayMgr.GetInstance().SetOpenUnitList(characterType);
			RefreshOpenUnitList();
			RefreshUnitList();
			GameObject.FindWithTag ("plum").SendMessage("RefreshPlum");
		}


	}
}