using UnityEngine;
using System.Collections;
using CommonData;
using System;

public class Bullet : MonoBehaviour {
	
	public int bulletPower;
	public int bulletSpeed;
	public int bulletRange;
	public EnumAliasType bulletAliasType;
	public Vector3 startPosition;

	bool mbDied;
	
	void Start (){
		mbDied = false;
	}

	// Update is called once per frame
	void Update () {
		//gameObject.rigidbody.AddForce (bulletSpeed, 0, 0);
		gameObject.transform.Translate(bulletSpeed*0.01f, 0, 0);
		
		if(Math.Abs(gameObject.transform.localPosition.x - startPosition.x) > bulletRange)
		{
	//		Debug.Log("Bullet Destroy");
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (mbDied)
			return;

		bool bHit = false;
	//	Debug.Log(coll.transform.tag);
		if (bulletAliasType == EnumAliasType.ALIAS_TYPE_ANIMAL && coll.transform.tag == "enemy") {		
				bHit = true;
		} 
		if (bulletAliasType == EnumAliasType.ALIAS_TYPE_HUMAN && coll.transform.tag == "unit") {
				bHit = true;
		}
		if(bHit)
		{
	//		Debug.Log("Hit");
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
		startPosition = sendBC.position;
	}
}
