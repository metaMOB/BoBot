using UnityEngine;
using System.Collections;

public class BoBot_LevelBGComponent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var height = Screen.height;
  		var width = Screen.width;
    	transform.localScale = new Vector3(width, 0.1f, height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
