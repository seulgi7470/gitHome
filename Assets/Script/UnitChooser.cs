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
	int mPlum;
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
				if( index >= mUnitList.Count )
					return;
				characterType =(EnumCharacterType)mUnitList[index];
				unitdata = DataMgr.GetInstance().GetUnitData(characterType);

				for(int i=1; i< 10; i++)
				{
					if(unitInfo != null)
					{
						UnitChooser unitChooser = gameObject.transform.parent.FindChild("Choose_"+i).GetComponent<UnitChooser>();
						unitChooser.unitInfo.gameObject.SetActive(false);
						unitChooser.mbSelected = false;
						unitChooser.mbOpenedSelected = false;
					}
				}

				do
				{
					if( characterType == EnumCharacterType.CHARACTER_TYPE_NONE )
						break;
					string spriteName = "";
					switch(characterType)
					{
					case EnumCharacterType.CHARACTER_TYPE_RAT:
						spriteName = "unit_rat";
						break;
					case EnumCharacterType.CHARACTER_TYPE_ELEPHANT: 
						spriteName = "unit_elephant";
						break;
					case EnumCharacterType.CHARACTER_TYPE_HORSE:
						spriteName = "unit_horse";
						break;
					case EnumCharacterType.CHARACTER_TYPE_ALPACA:
						spriteName = "unit_alpaca";
						break;
					}
					unitInfo.SetActive(true);

					UISprite imgSprite = unitInfo.transform.FindChild("UnitInfo_Img").GetComponent<UISprite>();
					imgSprite.spriteName = spriteName;
					imgSprite.MakePixelPerfect();
			
					UILabel txtLabel = unitInfo.transform.FindChild("UnitPlumTxt").GetComponent<UILabel>();
					txtLabel.text = unitdata.name + "\n\n";
					txtLabel.text += "황금자두 " + unitdata.plum.ToString("N0") + "에\n합류 가능";

					GameObject btnBuy = unitInfo.transform.FindChild("BuyBtn").gameObject;
					btnBuy.SetActive(true);
					btnBuy.GetComponent<UIButtonMessage>().target = gameObject;

					mbSelected = true;

				}while(false);
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
			if( index >= mOpenUnitList.Count )
				return;
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
			case EnumCharacterType.CHARACTER_TYPE_ALPACA:
				gameObject.GetComponentInChildren<UISprite>().spriteName = "btn_unit_alpaca";
				mbOpened = true;
				break;
			}
		}
	}
	
	void SelectUnit()
	{
		if(mbOpened)
		{
			if(gameObject.transform.FindChild("Img_Selected").gameObject.activeSelf)
				mbOpenedSelected = true;
			if(!mbOpenedSelected) // 오픈된 유닛이 선택되었을 때
			{
				gameObject.transform.FindChild("Img_Selected").gameObject.SetActive(true);
				PlayMgr.GetInstance ().AddSelectedUnit(openCharacterType);

				for(int i=1; i< 10; i++)
				{
					if(unitInfo != null)
					{
						UnitChooser unitChooser = gameObject.transform.parent.FindChild("Choose_"+i).GetComponent<UnitChooser>();
						unitChooser.unitInfo.gameObject.SetActive(false);
						unitChooser.mbSelected = false;
						unitChooser.mbOpenedSelected = false;
					}
				}
				unitdata = DataMgr.GetInstance().GetUnitData(openCharacterType);
				do
				{
					if(openCharacterType == EnumCharacterType.CHARACTER_TYPE_NONE)
						break;
					string spriteName = "";
					switch(openCharacterType)
					{
					case EnumCharacterType.CHARACTER_TYPE_RAT:
						spriteName = "unit_rat";
						break;
					case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
						spriteName = "unit_elephant";
	               		break;
	            	case EnumCharacterType.CHARACTER_TYPE_HORSE:
						spriteName = "unit_horse";
	        			break;
					case EnumCharacterType.CHARACTER_TYPE_ALPACA:
						spriteName = "unit_alpaca";
						break;
	                }
					unitInfo.SetActive(true);
					UISprite imgSprite = unitInfo.transform.FindChild("UnitInfo_Img").GetComponent<UISprite>();
					imgSprite.spriteName = spriteName;
					imgSprite.MakePixelPerfect();

					UILabel infoLabel = unitInfo.transform.FindChild("UnitPlumTxt").GetComponent<UILabel>();
					infoLabel.text = unitdata.name + "\n\n";
					infoLabel.text += unitdata.contents;
					unitInfo.transform.FindChild("BuyBtn").GetComponent<UIButtonMessage>().target = gameObject;

				}while(false);
                
				unitInfo.transform.FindChild("BuyBtn").gameObject.SetActive(false);
                mbOpenedSelected = true;
            }
            else // 오픈된 유닛 선택이 취소되었을 때
			{
				for(int i=1; i< 10; i++)
				{
					if(unitInfo != null)
					{
						UnitChooser unitChooser = gameObject.transform.parent.FindChild("Choose_"+i).GetComponent<UnitChooser>();
						unitChooser.unitInfo.gameObject.SetActive(false);
						unitChooser.mbSelected = false;
						unitChooser.mbOpenedSelected = false;
					}
				}

                PlayMgr.GetInstance().RemoveSelectedUnit(openCharacterType);
                gameObject.transform.FindChild("Img_Selected").gameObject.SetActive(false);
                unitInfo.SetActive(true);
                mbOpenedSelected = false;
            }
        }
        RefreshUnitList();
	}	
    
    public void BuyUnit(GameObject gameObj)
    {
		if( index >= mUnitList.Count )
			return;
		characterType =(EnumCharacterType)mUnitList[index];
		unitdata = DataMgr.GetInstance().GetUnitData(characterType);

		if(index == 0 ||(EnumCharacterType)mOpenUnitList[index-1] != EnumCharacterType.CHARACTER_TYPE_NONE )
		{
				mPlum = unitdata.plum;
	        if(PlayMgr.GetInstance().plum >= mPlum)
	        {
	            PlayMgr.GetInstance().plum -= mPlum;
	            PlayMgr.GetInstance().SetOpenUnitList(characterType);
	            RefreshOpenUnitList();
	            RefreshUnitList();
	            GameObject.FindWithTag ("plum").SendMessage("RefreshPlum");
	            mbSelected = false;
	        }
		}
    }
}