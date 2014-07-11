using UnityEngine;
using System.Collections;

public class TowerHP : MonoBehaviour {

	public UIFilledSprite delayWidget;
//	public GameObject tower;
	// Use this for initialization
	void Start () {
		delayWidget.fillAmount = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DecreaseHP(GameObject gameObj)
	{
	//        	Debug.Log (gameObj.GetComponent<Unit>().hp);
		delayWidget.fillAmount = gameObj.GetComponent<Unit>().hp / gameObj.GetComponent<Unit>().maxHp;
	}
}
