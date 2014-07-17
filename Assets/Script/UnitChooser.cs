using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;

public class UnitChooser: MonoBehaviour {
	public int index;
	public EnumAliasType aliasType;
	public EnumCharacterType openCharacterType;
	public EnumCharacterType characterType;
	public GameObject unitInfo; 
	List<int> mOpenUnitList;
	List<int> mUnitList;
	
	public bool mbOpenedSelected = false;
	public bool mbSelected = false;
	
	bool mbOpened = false;
	UnitData unitdata;
	// Use this for initialization
	void Start () {
		RefreshOpenUnitList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void RefreshUnitList() // 유닛리스트 리프레쉬 
	{
		mUnitList = PlayMgr.GetInstance().GetUnitList();
		if(mUnitList.Count > 0 && !mbOpened) // 유닛이 열리기 전에
		{
			if(mbSelected) // 유닛 선택 취소
			{
				unitInfo.gameObject.SetActive(false);
				mbSelected = false;
			}
			else{ // 유닛 선택
				characterType =(EnumCharacterType)mUnitList[index];
				unitdata = DataMgr.GetInstance().GetUnitData(characterType);

				for(int i=1; i< 10; i++)
				{
					if(gameObject.transform.parent.FindChild("Choose_"+i).FindChild("UnitInfo") != null)
					{
						gameObject.transform.parent.FindChild("Choose_"+i).FindChild("UnitInfo").gameObject.SetActive(false);
						gameObject.transform.parent.FindChild("Choose_"+i).GetComponent<UnitChooser>().mbSelected = false;
						gameObject.transform.parent.FindChild("Choose_"+i).GetComponent<UnitChooser>().mbOpenedSelected = false;
					}

				}

				switch(characterType)
				{
				case EnumCharacterType.CHARACTER_TYPE_NONE:
					break;
				case EnumCharacterType.CHARACTER_TYPE_RAT:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "unit_rat";
					unitInfo.transform.FindChild("UnitPlumTxt").GetComponent<UILabel>().text
						= unitdata.plum.ToString("N0");
					mbSelected = true;
					break;
				case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "unit_elephant";
					unitInfo.transform.FindChild("UnitPlumTxt").GetComponent<UILabel>().text
						= unitdata.plum.ToString("N0");
					mbSelected = true;
					break;
				case EnumCharacterType.CHARACTER_TYPE_HORSE:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "unit_horse";
					unitInfo.transform.FindChild("UnitPlumTxt").GetComponent<UILabel>().text
						= unitdata.plum.ToString("N0");
					mbSelected = true;
					break;
				}
			}
		}
		else if(mbOpened && mbSelected)  // 유닛이 열려있고, 열리기전에 선택되었을 때
		{
			unitInfo.SetActive(false);
			mbSelected = false;
		}
	}
	void RefreshOpenUnitList()  // 오픈된 유닛리스트 리프레쉬
	{
		mOpenUnitList = PlayMgr.GetInstance().GetOpenUnitList();
		if(mOpenUnitList.Count > 0)
		{
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
	
	void SelectUnit()
	{
		if(mbOpened)
		{
			if(!mbOpenedSelected) // 오픈된 유닛이 선택되었을 때
			{
				gameObject.transform.FindChild("Img_Selected").gameObject.SetActive(true);
				PlayMgr.GetInstance ().AddSelectedUnit(openCharacterType);

				for(int i=1; i< 10; i++)
				{
					if(gameObject.transform.parent.FindChild("Choose_"+i).FindChild("UnitInfo") != null)
					{
						gameObject.transform.parent.FindChild("Choose_"+i).FindChild("UnitInfo").gameObject.SetActive(false);
						gameObject.transform.parent.FindChild("Choose_"+i).GetComponent<UnitChooser>().mbSelected = false;
						gameObject.transform.parent.FindChild("Choose_"+i).GetComponent<UnitChooser>().mbOpenedSelected = false;
					}
				}

				switch(openCharacterType)
				{
				case EnumCharacterType.CHARACTER_TYPE_NONE:
					break;
				case EnumCharacterType.CHARACTER_TYPE_RAT:
					unitInfo.SetActive(true);
					unitInfo.transform.FindChild("UnitInfo_Img")
						.GetComponentInChildren<UISprite>().spriteName = "unit_rat";
					break;
				case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
					unitInfo.gameObject.SetActive(true);
                        unitInfo.transform.FindChild("UnitInfo_Img")
                            .GetComponentInChildren<UISprite>().spriteName = "unit_elephant";
                        break;
                    case EnumCharacterType.CHARACTER_TYPE_HORSE:
                        unitInfo.SetActive(true);
                        unitInfo.transform.FindChild("UnitInfo_Img")
                            .GetComponentInChildren<UISprite>().spriteName = "unit_horse";
                        break;
                }
                unitInfo.transform.FindChild("BuyBtn").gameObject.SetActive(false);
                unitInfo.transform.FindChild("UnitPlumTxt").gameObject.SetActive(false);
                mbOpenedSelected = true;
            }
            else // 오픈된 유닛 선택이 취소되었을 때
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
        if(PlayMgr.GetInstance().plum >= unitdata.plum)
        {
			unitdata = DataMgr.GetInstance().GetUnitData((EnumCharacterType)mUnitList[index]);
            PlayMgr.GetInstance().plum -= unitdata.plum;
            PlayMgr.GetInstance().SetOpenUnitList(characterType);
            RefreshOpenUnitList();
            RefreshUnitList();
            GameObject.FindWithTag ("plum").SendMessage("RefreshPlum");
            mbSelected = false;
        }
    }
}