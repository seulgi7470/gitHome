using UnityEngine;
using System.Collections;
using CommonData;
using System;

public class Unit : MonoBehaviour {
	//DATA
	public EnumAliasType aliasType;
	public EnumCharacterType characterType;
	public EnumBulletType bulletType;

	public int bulletDelay;
	public int power;
	public int range;
	public float speed;  // 유닛의 속도
	public float returnSpeed; // 0에서 다시 되돌릴 유닛 속도
	public int bulletSpeed; // 불릿의 속도
	public float hp; // hp
	public float maxHp;
	public int price;
	public int plum;

	//상태
	bool mbMove = true;
	bool mbAttackStarted = false;
	bool mbDied = false;
	EnumAnimationState mAnimationState = EnumAnimationState.ANIMATION_STATE_NONE;
	SPSpriteAnimation mSpriteAnim;
	EnumGameState mGameState;

	// Use this for initialization
	void Start () {
		mbMove = true;
		mbAttackStarted = false;
		mbDied = false;
		mSpriteAnim = gameObject.GetComponentInChildren<SPSpriteAnimation> ();

		LoadUnitData ();
		Animate ( EnumAnimationState.ANIMATION_STATE_MOVE );
		mGameState = EnumGameState.GAME_STATE_PLAYGAME;
	}

	public void LoadUnitData() {

		UnitData unitData = DataMgr.GetInstance().GetUnitData(characterType);
		hp = maxHp = unitData.maxHp;
		speed = returnSpeed = unitData.speed;
		power = unitData.power;
		range = unitData.range;
		bulletDelay = unitData.bulletDelay;
		bulletSpeed = unitData.bulletSpeed;
		price = unitData.price;
		plum = unitData.plum;
	}

	// Update is called once per frame
	void Update () {

		if (mbDied)
			return;

		FindTarget ();
		ProcessAttack();
		ProcessMoving();

	}

	void FindTarget()
	{
		string strTag = "";
		switch (aliasType) {
		case EnumAliasType.ALIAS_TYPE_ANIMAL:
			strTag = "enemy";
			break;
		case EnumAliasType.ALIAS_TYPE_HUMAN:
			strTag = "unit";
			break;
		}

		mbMove = true;

		GameObject[] arrChar = GameObject.FindGameObjectsWithTag (strTag);
		if (arrChar != null) 
		{
			foreach (GameObject ob in arrChar) 
			{
				Unit unit = ob.GetComponent<Unit>();
				if(unit == null)
					continue;
				if(unit.IsDied())
					continue;
				if (Math.Abs(ob.transform.localPosition.x - gameObject.transform.localPosition.x) < range)
				{
					mbMove = false;
				}
			}
		}
	}

	void ProcessAttack()
	{
		if (!mbMove) {
			if (!mbAttackStarted) {
				StartCoroutine (StartAttack ());
				mbAttackStarted = true;
			}
		}
	}

	void ProcessMoving()
	{
		if(mbMove) {
			if(mAnimationState != EnumAnimationState.ANIMATION_STATE_MOVE)
				Animate(EnumAnimationState.ANIMATION_STATE_MOVE);
			gameObject.transform.Translate (speed * Time.deltaTime * 0.1f, 0, 0);
			mbAttackStarted = false;
		}
	}

	IEnumerator StartAttack()
	{
		if (!mbAttackStarted && !mbMove)
			Animate (EnumAnimationState.ANIMATION_STATE_LOAD);

		yield return new WaitForSeconds (bulletDelay);

		if (!mbDied && !mbMove) {
			Animate (EnumAnimationState.ANIMATION_STATE_ATTACK);
		}
	}


	void CreateBullet()
	{
		
		BulletContext sendBC;
		sendBC.delay = bulletDelay;
		sendBC.unitType = characterType;
		sendBC.aliasType = aliasType;
		sendBC.range = range + 100;
		sendBC.damage = power;
		sendBC.speed = bulletSpeed;
		sendBC.position = gameObject.transform.localPosition;
		if(characterType == EnumCharacterType.CHARACTER_TYPE_ELEPHANT)
		{
			sendBC.bulletType = EnumBulletType.BULLET_TYPE_THREE;
		}
		else
		{
			sendBC.bulletType = EnumBulletType.BULLET_TYPE_ONE;
		}
		GameObject.FindWithTag ("GM").SendMessage ("CreateBullet", sendBC);

	}

	void Animate(EnumAnimationState animState)
	{
		mAnimationState = animState;
		if(!mSpriteAnim)
			return;

		mSpriteAnim.Reset ();
		switch (animState) {
		case EnumAnimationState.ANIMATION_STATE_LOAD:
			mSpriteAnim.namePrefix = "unit_load";
			mSpriteAnim.isLoop = true;
			break;
		case EnumAnimationState.ANIMATION_STATE_ATTACK:
			mSpriteAnim.namePrefix = "unit_attack";
			mSpriteAnim.isLoop = false;
			break;
		case EnumAnimationState.ANIMATION_STATE_MOVE:
			mSpriteAnim.namePrefix = "unit_walk";
			mSpriteAnim.isLoop = true;
			break;
		case EnumAnimationState.ANIMATION_STATE_DEAD:
			mSpriteAnim.namePrefix = "unit_die";
			mSpriteAnim.isLoop = false;
			break;
		}
	}

	void OnAnimationEnd()  
	{
		if (mAnimationState == EnumAnimationState.ANIMATION_STATE_DEAD) {
			Destroy (gameObject);
		}

		if (mbDied) {
			return;
		}

		if (mAnimationState == EnumAnimationState.ANIMATION_STATE_ATTACK) {// 공격애니메이션이 끝났을때
			Animate (EnumAnimationState.ANIMATION_STATE_LOAD);
			CreateBullet ();
			mbAttackStarted = false;
		}
	}

	
	public void Damage(float dam)
	{
		if (mbDied)
			return;
		
		hp -= dam;

		if(gameObject == GameObject.Find("UnitTower(Clone)"))
		{
			GameObject.FindWithTag("UHP").SendMessage("DecreaseHP", gameObject);
		}
		if(gameObject == GameObject.Find("EnemyTower(Clone)"))
		{
			GameObject.FindWithTag("EHP").SendMessage("DecreaseHP", gameObject);
		}

		if (hp > 0) {
		} else if (hp <= 0) {
			mbDied = true;
			Animate (EnumAnimationState.ANIMATION_STATE_DEAD);
			switch (characterType)
			{
			case EnumCharacterType.CHARACTER_TYPE_UNITTOWER:
				GameObject.FindWithTag("GM").SendMessage("GameOver", false);
				break;
			case EnumCharacterType.CHARACTER_TYPE_ENEMYTOWER:
				PlayMgr.GetInstance().plum += plum;
				GameObject.FindWithTag("plum").SendMessage("RefreshPlum");
				GameObject.FindWithTag("GM").SendMessage("GameOver", true);
				break;
			case EnumCharacterType.CHARACTER_TYPE_ENEMY1:
				PlayMgr.GetInstance().plum += plum;
				GameObject.FindWithTag("plum").SendMessage("RefreshPlum");
				break;
			case EnumCharacterType.CHARACTER_TYPE_ENEMY2:
				PlayMgr.GetInstance().plum += plum;
				GameObject.FindWithTag("plum").SendMessage("RefreshPlum");
				break;
			}
		}
	}

	public bool IsDied()
	{
		return mbDied;
	}
}
