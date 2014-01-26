using UnityEngine;
using System.Collections;

public class BoBot_SuccessTag : MonoBehaviour {
	public int nextRiddle;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (){
		//SAVE!!
		try {
			Debug.Log ("humbug");
			BoBotGlobal.environment.setActiveRiddle (nextRiddle);
		}
		catch {
		}
	}
}
