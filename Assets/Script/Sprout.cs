using UnityEngine;
using System.Collections;

public class Sprout : MonoBehaviour {

	public TextMesh sproutText;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		int sproutValue = PlayMgr.GetInstance().sproutValue;
		sproutText.text = sproutValue.ToString ("N0");
	}
}
