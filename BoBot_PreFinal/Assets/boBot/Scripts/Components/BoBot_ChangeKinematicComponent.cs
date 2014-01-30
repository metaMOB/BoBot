using UnityEngine;
using System.Collections;

public class BoBot_ChangeKinematicComponent: BoBot_OnOffComponent {
	
	public float delay = 5f;	
	private BoBot_DebugComponent debugInfo;
	private float timer = 0;
	private bool run = false;

	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		if (this.rigidbody != null){
			this.rigidbody.isKinematic = true;
		}		
	}
	
	void Update () {		
		if (this.state){
			timer += Time.deltaTime;
			if (timer > delay){
				if (this.rigidbody != null){
					this.rigidbody.isKinematic = false;	
					this.rigidbody.useGravity = true;	
				}
				
				foreach (Rigidbody body in gameObject.GetComponentsInChildren<Rigidbody>()){
					body.isKinematic = false;	
					body.useGravity = true;					
				}
			}
		}
		
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("ChangeKinematicComponent");
			debugInfo.addText ("> Active "+isRunning);
			debugInfo.addText ("> IsKinematic "+ this.rigidbody.isKinematic);
		}
	}
}
