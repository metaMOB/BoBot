using UnityEngine;
using System.Collections;

public class BoBot_InverseGravityComponent : BoBot_OnOffComponent {

 	private Rigidbody rigid;
	private Vector3 gravity;
	public bool inverse;
	public float factor = 4;
	
	private BoBot_DebugComponent debugInfo;
	
	void Start () {
		debugInfo = gameObject.GetComponentInChildren<BoBot_DebugComponent>();
		
		rigid = gameObject.rigidbody;
		gravity = Physics.gravity;
		isRunning = inverse;
	}
	
	void Update () {
	 	if (state && !rigid.isKinematic){
			rigid.AddForce (-gravity*rigid.mass*factor);
		}
		
		if (BoBotGlobal.debugging && debugInfo){
			debugInfo.addText ("InverseGravityComponent");
			debugInfo.addText ("> Active "+isRunning);
			debugInfo.addText ("> Vel: "+ (rigid.velocity.x).ToString("#0.##") + "/"+ (rigid.velocity.y).ToString("#0.##"));
		}
	}
}
