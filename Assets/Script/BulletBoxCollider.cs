using UnityEngine;
using System.Collections;

public class BulletBoxCollider: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll)
	{
		// 히트했다를 알려주기
		if(gameObject.transform.parent != null)
		{
			gameObject.transform.parent.SendMessage("ChildrenCollision",coll);
		}

	}
}
