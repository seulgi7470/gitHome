using UnityEngine;
using System.Collections;

public class Camera_Move : MonoBehaviour {

	public float beginX, beginY, moveX, moveY, endX, endY;

	float mDestX;

	void Start() {
		mDestX = gameObject.transform.position.x;
	}

	void Update() {
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				beginX = Input.GetTouch(0).position.x;
				beginY = Input.GetTouch(0).position.y;
			} 
			else if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				moveX = Input.GetTouch(0).position.x;
				moveY = Input.GetTouch(0).position.y;
			} 
			else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				endX = Input.GetTouch(0).position.x;
				endY = Input.GetTouch(0).position.y;
				float dx = (beginX - endX) * 0.01f;
				mDestX = gameObject.transform.position.x + dx;
			}
		}
		float deltaX = mDestX - gameObject.transform.position.x;
		MoveCamera (deltaX);
	}

	void MoveCamera(float dx)
	{
		transform.Translate (((dx * 0.1f)*100.0f) / 100, 0, 0);
		
		if (transform.localPosition.x >= 1000.0f) {
			transform.localPosition = new Vector3( 1000.0f, transform.localPosition.y, transform.localPosition.z);
			return;
		}
		
		if (transform.localPosition.x < 0){
			transform.localPosition = new Vector3( 0, transform.localPosition.y, transform.localPosition.z);
			return;
		}
	}

}