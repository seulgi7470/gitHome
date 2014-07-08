using UnityEngine;
using System.Collections;
using CommonData;
using System;

public class Bullet : MonoBehaviour {
	
	public int bulletPower;
	public int bulletSpeed;
	public int bulletRange;
	public EnumAliasType bulletAliasType;
	public EnumCharacterType bulletUnitType;
	public Vector3 startPosition;

	bool mbDied;
	bool bHit = false;
	SPSpriteAnimation mSpriteAnim;

	
	void Start (){
		mbDied = false;
	}

	// Update is called once per frame
	void Update () {
		//gameObject.rigidbody.AddForce (bulletSpeed, 0, 0);
		gameObject.transform.Translate(bulletSpeed*0.01f, 0, 0);
		if(bulletUnitType == EnumCharacterType.CHARACTER_TYPE_ELEPHANT)
		{
			if(mSpriteAnim)
			{
				mSpriteAnim.Reset();
				mSpriteAnim.namePrefix = "bullet";
				mSpriteAnim.isLoop = false;
			}
		}


		if(Math.Abs(gameObject.transform.localPosition.x - startPosition.x) > bulletRange)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (mbDied)
			return;

		if(bulletUnitType == EnumCharacterType.CHARACTER_TYPE_ELEPHANT)
		{
			if(coll.transform.tag == "enemy")
			{
				Debug.Log("Damage " + bulletPower);
				bHit = true;
			}
		}
		else
		{
			bHit = false;
			if (bulletAliasType == EnumAliasType.ALIAS_TYPE_ANIMAL && coll.transform.tag == "enemy") {		
				bHit = true;
			} 
			if (bulletAliasType == EnumAliasType.ALIAS_TYPE_HUMAN && coll.transform.tag == "unit") {
				bHit = true;
			}
		}

		if(bHit)
		{
			coll.GetComponent<Unit> ().Damage (bulletPower);
			Destroy (gameObject);
			mbDied = true;
		}
	}

	public void LoadBullet(BulletContext sendBC) {
		bulletSpeed = sendBC.speed;
		bulletPower = sendBC.damage;
		bulletRange = sendBC.range;
		bulletAliasType = sendBC.aliasType;
		bulletUnitType = sendBC.unitType;
		startPosition = sendBC.position;
	}

	void OnAnimationEnd()  
	{
		if (mbDied) {
			return;
		}
	}
}
