using UnityEngine;
using System.Collections;

public class BoBot_LightningTag : MonoBehaviour {
	
	private BoBot_LightningControl controller;
	
	public float timeBeetween = 0f;
	public float varianz = 0f;
	
	private float timeBeetweenOld = 0f;
	private float varianzOld = 0f;
	
	private BoBot_DebugComponent debugInfo;	
	// Use this for initialization
	void Start () {
		controller = GameObject.Find("LightningControl").GetComponent<BoBot_LightningControl>();
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		
	}
	
	void Update (){
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("LightningTag");
			debugInfo.addText ("> Time between "+ (timeBeetween).ToString("#0.00"));
			debugInfo.addText ("> Varianz "+ (varianz).ToString("#0.00"));
		}
	}
	
	void OnTriggerEnter (Collider other){
		if (other.CompareTag("Player")){
			timeBeetweenOld = controller.timeBetween;
			varianzOld = controller.varianz;
			controller.setLightning(timeBeetween, varianz);
		}
	}
	
	void OnTriggerExit (Collider other){
		if (other.CompareTag("Player")){
			controller.setLightning(timeBeetweenOld, varianzOld);
		}
	}
}
