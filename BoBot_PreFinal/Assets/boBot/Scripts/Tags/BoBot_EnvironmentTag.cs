using UnityEngine;
using System.Collections;

public class BoBot_EnvironmentTag : MonoBehaviour {
	
	private BoBot_LightningControl controller;
	private BoBot_Rain rain;
	
	public float timeBeetween = 0f;
	public float varianz = 0f;
	
	public int minFlashes = 1;
	public int maxFlashes = 2;
	public bool noSound = false;
	public float rainIntensity = 0f;
	public float rainDeltaTime = 1f;
	
	private float timeBeetweenOld = 0f;
	private float varianzOld = 0f;
	
	private BoBot_DebugComponent debugInfo;	
	
	// Use this for initialization
	void Start () {
		controller = GameObject.Find("LightningControl").GetComponent<BoBot_LightningControl>();
		rain = GameObject.Find("Rain").GetComponent<BoBot_Rain>();
		
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
		//Debug.Log ("name "+other.tag);
		if (other.CompareTag("Player")){
			//timeBeetweenOld = controller.timeBetween;
			//varianzOld = controller.varianz;
			controller.setLightning(timeBeetween, varianz, noSound, minFlashes, maxFlashes);
			rain.setIntensity(rainIntensity, rainDeltaTime); 
		}
	}
	
	void OnTriggerExit (Collider other){
		/*if (other.CompareTag("Player")){
			controller.setLightning(timeBeetween, varianz, noSound, minFlashes, maxFlashes);
		}*/
	}
}
