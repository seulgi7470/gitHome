using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;

public class UnitChooser: MonoBehaviour {
	public int index = 0;
	public EnumAliasType aliasType;
	public EnumCharacterType characterType;
	List<int> mUnitList;
	List<int> mOpenUnitList;
	bool mbSelected = false;
	bool mbOpened = false;

	// Use this for initialization
	void Start () {
		mUnitList = PlayMgr.GetInstance().GetUnitList();
		mOpenUnitList = PlayMgr.GetInstance().GetOpenUnitList();
		if(mOpenUnitList.Count > 0)
		{
//			Debug.Log (gameObject + "  " + mOpenUnitList[index]);
			characterType = (EnumCharacterType)mOpenUnitList[index];
			switch(characterType)
			{
			case EnumCharacterType.CHARACTER_TYPE_NONE:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_lock";
				break;
			case EnumCharacterType.CHARACTER_TYPE_RAT:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_rat";
				mbOpened = true;
				break;
			case EnumCharacterType.CHARACTER_TYPE_RAT1:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_rat1";
				mbOpened = true;
				break;
			}

		}

	}
	
	// Update is called once per frame
	void Update () {

	
	}
		

	void SelectUnit()
	{
		if(mbOpened)
		{
			if(!mbSelected)
			{
				PlayMgr.GetInstance ().GetSelectList ().Add ((int)characterType);
				gameObject.transform.FindChild("Img_Selected").gameObject.SetActive(true);
				mbSelected = true;
			}
			else
			{
				PlayMgr.GetInstance().GetSelectList().Remove((int)characterType);
				gameObject.transform.FindChild("Img_Selected").gameObject.SetActive(false);
				mbSelected = false;
			}
		}
	}
}