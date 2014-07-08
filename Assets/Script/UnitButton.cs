using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;
public class UnitButton : MonoBehaviour {

	public int buttonIndex;

	public EnumCharacterType charType;

	// Use this for initializationtonI
	private bool isRefresh;
	void Start () {
		charType = EnumCharacterType.CHARACTER_TYPE_NONE;
		isRefresh = false;
	}
	
	// Update is called once per frame
	void Update () {

		if( !isRefresh )
		{
			refreshCharacter();
		}
		else
		{
			EnumCharacterType _charType = PlayMgr.GetInstance().GetSelectedUnitAt(buttonIndex);
			if( charType != _charType )
			{
				//현재 인덱스에 캐릭터가 바뀌었을떄
				charType = _charType;
				isRefresh = false;
			}
		}

	}

	void refreshCharacter()
	{
		string spriteName = "";
		EnumCharacterType charType1 = (EnumCharacterType)charType;
		Transform child = gameObject.transform.FindChild ("ChooseCharImg");
		switch (charType1) {
		case EnumCharacterType.CHARACTER_TYPE_NONE:
			spriteName =  "btn_unit_empty";
			child.transform.localScale = new Vector3(138,144,1);
			break;
		case EnumCharacterType.CHARACTER_TYPE_RAT:
				spriteName = "btn_unit_rat";
				child.transform.localScale = new Vector3(138,144,1);
				break;
		case EnumCharacterType.CHARACTER_TYPE_HORSE:
				spriteName = "btn_unit_horse";
				child.transform.localScale = new Vector3(138,144,1);
				break;
		case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
				spriteName = "btn_unit_elephant";
				child.transform.localScale = new Vector3(138,144,1);
			break;
		}

		if (child != null) {
			child.gameObject.SetActive (true);
			UISprite childName = child.GetComponent<UISprite> ();
			if(childName != null)
			{
				childName.spriteName = spriteName;
			}
		}
		isRefresh = true;
	}
}