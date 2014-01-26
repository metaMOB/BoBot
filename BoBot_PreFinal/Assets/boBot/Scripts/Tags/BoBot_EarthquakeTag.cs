using UnityEngine;
using System.Collections;

public class BoBot_EarthquakeTag : MonoBehaviour {
	
	private BoBot_RiddleComponent riddle;
	private BoBot_DebugComponent debugInfo;
	
	// Use this for initialization
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		riddle = this.transform.parent.parent.GetComponent<BoBot_RiddleComponent>();

		
	}
	
	// Update is called once per frame
	void Update(){
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("EarthquakeTag");
		}
	}
		
	
	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")){ 	
			riddle.on ();
		}
	}
}
