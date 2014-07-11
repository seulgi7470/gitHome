using UnityEngine;
using System.Collections;
using CommonData;
using System.Collections.Generic;

public class GM : MonoBehaviour {
	public GameObject unitTower;
	public Transform unitObjPool;
	public Transform unitSpawn;
	public GameObject enemyTower;
	public Transform enemyObjPool;
	public GameObject enemy;
	public Transform enemySpawn;
	public GameObject bullet;
	public Transform bulletObjPool;
	
	public GameObject startUI;
	public GameObject gameUI;
	public GameObject resultUI;
	public TextMesh resultText;
	public GameObject pauseUI;

	public int[,] enemyList;
	public int stageNo;
	public int sproutDelay = 0;
	public TextMesh stageText;

	bool mbSpawnChk = true;
	int mEnemyIndex = 0;
	int rand;

	// Use this for initialization
	void Start () {
		if (mbSpawnChk) {
			var unit1 = Instantiate
				(Resources.Load(GetUnitPrefabPath(EnumCharacterType.CHARACTER_TYPE_UNITTOWER)), 
				 Vector3.zero, Quaternion.identity) as GameObject;
			unit1.transform.parent = unitObjPool;
			unit1.transform.localScale = new Vector3 (1, 1, 15);
			unit1.transform.localPosition += unitSpawn.transform.localPosition;

			var unit2 = Instantiate
				(Resources.Load(GetUnitPrefabPath(EnumCharacterType.CHARACTER_TYPE_ENEMYTOWER)), 
				 Vector3.zero, Quaternion.identity) as GameObject;
			unit2.transform.parent = enemyObjPool;
			unit2.transform.localScale = new Vector3 (1, 1, 15);
			unit2.transform.localPosition += enemySpawn.transform.localPosition;
		
			mbSpawnChk = false;
		}
		if (!mbSpawnChk) {
			mbSpawnChk = true;
		}

		stageNo = PlayMgr.GetInstance().currentStageNo;
/*		if(stageNo >= 1)
		{
			PlayMgr.GetInstance().SetOpenUnitList(EnumCharacterType.CHARACTER_TYPE_ELEPHANT);
		}*/
		Debug.Log ("get CurrentStageNo = " + stageNo);
		stageText.text = (PlayMgr.GetInstance().currentStageNo + 1).ToString("N0");
		PlayMgr.GetInstance().GetOpenUnitList();

	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
		IncreaseSproutValue();
	}

	void IncreaseSproutValue()
	{
		if(gameUI.activeSelf)
		{
			if(Time.timeScale > 0)
			{
				sproutDelay++;
				int stageDelay = 10 - PlayMgr.GetInstance().currentStageNo;
				if(sproutDelay >  stageDelay )
				{
					PlayMgr.GetInstance().sproutValue++;
					sproutDelay = 0;
				}
			}
		}
	}

	public void CreateUnit(EnumCharacterType charType)
	{
		var unit1 = Instantiate
			(Resources.Load(GetUnitPrefabPath(charType)), Vector3.zero, Quaternion.identity) as GameObject;
		unit1.transform.parent = unitObjPool;
	
		if(charType == EnumCharacterType.CHARACTER_TYPE_ELEPHANT)
		{
			UnitData unitdata = DataMgr.GetInstance().GetUnitData(charType);
			// 백개의 랜덤 홀수는 1배 짝수중 0 <= x < 50 은 1.5배 60 <= x < 99 은 2배 50 <= x < 60 은 3배
			rand = Random.Range(0,100); 
			if(rand % 2 == 1)
			{
				unit1.transform.localScale = new Vector3(1,1,1);
			}
			else
			{
				if(rand >= 0 && rand < 50)
				{
					unit1.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
					unitdata.maxHp *= 1.2f;
				}
				else if(rand >= 60 && rand < 100)
				{
					unit1.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
					unitdata.maxHp *= 1.5f;
				}
				else
				{
					unit1.transform.localScale = new Vector3(2,2,2);
					unitdata.maxHp *= 2;
				}
			}
		}
		else
		{
			unit1.transform.localScale = new Vector3 (1, 1, 1);
		}

		int random = Random.Range (-1,2);
		if(random == -1)
		{
			unit1.transform.localPosition += new Vector3(unitSpawn.transform.localPosition.x - 5,
                                             unitSpawn.transform.localPosition.y - 5,
                                             unitSpawn.transform.localPosition.z);
		}
		else if(random == 0)
		{
			unit1.transform.localPosition += new Vector3(unitSpawn.transform.localPosition.x,
	                                             unitSpawn.transform.localPosition.y,
	                                             unitSpawn.transform.localPosition.z);
		}
		else
		{
			unit1.transform.localPosition += new Vector3(unitSpawn.transform.localPosition.x + 5,
	                                             unitSpawn.transform.localPosition.y + 5,
	                                             unitSpawn.transform.localPosition.z);
		}
		/*

		unit1.transform.localPosition += new Vector3((Random.Range(-1,1) * 3) + unitSpawn.transform.localPosition.x,
                                             	(Random.Range(-1,1) * 3) + unitSpawn.transform.localPosition.y,
		                                             unitSpawn.transform.localPosition.z);*/
	}

	public void CreateBullet(BulletContext sendBC) 
	{
		if(sendBC.unitType == EnumCharacterType.CHARACTER_TYPE_ENEMY2)
		{
			var bullet1 = Instantiate
				(Resources.Load ("Prefabs/Arrow"), Vector3.zero, Quaternion.identity) as GameObject;
			bullet1.transform.parent = bulletObjPool;
			bullet1.transform.localPosition = new Vector3(sendBC.position.x,
			            						sendBC.position.y + 48.0f,
			           							sendBC.position.z);
	//		Debug.Log ("sendBC position " + sendBC.position + " arrow position " + bullet1.transform.localPosition);

			bullet1.transform.localScale = new Vector3 (1, 1, 15);
			bullet1.SendMessage("LoadBullet", sendBC);
        }
		else if(sendBC.unitType == EnumCharacterType.CHARACTER_TYPE_ELEPHANT)
		{
			var bullet1 = Instantiate
				(Resources.Load ("Prefabs/Water"), Vector3.zero, Quaternion.identity) as GameObject;
			bullet1.transform.parent = bulletObjPool;
			if(rand % 2 == 1)
			{
				bullet1.transform.localPosition = new Vector3(sendBC.position.x + 115.0f,
				                                              sendBC.position.y + 90.0f,
				                                              sendBC.position.z);
			}
			else
			{
				if(rand >= 0 && rand < 50)
				{
					bullet1.transform.localPosition = new Vector3(sendBC.position.x + (115.0f * 1.2f),
					                                              sendBC.position.y + (90.0f * 1.2f),
					                                              sendBC.position.z);
				}
				else if(rand >= 60 && rand < 100)
				{
					bullet1.transform.localPosition = new Vector3(sendBC.position.x + (115.0f * 1.5f),
					                                              sendBC.position.y + (90.0f * 1.5f),
					                                              sendBC.position.z);
				}
				else
				{
					bullet1.transform.localPosition = new Vector3(sendBC.position.x + (115.0f * 2.0f),
					                                              sendBC.position.y + (90.0f * 2.0f),
					                                              sendBC.position.z);
				}
			}
			
	//		Debug.Log ("sendBC position " + sendBC.position + " water position " + bullet1.transform.localPosition);
			bullet1.transform.localScale = new Vector3 (1, 1, 15);
			bullet1.SendMessage("LoadBullet", sendBC);
		}
		else
		{
			var bullet1 = Instantiate
				(Resources.Load ("prefabs/Bullet"), Vector3.zero, Quaternion.identity) as GameObject;
			bullet1.transform.parent = bulletObjPool;
			bullet1.transform.localPosition = sendBC.position;
			bullet1.transform.localScale = new Vector3 (1, 1, 15);
			bullet1.SendMessage("LoadBullet", sendBC);
		}

	}

	IEnumerator CreateEnemy()
	{


		enemyList =	StageInfo.GetInstance().GetEnemyList();
		if(mEnemyIndex >= 10)
			mEnemyIndex = 0;

		int enemyNum = enemyList[stageNo, mEnemyIndex];
		EnumCharacterType charType = StageInfo.GetInstance().GetEnemyType(enemyNum);

		yield return new WaitForSeconds(StageInfo.GetInstance().CreateEnemyDelay(enemyNum));

		if(charType != EnumCharacterType.CHARACTER_TYPE_NONE)
		{
			var unit1 = Instantiate
				(Resources.Load(GetUnitPrefabPath(charType)), Vector3.zero, Quaternion.identity) as GameObject;
			unit1.transform.parent = enemyObjPool;
			unit1.transform.localScale = new Vector3 (1, 1, 1);
		
			int random = Random.Range (-1,2);
			if(random == -1)
			{
				unit1.transform.localPosition += new Vector3(enemySpawn.transform.localPosition.x - 5,
				                                             enemySpawn.transform.localPosition.y - 5,
				                                             enemySpawn.transform.localPosition.z);
			}
			else if(random == 0)
			{
				unit1.transform.localPosition += new Vector3(enemySpawn.transform.localPosition.x,
				                                             enemySpawn.transform.localPosition.y,
				                                             enemySpawn.transform.localPosition.z);
			}
			else
			{
				unit1.transform.localPosition += new Vector3(enemySpawn.transform.localPosition.x + 5,
				                                             enemySpawn.transform.localPosition.y + 5,
				                                             enemySpawn.transform.localPosition.z);
			}
			/*
			unit1.transform.localPosition += new Vector3((Random.Range(-1,1) * 3) + enemySpawn.transform.localPosition.x,
	                                             (Random.Range(-1,1) * 3) + enemySpawn.transform.localPosition.y,
	                                             enemySpawn.transform.localPosition.z); */

		}
			
		mEnemyIndex++;
		StartCoroutine(CreateEnemy());

	}

	public string GetUnitPrefabPath(EnumCharacterType charType)
	{
		string strPath = "Prefabs/";
		switch(charType)
		{
		case EnumCharacterType.CHARACTER_TYPE_RAT:
			strPath += "Rat";
			break;
		case EnumCharacterType.CHARACTER_TYPE_HORSE:
			strPath += "Horse";
			break;
		case EnumCharacterType.CHARACTER_TYPE_RAT1:
			strPath += "Rat1";
			break;
		case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
			strPath += "Elephant";
			break;
		case EnumCharacterType.CHARACTER_TYPE_ENEMY1:
			strPath += "Enemy1";
			break;
		case EnumCharacterType.CHARACTER_TYPE_ENEMY2:
			strPath += "Enemy2";
			break;
		case EnumCharacterType.CHARACTER_TYPE_UNITTOWER:
			strPath += "UnitTower";
			break;
		case EnumCharacterType.CHARACTER_TYPE_ENEMYTOWER:
			strPath += "EnemyTower";
			break;
		}
		
		return strPath;
	}

	public void ChangeUItoState(EnumGameState eGameState)
	{
		PlayMgr.GetInstance().gameState = eGameState;
		switch(eGameState)
		{
		case EnumGameState.GAME_STATE_STOP:
			startUI.SetActive(false);
			resultUI.SetActive(false);
			pauseUI.SetActive(true);
			Time.timeScale = 0.0f;
			break;
		case EnumGameState.GAME_STATE_SELECTUNIT:
			PlayMgr.GetInstance().ClearSelectedUnit();
			startUI.SetActive (true);
			gameUI.SetActive (false);
			resultUI.SetActive(false);
			pauseUI.SetActive(false);
			Application.LoadLevel("savetheplum");
			break;
		case EnumGameState.GAME_STATE_PLAYGAME:
			startUI.SetActive (false);
			gameUI.SetActive (true);
			resultUI.SetActive(false);
			pauseUI.SetActive(false);
			Time.timeScale = 1.0f;
			break;
		case EnumGameState.GAME_STATE_GAMERESULT:
			Time.timeScale = 0.0f;
			startUI.SetActive(false);
			resultUI.SetActive(true);
			pauseUI.SetActive(false);
			break;
		}
	}

	public void GameOver(bool win) {

		ChangeUItoState(EnumGameState.GAME_STATE_GAMERESULT);
		resultUI.SendMessage("SetGameResult", win);
		if(win)
		{
			PlayMgr.GetInstance().OpenNextStage();
		}

	}

	public void ReturnSelectStage() {
		Application.LoadLevel("selectstage");
	}
	
	public void ReplayGame() {
		PlayMgr.GetInstance().currentStageNo = stageNo;
		ChangeUItoState(EnumGameState.GAME_STATE_SELECTUNIT);
	}

	public void NextGame() {
		PlayMgr.GetInstance().currentStageNo++;
		ChangeUItoState(EnumGameState.GAME_STATE_SELECTUNIT);
	}

	public void StartGame() {
		if(PlayMgr.GetInstance().CountSelectedUnit() <= 0)
			return;
		else
		{
			ChangeUItoState(EnumGameState.GAME_STATE_PLAYGAME);
			PlayMgr.GetInstance().sproutValue = 0;
			StartCoroutine (CreateEnemy ());
		}
	}

	public void PauseGame() {
		ChangeUItoState(EnumGameState.GAME_STATE_STOP);
	}

	public void ReturnGame() {
		ChangeUItoState(EnumGameState.GAME_STATE_PLAYGAME);
	}

	public void OnPressedStartBtn(GameObject gameObj) {
		//gameObj.GetComponentInChildren<UISprite>().spriteName = "btn_start_on";
	}

	public void OnReleasedStartBtn(GameObject gameObj) {
		//gameObj.GetComponentInChildren<UISprite>().spriteName = "btn_start";
	}
}
