using UnityEngine;
using System.Collections;

public class BoBot_AnimationTag : MonoBehaviour {
	
	private BoBot_DebugComponent debugInfo;
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
	}
	
	void Update () {
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("AnimationTag");	
		}
	}
}
