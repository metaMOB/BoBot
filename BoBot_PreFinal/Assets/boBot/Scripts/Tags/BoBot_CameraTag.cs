using UnityEngine;
using System.Collections;

public class BoBot_CameraTag : MonoBehaviour {
	private BoBot_SmoothFollow2D cameraComponent;
	
	public float zoomLevelEnter = 8f;
	public float durationEnter = 2f;
	public float zoomLevelExit = 8f;
	public float durationExit = 2f;
	
	private BoBot_DebugComponent debugInfo;
			
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		Physics.IgnoreLayerCollision (0,1);
		Physics.IgnoreLayerCollision (0,2);
		Physics.IgnoreLayerCollision (0,3);
		Physics.IgnoreLayerCollision (0,4);
		Physics.IgnoreLayerCollision (0,5);
		Physics.IgnoreLayerCollision (0,6);
		Physics.IgnoreLayerCollision (0,7);
		Physics.IgnoreLayerCollision (0,10);
		cameraComponent = GameObject.Find("Main Camera").GetComponent<BoBot_SmoothFollow2D>();
	}
	
	void Update(){
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("CameraTag");
			debugInfo.addText ("> Zoom/Duration Enter  "+ (zoomLevelEnter).ToString("#0.00")+" / "+ (durationEnter).ToString("#0.00"));
			debugInfo.addText ("> Zoom/Duration Exit  "+ (zoomLevelExit).ToString("#0.00")+" / "+ (durationExit).ToString("#0.00"));
		}
	}
	
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")){ 
			cameraComponent.setZoomLevel (this.zoomLevelEnter, this.durationEnter);
		}
	}
}
