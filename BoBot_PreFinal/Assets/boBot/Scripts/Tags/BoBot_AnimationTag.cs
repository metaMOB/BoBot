using UnityEngine;
using System.Collections;

public class BoBot_AnimationTag : MonoBehaviour {
	
	private BoBot_DebugComponent debugInfo;
	// Use this for initialization
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("AnimationTag");	
		}
	}
}
