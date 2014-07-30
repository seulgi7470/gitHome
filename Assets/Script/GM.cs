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
	public UILabel stageText;
	 
	bool mbSpawnChk = true;
	int mEnemyIndex = 0;
	int rand;
	float time;
	int[] mArrGoldPlum;

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

		Debug.Log ("get CurrentStageNo = " + stageNo);
		stageText.text = (PlayMgr.GetInstance().currentStageNo + 1).ToString("N0");
		PlayMgr.GetInstance().GetOpenUnitList();
		GameObject.FindWithTag("plum").SendMessage("ClearTempPlum");
		time = 0;
		mArrGoldPlum = PlayMgr.GetInstance().GetArrGoldPlum();
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayMgr.GetInstance().gameState == EnumGameState.GAME_STATE_PLAYGAME)
		{
			time += Time.deltaTime;
		//	Debug.Log ("Time : " + (int)time);
		}


		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if( PlayMgr.GetInstance().gameState == EnumGameState.GAME_STATE_PLAYGAME )
			{
				PauseGame();
			}
			else 
			{
				ReturnSelectStage();
			}
		}

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
		var unit = Instantiate
			(Resources.Load(GetUnitPrefabPath(charType)), Vector3.zero, Quaternion.identity) as GameObject;
		unit.transform.parent = unitObjPool;
		unit.transform.localScale = new Vector3 (1, 1, 1);

		int random = Random.Range (-1,2);
		float randomOffset = random * 5;

		unit.transform.localPosition += new Vector3(unitSpawn.transform.localPosition.x + randomOffset,
		                                            unitSpawn.transform.localPosition.y + randomOffset,
		                                            unitSpawn.transform.localPosition.z + randomOffset);

		UISprite sprite = unit.GetComponentInChildren<UISprite>();
		if(sprite){
			sprite.depth = Depth.MIN_UNIT_DEPTH - random; 
		}
	}

	public void CreateBullet(BulletContext sendBC) 
	{
		var bullet1 = Instantiate
			(Resources.Load (sendBC.prefab), Vector3.zero, Quaternion.identity) as GameObject;
		bullet1.transform.parent = bulletObjPool;
		bullet1.transform.localPosition= new Vector3(sendBC.position.x,
		                                             sendBC.position.y,
		                                             0);
		bullet1.transform.localScale = new Vector3 (1, 1, 15);
		bullet1.SendMessage("LoadBullet", sendBC);

		UISprite sprite = bullet1.GetComponentInChildren<UISprite>();
		if(sprite){
			sprite.depth = Depth.MIN_BULLET_DEPTH; 
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
			float randomOffset = random * 5;

			unit1.transform.localPosition += new Vector3(enemySpawn.transform.localPosition.x + randomOffset,
			                                             enemySpawn.transform.localPosition.y + randomOffset,
			                                             enemySpawn.transform.localPosition.z + randomOffset);

			UISprite sprite = unit1.GetComponentInChildren<UISprite>();
			if(sprite){
				sprite.depth = Depth.MIN_ENEMY_DEPTH - random; 
			}
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
		case EnumCharacterType.CHARACTER_TYPE_ELEPHANT:
			strPath += "Elephant";
			break;
		case EnumCharacterType.CHARACTER_TYPE_ALPACA:
			strPath += "Alpaca";
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
		if(time <= 100)
		{
			mArrGoldPlum[stageNo] = 3;
			PlayMgr.GetInstance().plum += (stageNo + 1) * 3 * 43;
		}
		else if(time <= 150)
		{
			mArrGoldPlum[stageNo] = 2;
			PlayMgr.GetInstance().plum += (stageNo + 1) * 2 * 43;
		}
		else if(time <= 200)
		{
			mArrGoldPlum[stageNo] = 1;
			PlayMgr.GetInstance().plum += (stageNo + 1) * 1 * 43;
		}
		else
		{
			mArrGoldPlum[stageNo] = 0;
			PlayMgr.GetInstance().plum += (stageNo + 1) * 0 * 43;
		}
		PlayMgr.GetInstance().SetArrGoldPlum(mArrGoldPlum);
		if(win)
		{
			PlayMgr.GetInstance().OpenNextStage();
		}

	}

	public void ReturnSelectStage() {
		PlayMgr.GetInstance().ClearSelectedUnit();
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
			PlayMgr.GetInstance().sproutValue = 30 + stageNo*10;
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
