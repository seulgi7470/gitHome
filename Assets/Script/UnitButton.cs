using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;
public class UnitButton : MonoBehaviour {

	public int buttonIndex;

	public EnumCharacterType charType;

	// Use this for initializationtonI
	void Start () {
		charType = EnumCharacterType.CHARACTER_TYPE_NONE;
	}
	
	// Update is called once per frame
	void Update () {
		List<int> selectList = PlayMgr.GetInstance ().GetSelectList ();

		if (buttonIndex < selectList.Count) {
			charType = (EnumCharacterType)selectList[buttonIndex];
			refreshCharacter();
		}
		else
		{
			charType = EnumCharacterType.CHARACTER_TYPE_NONE;
			Transform child = gameObject.transform.FindChild ("ChooseCharImg");
			if(child != null)
			{
				child.gameObject.SetActive(false);
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
				child.transform.localScale = new Vector3(153,137,1);
				break;
		case EnumCharacterType.CHARACTER_TYPE_RAT1:
				spriteName = "쥐1";
				child.transform.localScale = new Vector3(120,110,1);
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
	}
}