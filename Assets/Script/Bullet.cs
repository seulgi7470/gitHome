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
	public EnumBulletType bulletType;
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

		if(bulletType == EnumBulletType.BULLET_TYPE_THREE)
		{
			if(mSpriteAnim)
			{
				mSpriteAnim.Reset();
				mSpriteAnim.namePrefix = "bullet";
				mSpriteAnim.isLoop = false;
			}
			// bullet type에 따라 위치 옮기기
			if(gameObject.transform.FindChild("WaterCollider").localPosition.x < 90)
			{
				Vector3 temp = gameObject.transform.FindChild("WaterCollider").localPosition;
				temp.x += 10;
				gameObject.transform.FindChild("WaterCollider").localPosition = temp;
			}
			if(gameObject.transform.FindChild("WaterCollider").localPosition.y > -50)
			{
				Vector3 temp = gameObject.transform.FindChild("WaterCollider").localPosition;
				temp.y -= 6;
                gameObject.transform.FindChild("WaterCollider").localPosition = temp;
            }
		}
		else{
			gameObject.transform.Translate(bulletSpeed*0.01f, 0, 0);
		}


		if(Math.Abs(gameObject.transform.localPosition.x - startPosition.x) > bulletRange)
		{
			Destroy(gameObject);
		}
	}

	public void ChildrenCollision(Collider coll) {
		if (mbDied)
			return;

		bHit = false;

		if(bulletType != EnumBulletType.BULLET_TYPE_THREE)
		{
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
		bulletType = sendBC.bulletType;
	}

	void OnAnimationEnd()  
	{
		if (mbDied) {
			return;
		}
		Destroy (gameObject);
		mbDied = true;
	}
}
